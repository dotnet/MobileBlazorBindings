// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.CodeAnalysis;
using Microsoft.MobileBlazorBindings.ComponentGenerator.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Xml;

namespace Microsoft.MobileBlazorBindings.ComponentGenerator
{
    public class ComponentWrapperGenerator
    {
        private GeneratorSettings Settings { get; }
        public ComponentWrapperGenerator(GeneratorSettings settings)
        {
            Settings = settings ?? throw new ArgumentNullException(nameof(settings));
        }

        public (string HintName, string Source)[] GenerateComponentWrapper(INamedTypeSymbol typeToGenerate)
        {
            typeToGenerate = typeToGenerate ?? throw new ArgumentNullException(nameof(typeToGenerate));

            var propertiesToGenerate = GetPropertiesToGenerate(typeToGenerate);

            var componentSource = GenerateComponentFile(typeToGenerate, propertiesToGenerate);
            var handlerSource = GenerateHandlerFile(typeToGenerate, propertiesToGenerate);

            return new[] { componentSource, handlerSource };
        }

        private (string HintName, string Source) GenerateComponentFile(INamedTypeSymbol typeToGenerate, IEnumerable<IPropertySymbol> propertiesToGenerate)
        {
            var hintName = typeToGenerate.Name;

            Console.WriteLine($"Generating component for type '{typeToGenerate.MetadataName}' into file '{hintName}'.");

            var componentName = typeToGenerate.Name;
            var componentHandlerName = $"{componentName}Handler";
            var componentBaseName = GetBaseTypeOfInterest(typeToGenerate).Name;

            // header
            var headerText = Settings.FileHeader;

            var typeNamespace = typeToGenerate.ContainingNamespace.GetFullName();
            var typeNamespaceAlias = GetNamespaceAlias(typeToGenerate.ContainingNamespace);

            // usings
            var usings = new List<UsingStatement>
            {
                new UsingStatement { Namespace = "Microsoft.AspNetCore.Components", IsUsed = true, },
                new UsingStatement { Namespace = "Microsoft.MobileBlazorBindings.Core", IsUsed = true, },
                new UsingStatement { Namespace = $"{Settings.RootNamespace}.Handlers", IsUsed = true, },
                new UsingStatement { Namespace = "System.Threading.Tasks", IsUsed = true, },
                new UsingStatement { Namespace = "Xamarin.Forms", Alias = "XF" }
            };

            if (typeNamespace != "Xamarin.Forms")
            {
                usings.Add(new UsingStatement { Namespace = typeNamespace, Alias = typeNamespaceAlias });
            }

            if (Settings.RootNamespace != "Microsoft.MobileBlazorBindings.Elements")
            {
                usings.Add(new UsingStatement { Namespace = "Microsoft.MobileBlazorBindings.Elements", IsUsed = true });
            }

            var componentNamespacePrefix = GetNamespacePrefix(typeToGenerate, usings);

            // props
            var propertyDeclarationBuilder = new StringBuilder();
            if (propertiesToGenerate.Any())
            {
                propertyDeclarationBuilder.AppendLine();
            }
            foreach (var prop in propertiesToGenerate)
            {
                propertyDeclarationBuilder.Append(GetPropertyDeclaration(prop, usings));
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
                    .Where(u => u.IsUsed)
                    .OrderBy(u => u.ComparableString)
                    .Select(u => u.UsingText));

            var isComponentAbstract = typeToGenerate.IsAbstract;
            var classModifiers = string.Empty;
            if (isComponentAbstract)
            {
                classModifiers += "abstract ";
            }
            var componentHasPublicParameterlessConstructor = typeToGenerate.Constructors
                    .Any(ctor => ctor.DeclaredAccessibility == Accessibility.Public && !ctor.Parameters.Any());

            var staticConstructor = string.Empty;
            if (!isComponentAbstract && componentHasPublicParameterlessConstructor)
            {
                staticConstructor = $@"        static {componentName}()
        {{
            ElementHandlerRegistry.RegisterElementHandler<{componentName}>(
                renderer => new {componentHandlerName}(renderer, new {componentNamespacePrefix}{componentName}()));
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
        public new {componentNamespacePrefix}{componentName} NativeControl => (({componentHandlerName})ElementHandler).{componentName}Control;

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

            return (hintName, outputBuilder.ToString());
        }

        private static string GetNamespacePrefix(ITypeSymbol type, List<UsingStatement> usings)
        {
            // Check if there's a 'using' already. If so, check if it has an alias. If not, add a new 'using'.
            var namespaceAlias = string.Empty;

            var namespaceName = type.ContainingNamespace.GetFullName();

            var existingUsing = usings.FirstOrDefault(u => u.Namespace == namespaceName);
            if (existingUsing == null)
            {
                usings.Add(new UsingStatement { Namespace = type.ContainingNamespace.GetFullName(), IsUsed = true, });
                return string.Empty;
            }
            else
            {
                existingUsing.IsUsed = true;
                if (existingUsing.Alias != null)
                {
                    return existingUsing.Alias + ".";
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        private static readonly List<string> DisallowedComponentPropertyTypes = new List<string>
        {
            "Xamarin.Forms.Brush",
            "Xamarin.Forms.Button.ButtonContentLayout", // TODO: This is temporary; should be possible to add support later
            "Xamarin.Forms.ColumnDefinitionCollection",
            "Xamarin.Forms.ControlTemplate",
            "Xamarin.Forms.DataTemplate",
            "Xamarin.Forms.Element",
            "Xamarin.Forms.Font", // TODO: This is temporary; should be possible to add support later
            "Xamarin.Forms.FormattedString",
            "Xamarin.Forms.Shapes.Geometry",
            "System.Windows.Input.ICommand",
            "System.Object",
            "Xamarin.Forms.Page",
            "Xamarin.Forms.ResourceDictionary",
            "Xamarin.Forms.RowDefinitionCollection",
            "Xamarin.Forms.ShellContent",
            "Xamarin.Forms.ShellItem",
            "Xamarin.Forms.ShellSection",
            "Xamarin.Forms.Style", // TODO: This is temporary; should be possible to add support later
            "Xamarin.Forms.IVisual",
            "Xamarin.Forms.View",
        };

        private static string GetPropertyDeclaration(IPropertySymbol prop, IList<UsingStatement> usings)
        {
            var propertyType = prop.Type;
            string propertyTypeName;

            if (propertyType.GetFullName() == "System.Collections.Generic.IList<System.String>")
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

            var xmlDocContents = GetXmlDocContents(prop, indent);

            return $@"{xmlDocContents}{indent}[Parameter] public {propertyTypeName} {GetIdentifierName(prop.Name)} {{ get; set; }}
";
        }
        private static string GetXmlDocText(XmlElement xmlDocElement)
        {
            var allText = xmlDocElement?.InnerXml;
            allText = allText?.Replace("To be added.", string.Empty);
            if (string.IsNullOrWhiteSpace(allText))
            {
                return null;
            }
            return allText;
        }

        private static string GetXmlDocContents(IPropertySymbol prop, string indent)
        {
            var xmlDocString = prop.GetDocumentationCommentXml();

            if (string.IsNullOrEmpty(xmlDocString))
            {
                return null;
            }

            var xmlDoc = new XmlDocument();
            // Returned XML doc string has no root element, which does not allow to parse it.
            xmlDoc.LoadXml($"<member>{xmlDocString}</member>");
            var xmlDocNode = xmlDoc.FirstChild;

            var xmlDocContents = string.Empty;
            // Format of XML docs we're looking for in a given property:
            // <member name="P:Xamarin.Forms.ActivityIndicator.Color">
            //     <summary>Gets or sets the <see cref="T:Xamarin.Forms.Color" /> of the ActivityIndicator. This is a bindable property.</summary>
            //     <value>A <see cref="T:Xamarin.Forms.Color" /> used to display the ActivityIndicator. Default is <see cref="P:Xamarin.Forms.Color.Default" />.</value>
            //     <remarks />
            // </member>

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
            return xmlDocContents;
        }

        private static string GetTypeNameAndAddNamespace(ITypeSymbol type, IList<UsingStatement> usings)
        {
            var typeName = GetCSharpType(type);
            if (typeName != null)
            {
                return typeName;
            }

            // Check if there's a 'using' already. If so, check if it has an alias. If not, add a new 'using'.
            var namespaceAlias = string.Empty;

            var containingNamespaceName = type.ContainingNamespace.GetFullName();

            var existingUsing = usings.FirstOrDefault(u => u.Namespace == containingNamespaceName);
            if (existingUsing == null)
            {
                usings.Add(new UsingStatement { Namespace = containingNamespaceName, IsUsed = true, });
            }
            else
            {
                existingUsing.IsUsed = true;
                if (existingUsing.Alias != null)
                {
                    namespaceAlias = existingUsing.Alias + ".";
                }
            }
            typeName = namespaceAlias + FormatTypeName(type, usings);
            return typeName;
        }

        private static string FormatTypeName(ITypeSymbol type, IList<UsingStatement> usings)
        {
            var namedType = type as INamedTypeSymbol;

            if (namedType == null || !namedType.IsGenericType)
            {
                return type.Name;
            }

            var typeNameBuilder = new StringBuilder();
            typeNameBuilder.Append(type.Name);
            typeNameBuilder.Append('<');
            var genericArgs = namedType.TypeArguments;
            for (var i = 0; i < genericArgs.Length; i++)
            {
                if (i > 0)
                {
                    typeNameBuilder.Append(", ");
                }
                typeNameBuilder.Append(GetTypeNameAndAddNamespace(genericArgs[i], usings));

            }
            typeNameBuilder.Append('>');
            return typeNameBuilder.ToString();
        }

        private static readonly Dictionary<string, Func<string, string>> TypeToAttributeHelperGetter = new Dictionary<string, Func<string, string>>
        {
            { "Xamarin.Forms.Color", propValue => $"AttributeHelper.ColorToString({propValue})" },
            { "Xamarin.Forms.CornerRadius", propValue => $"AttributeHelper.CornerRadiusToString({propValue})" },
            { "Xamarin.Forms.GridLength", propValue => $"AttributeHelper.GridLengthToString({propValue})" },
            { "Xamarin.Forms.ImageSource", propValue => $"AttributeHelper.ObjectToDelegate({propValue})" },
            { "Xamarin.Forms.Keyboard", propValue => $"AttributeHelper.ObjectToDelegate({propValue})" },
            { "Xamarin.Forms.LayoutOptions", propValue => $"AttributeHelper.LayoutOptionsToString({propValue})" },
            { "Xamarin.Forms.Thickness", propValue => $"AttributeHelper.ThicknessToString({propValue})" },
            { "System.DateTime", propValue => $"AttributeHelper.DateTimeToString({propValue})" },
            { "System.TimeSpan", propValue => $"AttributeHelper.TimeSpanToString({propValue})" },
            { "System.Boolean", propValue => $"{propValue}" },
            { "System.Double", propValue => $"AttributeHelper.DoubleToString({propValue})" },
            { "System.Single", propValue => $"AttributeHelper.SingleToString({propValue})" },
            { "System.Int32", propValue => $"{propValue}" },
            { "System.String", propValue => $"{propValue}" },
            { "System.Collections.Generic.IList<System.String>", propValue => $"{propValue}" },
        };

        private static string GetPropertyRenderAttribute(IPropertySymbol prop)
        {
            var propValue = prop.Type.IsValueType ? $"{GetIdentifierName(prop.Name)}.Value" : GetIdentifierName(prop.Name);
            string formattedValue;

            if (TypeToAttributeHelperGetter.TryGetValue(prop.Type.GetFullName(), out var formattingFunc))
            {
                formattedValue = formattingFunc(propValue);
            }
            else if (prop.Type.TypeKind == TypeKind.Enum)
            {
                formattedValue = $"(int){propValue}";
            }
            else
            {
                formattedValue = $"AttributeHelper.ObjectToDelegate({propValue})";
            }

            return $@"            if ({GetIdentifierName(prop.Name)} != null)
            {{
                builder.AddAttribute(nameof({GetIdentifierName(prop.Name)}), {formattedValue});
            }}
";
        }

        private static readonly Dictionary<SpecialType, string> TypeToCSharpName = new Dictionary<SpecialType, string>
        {
            { SpecialType.System_Boolean, "bool" },
            { SpecialType.System_Byte, "byte" },
            { SpecialType.System_SByte, "sbyte" },
            { SpecialType.System_Char, "char" },
            { SpecialType.System_Decimal, "decimal" },
            { SpecialType.System_Double, "double" },
            { SpecialType.System_Single, "float" },
            { SpecialType.System_Int32, "int" },
            { SpecialType.System_UInt32, "uint" },
            { SpecialType.System_Int64, "long" },
            { SpecialType.System_UInt64, "ulong" },
            { SpecialType.System_Object, "object" },
            { SpecialType.System_Int16, "short" },
            { SpecialType.System_UInt16, "ushort" },
            { SpecialType.System_String, "string" },
        };

        private static string GetCSharpType(ITypeSymbol propertyType)
        {
            return TypeToCSharpName.TryGetValue(propertyType.SpecialType, out var typeName) ? typeName : null;
        }

        /// <summary>
        /// Finds the next non-generic base type of the specified type. This matches the Mobile Blazor Bindings
        /// model where there is no need to represent the intermediate generic base classes because they are
        /// generally only containers and have no API functionality that needs to be generated.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private static INamedTypeSymbol GetBaseTypeOfInterest(INamedTypeSymbol type)
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

        private (string HintName, string Source) GenerateHandlerFile(INamedTypeSymbol typeToGenerate, IEnumerable<IPropertySymbol> propertiesToGenerate)
        {
            var hintName = $"{typeToGenerate.Name}Handler";

            Console.WriteLine($"Generating component handler for type '{typeToGenerate.Name}' into file '{hintName}'.");

            var componentName = typeToGenerate.Name;
            var componentVarName = char.ToLowerInvariant(componentName[0]) + componentName.Substring(1);
            var componentHandlerName = $"{componentName}Handler";
            var componentBaseName = GetBaseTypeOfInterest(typeToGenerate).Name;
            var componentHandlerBaseName = $"{componentBaseName}Handler";
            var componentHandlerNamespace = $"{Settings.RootNamespace}.Handlers";

            // header
            var headerText = Settings.FileHeader;

            var typeNamespace = typeToGenerate.ContainingNamespace.GetFullName();
            var typeNamespaceAlias = GetNamespaceAlias(typeToGenerate.ContainingNamespace);

            // usings
            var usings = new List<UsingStatement>
            {
                //new UsingStatement { Namespace = "Microsoft.AspNetCore.Components", IsUsed = true, }, // Typically needed only when there are event handlers for the EventArgs types
                new UsingStatement { Namespace = "Microsoft.MobileBlazorBindings.Core", IsUsed = true, },
                new UsingStatement { Namespace = "Microsoft.MobileBlazorBindings.Elements", IsUsed = true, },
                new UsingStatement { Namespace = "System", IsUsed = true, },
                new UsingStatement { Namespace = "Xamarin.Forms", Alias = "XF" },
            };

            if(typeNamespace != "Xamarin.Forms")
            {
                usings.Add(new UsingStatement { Namespace = typeNamespace, Alias = typeNamespaceAlias });
            }

            if (componentHandlerNamespace != "Microsoft.MobileBlazorBindings.Elements.Handlers")
            {
                usings.Add(new UsingStatement { Namespace = "Microsoft.MobileBlazorBindings.Elements.Handlers", IsUsed = true });
            }

            var componentNamespacePrefix = GetNamespacePrefix(typeToGenerate, usings);

            // props
            var propsDefaultValues = GetDefaultPropertyValues(typeToGenerate, propertiesToGenerate, usings);

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
                    .Where(u => u.IsUsed)
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

namespace {componentHandlerNamespace}
{{
    public {classModifiers}partial class {componentHandlerName} : {componentHandlerBaseName}
    {{
{propsDefaultValues}
        public {componentName}Handler(NativeComponentRenderer renderer, {componentNamespacePrefix}{componentName} {componentVarName}Control) : base(renderer, {componentVarName}Control)
        {{
            {componentName}Control = {componentVarName}Control ?? throw new ArgumentNullException(nameof({componentVarName}Control));

            Initialize(renderer);
        }}

        partial void Initialize(NativeComponentRenderer renderer);

        public {componentNamespacePrefix}{componentName} {componentName}Control {{ get; }}
{applyAttributesMethod}    }}
}}
");

            return (hintName, outputBuilder.ToString());
        }

        private static string GetPropertySetAttribute(IPropertySymbol prop, List<UsingStatement> usings)
        {
            // Handle null values by resetting to default value
            var resetValueParameterExpression = BindablePropertyExistsForProp(prop)
                ? $"{prop.Name}DefaultValue"
                : string.Empty;

            var formattedValue = string.Empty;
            if (TypeToAttributeHelperSetter.TryGetValue(prop.Type.GetFullName(), out var propValueFormat))
            {
                var resetValueParameterExpressionAsExtraParameter = string.Empty;
                if (!string.IsNullOrEmpty(resetValueParameterExpression))
                {
                    resetValueParameterExpressionAsExtraParameter = ", " + resetValueParameterExpression;
                }
                formattedValue = string.Format(CultureInfo.InvariantCulture, propValueFormat, resetValueParameterExpressionAsExtraParameter);
            }
            else if (prop.Type.TypeKind == TypeKind.Enum)
            {
                var resetValueParameterExpressionAsExtraParameter = string.Empty;
                if (!string.IsNullOrEmpty(resetValueParameterExpression))
                {
                    resetValueParameterExpressionAsExtraParameter = ", (int)" + resetValueParameterExpression;
                }
                var castTypeName = GetTypeNameAndAddNamespace(prop.Type, usings);
                formattedValue = $"({castTypeName})AttributeHelper.GetInt(attributeValue{resetValueParameterExpressionAsExtraParameter})";
            }
            else if (prop.Type.SpecialType == SpecialType.System_String)
            {
                formattedValue =
                    string.IsNullOrEmpty(resetValueParameterExpression)
                    ? "(string)attributeValue"
                    : string.Format(CultureInfo.InvariantCulture, "(string)attributeValue ?? {0}", resetValueParameterExpression);
            }
            else
            {
                formattedValue = $"AttributeHelper.DelegateToObject<{GetTypeNameAndAddNamespace(prop.Type, usings)}>(attributeValue)";
            }

            return $@"                case nameof({GetNamespacePrefix(prop.ContainingType, usings)}{prop.ContainingType.Name}.{GetIdentifierName(prop.Name)}):
                    {prop.ContainingType.Name}Control.{GetIdentifierName(prop.Name)} = {formattedValue};
                    break;
";
        }

        private static string GetDefaultPropertyValues(ITypeSymbol type, IEnumerable<IPropertySymbol> properties, IList<UsingStatement> usings)
        {
            var bindableProps = properties.Where(BindablePropertyExistsForProp).ToList();

            if (bindableProps.Count == 0)
            {
                return string.Empty;
            }

            var stringBuilder = new StringBuilder();
            var typeName = GetTypeNameAndAddNamespace(type, usings);

            foreach (var prop in bindableProps)
            {
                var propTypeName = GetTypeNameAndAddNamespace(prop.Type, usings);
                var propertyDefaultValue = $"{typeName}.{prop.Name}Property.DefaultValue";

                stringBuilder.AppendLine($"        private static readonly {propTypeName} {prop.Name}DefaultValue = " +
                    $"{propertyDefaultValue} is {propTypeName} value ? value : default;");
            }

            return stringBuilder.ToString();
        }

        private static bool BindablePropertyExistsForProp(IPropertySymbol prop)
        {
            return prop.ContainingType.GetMembers(prop.Name + "Property").Length > 0;
        }

        private static readonly Dictionary<string, string> TypeToAttributeHelperSetter = new Dictionary<string, string>
        {
            { "Xamarin.Forms.Color", "AttributeHelper.StringToColor((string)attributeValue{0})" },
            { "Xamarin.Forms.CornerRadius", "AttributeHelper.StringToCornerRadius(attributeValue{0})" },
            { "Xamarin.Forms.GridLength", "AttributeHelper.StringToGridLength(attributeValue{0})" },
            { "Xamarin.Forms.ImageSource", "AttributeHelper.DelegateToObject<XF.ImageSource>(attributeValue{0})" },
            { "Xamarin.Forms.Keyboard", "AttributeHelper.DelegateToObject<XF.Keyboard>(attributeValue{0})" },
            { "Xamarin.Forms.LayoutOptions", "AttributeHelper.StringToLayoutOptions(attributeValue{0})" },
            { "Xamarin.Forms.Thickness", "AttributeHelper.StringToThickness(attributeValue{0})" },
            { "System.DateTime", "AttributeHelper.StringToDateTime(attributeValue{0})" },
            { "System.TimeSpan", "AttributeHelper.StringToTimeSpan(attributeValue{0})" },
            { "System.Boolean", "AttributeHelper.GetBool(attributeValue{0})" },
            { "System.Double", "AttributeHelper.StringToDouble((string)attributeValue{0})" },
            { "System.Single", "AttributeHelper.StringToSingle((string)attributeValue{0})" },
            { "System.Int32", "AttributeHelper.GetInt(attributeValue{0})" },
            { "System.Collections.Generic.IList<System.String>", "AttributeHelper.GetStringList(attributeValue)" },
        };

        private static IEnumerable<IPropertySymbol> GetPropertiesToGenerate(ITypeSymbol componentType)
        {
            var allPublicProperties = componentType.GetMembers()
                .OfType<IPropertySymbol>()
                .Where(p => p.DeclaredAccessibility == Accessibility.Public);

            return allPublicProperties
                .Where(HasPublicGetAndSet)
                .Where(IsPropertyBrowsable)
                .Where(prop => !DisallowedComponentPropertyTypes.Contains(prop.Type.GetFullName()))
                .OrderBy(prop => prop.Name, StringComparer.OrdinalIgnoreCase)
                .ToList();
        }

        private static bool HasPublicGetAndSet(IPropertySymbol propInfo)
        {
            return propInfo.GetMethod?.DeclaredAccessibility == Accessibility.Public
                && propInfo.SetMethod?.DeclaredAccessibility == Accessibility.Public;
        }

        private static bool IsPropertyBrowsable(IPropertySymbol propInfo)
        {
            // [EditorBrowsable(EditorBrowsableState.Never)]
            return !propInfo.GetAttributes().Any(a => a.AttributeClass?.Name == nameof(EditorBrowsableAttribute)
                && a.ConstructorArguments.Length == 1
                && a.ConstructorArguments[0].Value?.Equals((int)EditorBrowsableState.Never) == true);
        }

        private static string GetIdentifierName(string possibleIdentifier)
        {
            return ReservedKeywords.Contains(possibleIdentifier, StringComparer.Ordinal)
                ? $"@{possibleIdentifier}"
                : possibleIdentifier;
        }

        private static string GetNamespaceAlias(INamespaceSymbol namespaceSymbol)
        {
            var alias = "";
            while (!namespaceSymbol.IsGlobalNamespace)
            {
                alias = namespaceSymbol.Name[0] + alias;
                namespaceSymbol = namespaceSymbol.ContainingNamespace;
            }

            return alias;
        }

        private static readonly List<string> ReservedKeywords = new List<string>
            { "class", };
    }
}
