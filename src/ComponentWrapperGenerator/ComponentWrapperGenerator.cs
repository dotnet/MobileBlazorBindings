// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Input;
using System.Xml;
using XF = Xamarin.Forms;

namespace ComponentWrapperGenerator
{
    // TODO: XML Doc Comments

#pragma warning disable CA1724 // Type name conflicts with namespace name
    public class ComponentWrapperGenerator
#pragma warning restore CA1724 // Type name conflicts with namespace name
    {
        public ComponentWrapperGenerator(GeneratorSettings settings)
        {
            Settings = settings ?? throw new ArgumentNullException(nameof(settings));
        }

        private GeneratorSettings Settings { get; }

        public void GenerateComponentWrapper(Type typeToGenerate, XmlDocument xmlDocs, string outputFolder)
        {
            typeToGenerate = typeToGenerate ?? throw new ArgumentNullException(nameof(typeToGenerate));

            var propertiesToGenerate = GetPropertiesToGenerate(typeToGenerate);

            GenerateComponentFile(typeToGenerate, propertiesToGenerate, xmlDocs, outputFolder);
            GenerateHandlerFile(typeToGenerate, propertiesToGenerate, outputFolder);
        }

        private void GenerateComponentFile(Type typeToGenerate, IEnumerable<PropertyInfo> propertiesToGenerate, XmlDocument xmlDocs, string outputFolder)
        {
            var fileName = Path.Combine(outputFolder, $"{typeToGenerate.Name}.generated.cs");
            var directoryName = Path.GetDirectoryName(fileName);
            if (!string.IsNullOrEmpty(directoryName))
            {
                Directory.CreateDirectory(directoryName);
            }

            Console.WriteLine($"Generating component for type '{typeToGenerate.FullName}' into file '{fileName}'.");

            var componentName = typeToGenerate.Name;
            var componentHandlerName = $"{componentName}Handler";
            var componentBaseName = GetBaseTypeOfInterest(typeToGenerate).Name;

            // header
            var headerText = Settings.FileHeader;

            // usings
            var usings = new List<UsingStatement>
            {
                new UsingStatement { Namespace = "Microsoft.AspNetCore.Components" },
                new UsingStatement { Namespace = "Microsoft.MobileBlazorBindings.Core" },
                new UsingStatement { Namespace = "Microsoft.MobileBlazorBindings.Elements.Handlers" },
                new UsingStatement { Namespace = "System.Threading.Tasks" },
                new UsingStatement { Namespace = "Xamarin.Forms", Alias = "XF" }
            };

            // props
            var propertyDeclarationBuilder = new StringBuilder();
            if (propertiesToGenerate.Any())
            {
                propertyDeclarationBuilder.AppendLine();
            }
            foreach (var prop in propertiesToGenerate)
            {
                propertyDeclarationBuilder.Append(GetPropertyDeclaration(prop, usings, xmlDocs));
            }
            var propertyDeclarations = propertyDeclarationBuilder.ToString();

            var propertyAttributeBuilder = new StringBuilder();
            foreach (var prop in propertiesToGenerate)
            {
                propertyAttributeBuilder.Append(GetPropertyRenderAttribute(prop));
            }
            var propertyAttributes = propertyAttributeBuilder.ToString();
            var eventHandlerAttributes = "";

            var usingsText = string.Join(
                Environment.NewLine,
                usings
                    .Distinct()
                    .Where(u => u.Namespace != Settings.RootNamespace)
                    .OrderBy(u => u.ComparableString)
                    .Select(u => u.UsingText));

            var isComponentAbstract = typeToGenerate.IsAbstract;
            var classModifiers = string.Empty;
            if (isComponentAbstract)
            {
                classModifiers += "abstract ";
            }
            var componentHasPublicParameterlessConstructor =
                typeToGenerate
                    .GetConstructors()
                    .Any(ctor => ctor.IsPublic && !ctor.GetParameters().Any());

            var staticConstructor = string.Empty;
            if (!isComponentAbstract && componentHasPublicParameterlessConstructor)
            {
                staticConstructor = $@"        static {componentName}()
        {{
            ElementHandlerRegistry.RegisterElementHandler<{componentName}>(
                renderer => new {componentHandlerName}(renderer, new XF.{componentName}()));
        }}
";
            }

            var outputBuilder = new StringBuilder();
            outputBuilder.Append($@"{headerText}
{usingsText}

namespace {Settings.RootNamespace}
{{
    public {classModifiers}partial class {componentName} : {componentBaseName}
    {{
{staticConstructor}{propertyDeclarations}
        public new XF.{componentName} NativeControl => (({componentHandlerName})ElementHandler).{componentName}Control;

        protected override void RenderAttributes(AttributesBuilder builder)
        {{
            base.RenderAttributes(builder);

{propertyAttributes}{eventHandlerAttributes}
            RenderAdditionalAttributes(builder);
        }}

        partial void RenderAdditionalAttributes(AttributesBuilder builder);
    }}
}}
");

            File.WriteAllText(fileName, outputBuilder.ToString());
        }

        private static readonly List<Type> DisallowedComponentPropertyTypes = new List<Type>
        {
            typeof(XF.Button.ButtonContentLayout), // TODO: This is temporary; should be possible to add support later
            typeof(XF.ColumnDefinitionCollection),
            typeof(XF.ControlTemplate),
            typeof(XF.DataTemplate),
            typeof(XF.Element),
            typeof(XF.Font), // TODO: This is temporary; should be possible to add support later
            typeof(XF.FormattedString),
            typeof(XF.Shapes.Geometry),
            typeof(ICommand),
            typeof(XF.Keyboard), // TODO: This is temporary; should be possible to add support later
            typeof(object),
            typeof(XF.Page),
            typeof(XF.ResourceDictionary),
            typeof(XF.RowDefinitionCollection),
            typeof(XF.ShellContent),
            typeof(XF.ShellItem),
            typeof(XF.ShellSection),
            typeof(XF.Style), // TODO: This is temporary; should be possible to add support later
            typeof(XF.IVisual),
            typeof(XF.View),
        };

        private static string GetPropertyDeclaration(PropertyInfo prop, IList<UsingStatement> usings, XmlDocument xmlDocs)
        {
            var propertyType = prop.PropertyType;
            string propertyTypeName;
            if (propertyType == typeof(IList<string>))
            {
                // Lists of strings are special-cased because they are handled specially by the handlers as a comma-separated list
                propertyTypeName = "string";
            }
            else
            {
                propertyTypeName = GetTypeNameAndAddNamespace(propertyType, usings);
                if (propertyType.IsValueType)
                {
                    propertyTypeName += "?";
                }
            }
            const string indent = "        ";

            var xmlDocContents = GetXmlDocContents(prop, xmlDocs, indent);

            return $@"{xmlDocContents}{indent}[Parameter] public {propertyTypeName} {GetIdentifierName(prop.Name)} {{ get; set; }}
";
        }

        private static string GetXmlDocText(XmlElement xmlDocElement)
        {
            var allText = xmlDocElement?.InnerXml;
            allText = allText.Replace("To be added.", string.Empty, StringComparison.Ordinal);
            if (string.IsNullOrWhiteSpace(allText))
            {
                return null;
            }
            return allText;
        }

        private static string GetXmlDocContents(PropertyInfo prop, XmlDocument xmlDocs, string indent)
        {
            var xmlDocContents = string.Empty;
            // Format of XML docs we're looking for in a given property:
            // <member name="P:Xamarin.Forms.ActivityIndicator.Color">
            //     <summary>Gets or sets the <see cref="T:Xamarin.Forms.Color" /> of the ActivityIndicator. This is a bindable property.</summary>
            //     <value>A <see cref="T:Xamarin.Forms.Color" /> used to display the ActivityIndicator. Default is <see cref="P:Xamarin.Forms.Color.Default" />.</value>
            //     <remarks />
            // </member>
            var xmlDocNodeName = $"P:{prop.DeclaringType.Namespace}.{prop.DeclaringType.Name}.{prop.Name}";
            var xmlDocNode = xmlDocs.SelectSingleNode($"//member[@name='{xmlDocNodeName}']");
            if (xmlDocNode != null)
            {
                var summaryText = GetXmlDocText(xmlDocNode["summary"]);
                var valueText = GetXmlDocText(xmlDocNode["value"]);

                if (summaryText != null || valueText != null)
                {
                    var xmlDocContentBuilder = new StringBuilder();
                    if (summaryText != null)
                    {
                        xmlDocContentBuilder.AppendLine($"{indent}/// <summary>");
                        xmlDocContentBuilder.AppendLine($"{indent}/// {summaryText}");
                        xmlDocContentBuilder.AppendLine($"{indent}/// </summary>");
                    }
                    if (valueText != null)
                    {
                        xmlDocContentBuilder.AppendLine($"{indent}/// <value>");
                        xmlDocContentBuilder.AppendLine($"{indent}/// {valueText}");
                        xmlDocContentBuilder.AppendLine($"{indent}/// </value>");
                    }
                    xmlDocContents = xmlDocContentBuilder.ToString();
                }
            }

            return xmlDocContents;
        }

        private static string GetTypeNameAndAddNamespace(Type type, IList<UsingStatement> usings)
        {
            var typeName = GetCSharpType(type);
            if (typeName != null)
            {
                return typeName;
            }

            // Check if there's a 'using' already. If so, check if it has an alias. If not, add a new 'using'.
            var namespaceAlias = string.Empty;

            var existingUsing = usings.FirstOrDefault(u => u.Namespace == type.Namespace);
            if (existingUsing == null)
            {
                usings.Add(new UsingStatement { Namespace = type.Namespace });
            }
            else
            {
                if (existingUsing.Alias != null)
                {
                    namespaceAlias = existingUsing.Alias + ".";
                }
            }
            typeName = namespaceAlias + FormatTypeName(type, usings);
            return typeName;
        }

        private static string FormatTypeName(Type type, IList<UsingStatement> usings)
        {
            if (!type.IsGenericType)
            {
                return type.Name;
            }
            var typeNameBuilder = new StringBuilder();
            typeNameBuilder.Append(type.Name.Substring(0, type.Name.IndexOf('`', StringComparison.Ordinal)));
            typeNameBuilder.Append("<");
            var genericArgs = type.GetGenericArguments();
            for (var i = 0; i < genericArgs.Length; i++)
            {
                if (i > 0)
                {
                    typeNameBuilder.Append(", ");
                }
                typeNameBuilder.Append(GetTypeNameAndAddNamespace(genericArgs[i], usings));

            }
            typeNameBuilder.Append(">");
            return typeNameBuilder.ToString();
        }

        private static readonly Dictionary<Type, Func<string, string>> TypeToAttributeHelperGetter = new Dictionary<Type, Func<string, string>>
        {
            { typeof(XF.Color), propValue => $"AttributeHelper.ColorToString({propValue})" },
            { typeof(XF.CornerRadius), propValue => $"AttributeHelper.CornerRadiusToString({propValue})" },
            { typeof(XF.ImageSource), propValue => $"AttributeHelper.ImageSourceToString({propValue})" },
            { typeof(XF.LayoutOptions), propValue => $"AttributeHelper.LayoutOptionsToString({propValue})" },
            { typeof(XF.Thickness), propValue => $"AttributeHelper.ThicknessToString({propValue})" },
            { typeof(bool), propValue => $"{propValue}" },
            { typeof(double), propValue => $"AttributeHelper.DoubleToString({propValue})" },
            { typeof(float), propValue => $"AttributeHelper.SingleToString({propValue})" },
            { typeof(int), propValue => $"{propValue}" },
            { typeof(string), propValue => $"{propValue}" },
            { typeof(IList<string>), propValue => $"{propValue}" },
        };

        private static string GetPropertyRenderAttribute(PropertyInfo prop)
        {
            var propValue = prop.PropertyType.IsValueType ? $"{GetIdentifierName(prop.Name)}.Value" : GetIdentifierName(prop.Name);
            var formattedValue = propValue;
            if (TypeToAttributeHelperGetter.TryGetValue(prop.PropertyType, out var formattingFunc))
            {
                formattedValue = formattingFunc(propValue);
            }
            else if (prop.PropertyType.IsEnum)
            {
                formattedValue = $"(int){formattedValue}";
            }
            else
            {
                // TODO: Error?
                Console.WriteLine($"WARNING: Couldn't generate attribute render for {prop.DeclaringType.Name}.{prop.Name}");
            }

            return $@"            if ({GetIdentifierName(prop.Name)} != null)
            {{
                builder.AddAttribute(nameof({GetIdentifierName(prop.Name)}), {formattedValue});
            }}
";
        }

        private static readonly Dictionary<Type, string> TypeToCSharpName = new Dictionary<Type, string>
        {
            { typeof(bool), "bool" },
            { typeof(byte), "byte" },
            { typeof(sbyte), "sbyte" },
            { typeof(char), "char" },
            { typeof(decimal), "decimal" },
            { typeof(double), "double" },
            { typeof(float), "float" },
            { typeof(int), "int" },
            { typeof(uint), "uint" },
            { typeof(long), "long" },
            { typeof(ulong), "ulong" },
            { typeof(object), "object" },
            { typeof(short), "short" },
            { typeof(ushort), "ushort" },
            { typeof(string), "string" },
        };

        private static string GetCSharpType(Type propertyType)
        {
            return TypeToCSharpName.TryGetValue(propertyType, out var typeName) ? typeName : null;
        }

        /// <summary>
        /// Finds the next non-generic base type of the specified type. This matches the Mobile Blazor Bindings
        /// model where there is no need to represent the intermediate generic base classes because they are
        /// generally only containers and have no API functionality that needs to be generated.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private static Type GetBaseTypeOfInterest(Type type)
        {
            do
            {
                type = type.BaseType;
                if (!type.IsGenericType)
                {
                    return type;
                }
            }
            while (type != null);

            return null;
        }

        private void GenerateHandlerFile(Type typeToGenerate, IEnumerable<PropertyInfo> propertiesToGenerate, string outputFolder)
        {
            var fileName = Path.Combine(outputFolder, "Handlers", $"{typeToGenerate.Name}Handler.generated.cs");
            var directoryName = Path.GetDirectoryName(fileName);
            if (!string.IsNullOrEmpty(directoryName))
            {
                Directory.CreateDirectory(directoryName);
            }

            Console.WriteLine($"Generating component handler for type '{typeToGenerate.FullName}' into file '{fileName}'.");

            var componentName = typeToGenerate.Name;
            var componentVarName = char.ToLowerInvariant(componentName[0]) + componentName.Substring(1);
            var componentHandlerName = $"{componentName}Handler";
            var componentBaseName = GetBaseTypeOfInterest(typeToGenerate).Name;
            var componentHandlerBaseName = $"{componentBaseName}Handler";

            // header
            var headerText = Settings.FileHeader;

            // usings
            var usings = new List<UsingStatement>
            {
                //new UsingStatement { Namespace = "Microsoft.AspNetCore.Components" }, // Typically needed only when there are event handlers for the EventArgs types
                new UsingStatement { Namespace = "Microsoft.MobileBlazorBindings.Core" },
                new UsingStatement { Namespace = "System" },
                new UsingStatement { Namespace = "Xamarin.Forms", Alias = "XF" }
            };

            // props
            var propertySettersBuilder = new StringBuilder();
            foreach (var prop in propertiesToGenerate)
            {
                propertySettersBuilder.Append(GetPropertySetAttribute(prop, usings));
            }
            var propertySetters = propertySettersBuilder.ToString();

            var usingsText = string.Join(
                Environment.NewLine,
                usings
                    .Distinct()
                    .Where(u => u.Namespace != Settings.RootNamespace)
                    .OrderBy(u => u.ComparableString)
                    .Select(u => u.UsingText));

            var isComponentAbstract = typeToGenerate.IsAbstract;
            var classModifiers = string.Empty;
            if (isComponentAbstract)
            {
                classModifiers += "abstract ";
            }

            var applyAttributesMethod = string.Empty;
            if (!string.IsNullOrEmpty(propertySetters))
            {
                applyAttributesMethod = $@"
        public override void ApplyAttribute(ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName)
        {{
            switch (attributeName)
            {{
{propertySetters}                default:
                    base.ApplyAttribute(attributeEventHandlerId, attributeName, attributeValue, attributeEventUpdatesAttributeName);
                    break;
            }}
        }}
";
            }

            var outputBuilder = new StringBuilder();
            outputBuilder.Append($@"{headerText}
{usingsText}

namespace {Settings.RootNamespace}.Handlers
{{
    public {classModifiers}partial class {componentHandlerName} : {componentHandlerBaseName}
    {{
        public {componentName}Handler(NativeComponentRenderer renderer, XF.{componentName} {componentVarName}Control) : base(renderer, {componentVarName}Control)
        {{
            {componentName}Control = {componentVarName}Control ?? throw new ArgumentNullException(nameof({componentVarName}Control));

            Initialize(renderer);
        }}

        partial void Initialize(NativeComponentRenderer renderer);

        public XF.{componentName} {componentName}Control {{ get; }}
{applyAttributesMethod}    }}
}}
");

            File.WriteAllText(fileName, outputBuilder.ToString());
        }

        private static string GetPropertySetAttribute(PropertyInfo prop, List<UsingStatement> usings)
        {
            // Handle null values by resetting to default value
            var resetValueParameterExpression = string.Empty;
            var bindablePropertyForProp = GetBindablePropertyForProp(prop);
            if (bindablePropertyForProp != null)
            {
                var declaredDefaultValue = bindablePropertyForProp.DefaultValue;
                var defaultValueForType = GetDefaultValueForType(prop.PropertyType);
                var needsCustomResetValue = declaredDefaultValue == null ? false : !declaredDefaultValue.Equals(defaultValueForType);

                if (needsCustomResetValue)
                {
                    var valueExpression = GetValueExpression(declaredDefaultValue, usings);
                    if (string.IsNullOrEmpty(valueExpression))
                    {
                        Console.WriteLine($"WARNING: Couldn't get value expression for {prop.DeclaringType.Name}.{prop.Name} of type {prop.PropertyType.FullName}.");
                    }
                    resetValueParameterExpression = valueExpression;
                }
            }

            var formattedValue = string.Empty;
            if (TypeToAttributeHelperSetter.TryGetValue(prop.PropertyType, out var propValueFormat))
            {
                var resetValueParameterExpressionAsExtraParameter = string.Empty;
                if (!string.IsNullOrEmpty(resetValueParameterExpression))
                {
                    resetValueParameterExpressionAsExtraParameter = ", " + resetValueParameterExpression;
                }
                formattedValue = string.Format(CultureInfo.InvariantCulture, propValueFormat, resetValueParameterExpressionAsExtraParameter);
            }
            else if (prop.PropertyType.IsEnum)
            {
                var resetValueParameterExpressionAsExtraParameter = string.Empty;
                if (!string.IsNullOrEmpty(resetValueParameterExpression))
                {
                    resetValueParameterExpressionAsExtraParameter = ", (int)" + resetValueParameterExpression;
                }
                var castTypeName = GetTypeNameAndAddNamespace(prop.PropertyType, usings);
                formattedValue = $"({castTypeName})AttributeHelper.GetInt(attributeValue{resetValueParameterExpressionAsExtraParameter})";
            }
            else if (prop.PropertyType == typeof(string))
            {
                formattedValue =
                    string.IsNullOrEmpty(resetValueParameterExpression)
                    ? "(string)attributeValue"
                    : string.Format(CultureInfo.InvariantCulture, "(string)attributeValue ?? {0}", resetValueParameterExpression);
            }
            else
            {
                // TODO: Error?
                Console.WriteLine($"WARNING: Couldn't generate property set for {prop.DeclaringType.Name}.{prop.Name}");
            }

            return $@"                case nameof(XF.{prop.DeclaringType.Name}.{GetIdentifierName(prop.Name)}):
                    {prop.DeclaringType.Name}Control.{GetIdentifierName(prop.Name)} = {formattedValue};
                    break;
";
        }

        private static string GetValueExpression(object declaredDefaultValue, List<UsingStatement> usings)
        {
            if (declaredDefaultValue is null)
            {
                throw new ArgumentNullException(nameof(declaredDefaultValue));
            }

            return declaredDefaultValue switch
            {
                bool boolValue => boolValue ? "true" : "false",
                int intValue => GetIntValueExpression(intValue),
                float floatValue => floatValue.ToString("F", CultureInfo.InvariantCulture) + "f", // "Fixed-Point": https://docs.microsoft.com/en-us/dotnet/standard/base-types/standard-numeric-format-strings#the-fixed-point-f-format-specifier
                double doubleValue => doubleValue.ToString("F", CultureInfo.InvariantCulture), // "Fixed-Point": https://docs.microsoft.com/en-us/dotnet/standard/base-types/standard-numeric-format-strings#the-fixed-point-f-format-specifier
                Enum enumValue => GetTypeNameAndAddNamespace(enumValue.GetType(), usings) + "." + Enum.GetName(enumValue.GetType(), declaredDefaultValue),
                XF.LayoutOptions layoutOptionsValue => GetLayoutOptionsValueExpression(layoutOptionsValue),
                string stringValue => $@"""{stringValue}""",
                // TODO: More types here
                _ => null,
            };
        }

        private static string GetLayoutOptionsValueExpression(XF.LayoutOptions layoutOptionsValue)
        {
            var expandSuffix = layoutOptionsValue.Expands ? "AndExpand" : string.Empty;
            return $"XF.LayoutOptions.{layoutOptionsValue.Alignment}{expandSuffix}";
        }

        private static string GetIntValueExpression(int intValue)
        {
            return intValue switch
            {
                int.MinValue => "int.MinValue",
                int.MaxValue => "int.MaxValue",
                _ => intValue.ToString(CultureInfo.InvariantCulture),
            };
        }

        private static object GetDefaultValueForType(Type propertyType)
        {
            if (propertyType.IsValueType)
            {
                return Activator.CreateInstance(propertyType);
            }
            return null;
        }

        private static XF.BindableProperty GetBindablePropertyForProp(PropertyInfo prop)
        {
            var bindablePropertyField = prop.DeclaringType.GetField(prop.Name + "Property");
            if (bindablePropertyField == null)
            {
                return null;
            }
            return (XF.BindableProperty)bindablePropertyField.GetValue(null);
        }

        private static readonly Dictionary<Type, string> TypeToAttributeHelperSetter = new Dictionary<Type, string>
        {
            { typeof(XF.Color), "AttributeHelper.StringToColor((string)attributeValue{0})" },
            { typeof(XF.CornerRadius), "AttributeHelper.StringToCornerRadius(attributeValue{0})" },
            { typeof(XF.ImageSource), "AttributeHelper.StringToImageSource(attributeValue{0})" },
            { typeof(XF.LayoutOptions), "AttributeHelper.StringToLayoutOptions(attributeValue{0})" },
            { typeof(XF.Thickness), "AttributeHelper.StringToThickness(attributeValue{0})" },
            { typeof(bool), "AttributeHelper.GetBool(attributeValue{0})" },
            { typeof(double), "AttributeHelper.StringToDouble((string)attributeValue{0})" },
            { typeof(float), "AttributeHelper.StringToSingle((string)attributeValue{0})" },
            { typeof(int), "AttributeHelper.GetInt(attributeValue{0})" },
            { typeof(IList<string>), "AttributeHelper.GetStringList(attributeValue)" },
        };

        private static IEnumerable<PropertyInfo> GetPropertiesToGenerate(Type componentType)
        {
            var allPublicProperties = componentType.GetProperties();

            return
                allPublicProperties
                    .Where(HasPublicGetAndSet)
                    .Where(prop => prop.DeclaringType == componentType)
                    .Where(prop => !DisallowedComponentPropertyTypes.Contains(prop.PropertyType))
                    .Where(IsPropertyBrowsable)
                    .OrderBy(prop => prop.Name, StringComparer.OrdinalIgnoreCase)
                    .ToList();
        }

        private static bool HasPublicGetAndSet(PropertyInfo propInfo)
        {
            return propInfo.GetGetMethod() != null && propInfo.GetSetMethod() != null;
        }

        private static bool IsPropertyBrowsable(PropertyInfo propInfo)
        {
            // [EditorBrowsable(EditorBrowsableState.Never)]
            var attr = (EditorBrowsableAttribute)Attribute.GetCustomAttribute(propInfo, typeof(EditorBrowsableAttribute));
            return (attr == null) || (attr.State != EditorBrowsableState.Never);
        }

        private static string GetIdentifierName(string possibleIdentifier)
        {
            return ReservedKeywords.Contains(possibleIdentifier, StringComparer.Ordinal)
                ? $"@{possibleIdentifier}"
                : possibleIdentifier;
        }

        private static readonly List<string> ReservedKeywords = new List<string>
            { "class", };
    }
}
