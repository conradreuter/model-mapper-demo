using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Xml;

namespace ModelMapperDemo.Utility
{
    /// <summary>
    /// Reads XML documentation files.
    /// </summary>
    public static class XmlDoc
    {
        private static readonly IDictionary<Assembly, XmlDocument> _xmlsByAssembly
            = new Dictionary<Assembly, XmlDocument>();

        /// <summary>
        /// Read the XML summary of a type.
        /// </summary>
        public static string ReadSummary(Type type) =>
            ReadSummary(
                type.Assembly,
                $"T:{type.FullName}");

        /// <summary>
        /// Read the XML summary of a field.
        /// </summary>
        public static string ReadSummary(FieldInfo field) =>
            ReadSummary(
                field.DeclaringType.Assembly,
                $"F:{field.DeclaringType.FullName}.{field.Name}");

        private static string ReadSummary(Assembly assembly, string memberName)
        {
            memberName = memberName.Replace('+', '.');
            if (!_xmlsByAssembly.TryGetValue(assembly, out var xml))
            {
                var xmlFile = Path.Combine(
                    Path.GetDirectoryName(assembly.Location),
                    $"{assembly.GetName().Name}.xml");
                xml = new XmlDocument();
                xml.Load(xmlFile);
                _xmlsByAssembly.Add(assembly, xml);
            }
            var xpath = $"/doc/members/member[@name='{memberName}']/summary";
            var node = xml.SelectSingleNode(xpath);
            return node?.InnerText?.Trim();
        }
    }
}
