using System;

namespace UmlFromCode.Model
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, AllowMultiple = true)]
    public class MemberDiagramAttribute : UmlAttribute
    {
        public MemberDiagramAttribute(params string[] diagrams)
        {
            this.Diagrams = diagrams;
        }

        /// <summary>
        /// Which diagrams this attribute are related to.
        /// </summary>
        public string[] Diagrams { get; set; }

        /// <summary>
        /// This member points out if we want to hide it from the diagrams.
        /// </summary>
        public bool Hide { get; set; }
    }
}
