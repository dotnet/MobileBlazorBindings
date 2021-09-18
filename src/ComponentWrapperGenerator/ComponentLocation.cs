// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using System.Reflection;

namespace ComponentWrapperGenerator
{
    public class ComponentLocation
    {
        public Assembly Assembly { get; }
        public string NamespaceName { get; }
        public string XmlDocFilename { get; }

        public ComponentLocation(Assembly assembly, string namespaceName, string xmlDocFilename)
        {
            Assembly = assembly;
            NamespaceName = namespaceName;
            XmlDocFilename = xmlDocFilename;
        }
    }
}
