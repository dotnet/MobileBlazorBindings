using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Input;
using XF = Xamarin.Forms;

namespace ComponentWrapperGenerator
{
    public class ComponentWrapperGenerator
    {
        public ComponentWrapperGenerator(GeneratorSettings settings)
        {
            Settings = settings ?? throw new ArgumentNullException(nameof(settings));
        }

        private GeneratorSettings Settings { get; }

        public void GenerateComponentWrapper(Type typeToGenerate)
        {
            typeToGenerate = typeToGenerate ?? throw new ArgumentNullException(nameof(typeToGenerate));

            var propertiesToGenerate = GetPropertiesToGenerate(typeToGenerate);

            GenerateComponentFile(typeToGenerate, propertiesToGenerate);
            GenerateHandlerFile(typeToGenerate, propertiesToGenerate);
        }

        private void GenerateComponentFile(Type typeToGenerate, IEnumerable<PropertyInfo> propertiesToGenerate)
        {
            var fileName = $@"{typeToGenerate.Name}.cs";

            Console.WriteLine($"Generating component for type '{typeToGenerate.FullName}' into file '{fileName}'.");

            var componentName = typeToGenerate.Name;
            var componentHandlerName = $"{componentName}Handler";
            var componentBaseName = typeToGenerate.BaseType.Name;

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
                propertyDeclarationBuilder.Append(GetPropertyDeclaration(prop, usings));
            }
            var propertyDeclarations = propertyDeclarationBuilder.ToString();

            // events
            // TODO: This
            var eventDeclarations = "";

            var propertyAttributeBuilder = new StringBuilder();
            if (propertiesToGenerate.Any())
            {
                propertyAttributeBuilder.AppendLine();
            }
            foreach (var prop in propertiesToGenerate)
            {
                propertyAttributeBuilder.Append(GetPropertyRenderAttribute(prop));
            }
            var propertyAttributes = propertyAttributeBuilder.ToString();
            var eventHandlerAttributes = "";

            // event handlers
            // TODO: This
            var eventHandlerMethods = "";

            var usingsText = string.Join(
                Environment.NewLine,
                usings
                    .Distinct()
                    .Where(u => u.Namespace != Settings.RootNamespace)
                    .OrderBy(u => u.ComparableString)
                    .Select(u => u.UsingText));

            var outputBuilder = new StringBuilder();
            outputBuilder.Append($@"{headerText}
{usingsText}

namespace {Settings.RootNamespace}
{{
    public class {componentName} : {componentBaseName}
    {{
        static {componentName}()
        {{
            ElementHandlerRegistry.RegisterElementHandler<{componentName}>(
                renderer => new {componentHandlerName}(renderer, new XF.{componentName}()));
        }}
{propertyDeclarations}{eventDeclarations}
        public new XF.{componentName} NativeControl => (({componentHandlerName})ElementHandler).{componentName}Control;

        protected override void RenderAttributes(AttributesBuilder builder)
        {{
            base.RenderAttributes(builder);

{propertyAttributes}{eventHandlerAttributes}
        }}{eventHandlerMethods}
    }}
}}
");

            File.WriteAllText(fileName, outputBuilder.ToString());
        }

        private static readonly List<Type> DisallowedComponentPropertyTypes = new List<Type>
        {
            typeof(ICommand),
            typeof(object),
        };

        private static string GetPropertyDeclaration(PropertyInfo prop, IList<UsingStatement> usings)
        {
            var propertyType = prop.PropertyType;
            var propertyTypeName = GetTypeNameAndAddNamespace(propertyType, usings);
            if (propertyType.IsValueType)
            {
                propertyTypeName += "?";
            }
            return $@"        [Parameter] public {propertyTypeName} {prop.Name} {{ get; set; }}
";
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
            typeName = namespaceAlias + type.Name;
            return typeName;
        }

        private static readonly Dictionary<Type, Func<string, string>> TypeToAttributeHelperGetter = new Dictionary<Type, Func<string, string>>
        {
            { typeof(XF.Color), propValue => $"AttributeHelper.ColorToString({propValue})" },
            { typeof(XF.CornerRadius), propValue => $"AttributeHelper.CornerRadiusToString({propValue})" },
            { typeof(XF.LayoutOptions), propValue => $"AttributeHelper.LayoutOptionsToString({propValue})" },
            { typeof(XF.Thickness), propValue => $"AttributeHelper.ThicknessToString({propValue})" },
            { typeof(double), propValue => $"AttributeHelper.DoubleToString({propValue})" },
            { typeof(float), propValue => $"AttributeHelper.SingleToString({propValue})" },
        };

