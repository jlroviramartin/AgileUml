using System;

namespace UmlFromCode.Model
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, AllowMultiple = true)]
    public class ClassDiagramAttribute : UmlAttribute
    {
        public ClassDiagramAttribute(params string[] diagrams)
        {
            this.Diagrams = diagrams;
        }

        /// <summary>
        /// Which diagrams this attribute are related to.
        /// </summary>
        public string[] Diagrams { get; set; }

        /// <summary>
        /// Default include for nested members.
        /// </summary>
        public NamespaceInclude NestedInclude { get; set; } = NamespaceInclude.None;

        /// <summary>
        /// Default include for class members.
        /// </summary>
        public ClassInclude Include { get; set; } = ClassInclude.None;
    }
}
