using System;

namespace UmlFromCode.Model
{
    [AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Module, AllowMultiple = true)]
    public class DiagramAttribute : UmlAttribute
    {
        /// <summary>
        /// Which diagrams this attribute are related to.
        /// </summary>
        public string[] Diagrams { get; set; }

        /// <summary>
        /// Which namespaces this attribute are related to.
        /// </summary>
        public string[] Namespaces { get; set; }

        /// <summary>
        /// Default include for namespace members.
        /// </summary>
        public NamespaceInclude DefaultNamespaceInclude { get; set; } = NamespaceInclude.None;

        /// <summary>
        /// Default include for class members.
        /// </summary>
        public ClassInclude DefaultClassInclude { get; set; } = ClassInclude.None;
    }
}