        private static string GetPropertyRenderAttribute(PropertyInfo prop)
        {
            var propValue = prop.PropertyType.IsValueType ? $"{prop.Name}.Value" : prop.Name;
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
            }

            return $@"            if ({prop.Name} != null)
            {{
                builder.AddAttribute(nameof({prop.Name}), {formattedValue});
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

        private void GenerateHandlerFile(Type typeToGenerate, IEnumerable<PropertyInfo> propertiesToGenerate)
        {
            var fileName = $@"Handlers\{typeToGenerate.Name}Handler.cs";
            Directory.CreateDirectory("Handlers");

            Console.WriteLine($"Generating component handler for type '{typeToGenerate.FullName}' into file '{fileName}'.");

            var componentName = typeToGenerate.Name;
            var componentVarName = char.ToLowerInvariant(componentName[0]) + componentName.Substring(1);
            var componentHandlerName = $"{componentName}Handler";
            var componentBaseName = typeToGenerate.BaseType.Name;
            var componentHandlerBaseName = $"{componentBaseName}Handler";

            // header
            var headerText = Settings.FileHeader;

            // usings
            var usings = new List<UsingStatement>
            {
                new UsingStatement { Namespace = "Microsoft.AspNetCore.Components" },
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

            var outputBuilder = new StringBuilder();
            outputBuilder.Append($@"{headerText}
{usingsText}

namespace {Settings.RootNamespace}.Handlers
{{
    public class {componentHandlerName} : {componentHandlerBaseName}
    {{
        public {componentName}Handler(NativeComponentRenderer renderer, XF.{componentName} {componentVarName}Control) : base(renderer, {componentVarName}Control)
        {{
            {componentName}Control = {componentVarName}Control ?? throw new ArgumentNullException(nameof({componentVarName}Control));
        }}

        public XF.{componentName} {componentName}Control {{ get; }}

        public override void ApplyAttribute(ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName)
        {{
            switch (attributeName)
            {{
{propertySetters}                default:
                    base.ApplyAttribute(attributeEventHandlerId, attributeName, attributeValue, attributeEventUpdatesAttributeName);
                    break;
            }}
        }}
    }}
}}
");

            File.WriteAllText(fileName, outputBuilder.ToString());
        }

        private static string GetPropertySetAttribute(PropertyInfo prop, List<UsingStatement> usings)
        {
            var formattedValue = string.Empty;
            if (TypeToAttributeHelperSetter.TryGetValue(prop.PropertyType, out var propValue))
            {
                formattedValue = propValue;
            }
            else if (prop.PropertyType.IsEnum)
            {
                var castTypeName = GetTypeNameAndAddNamespace(prop.PropertyType, usings);
                formattedValue = $"({castTypeName})AttributeHelper.GetInt(attributeValue)";
            }
            else
            {
                // TODO: Error?
            }

            return $@"                case nameof(XF.{prop.DeclaringType.Name}.{prop.Name}):
                    {prop.DeclaringType.Name}Control.{prop.Name} = {formattedValue};
                    break;
";
        }

        private static readonly Dictionary<Type, string> TypeToAttributeHelperSetter = new Dictionary<Type, string>
        {
            { typeof(XF.Color), "AttributeHelper.StringToColor((string)attributeValue)" },
            { typeof(XF.CornerRadius), "AttributeHelper.StringToCornerRadius(attributeValue)" },
            { typeof(XF.LayoutOptions), "AttributeHelper.StringToLayoutOptions(attributeValue)" },
            { typeof(XF.Thickness), "AttributeHelper.StringToThickness(attributeValue)" },
            { typeof(bool), "AttributeHelper.GetBool(attributeValue)" },
            { typeof(double), "AttributeHelper.StringToDouble((string)attributeValue)" },
            { typeof(float), "AttributeHelper.SingleToString((string)attributeValue)" },
            { typeof(int), "AttributeHelper.GetInt(attributeValue)" },
            { typeof(string), "(string)attributeValue" },
        };

        private static IEnumerable<PropertyInfo> GetPropertiesToGenerate(Type componentType)
        {
            var allPublicProperties = componentType.GetProperties();

            return
                allPublicProperties
                    .Where(prop => prop.CanRead && prop.CanWrite)
                    .Where(prop => prop.DeclaringType == componentType)
                    .Where(prop => !DisallowedComponentPropertyTypes.Contains(prop.PropertyType))
                    .OrderBy(prop => prop.Name, StringComparer.OrdinalIgnoreCase)
                    .ToList();
        }
    }
}
