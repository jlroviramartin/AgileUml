using System.Collections.Generic;
using AgileUml.Model;

[assembly: Diagram(
    Diagrams = new string[] { "Inheritance" },
    Namespaces = new string[] { "UmlFromCode.TestModel" },
    DefaultNamespaceInclude = NamespaceInclude.All,
    DefaultClassInclude = ClassInclude.Generalizations)]

[assembly: Diagram(
    Diagrams = new string[] { "Relationships" },
    Namespaces = new string[] { "UmlFromCode.TestModel" },
    DefaultNamespaceInclude = NamespaceInclude.All,
    DefaultClassInclude = ClassInclude.Constructors | ClassInclude.Methods | ClassInclude.Properties | ClassInclude.Fields | ClassInclude.Associations)]

namespace UmlFromCode.TestModel
{
    public class ClassA
    {
        public ClassA()
        {
        }

        public int Method(int a, int b)
        {
            return 0;
        }

        public int Property { get; set; }
        public int field;


        [AssociationEnd(typeof(ClassB), nameof(ClassB.A), new[] { 1, 1 })]
        public ClassB B { get; set; }

        [AssociationEnd(typeof(ClassB), null, AssociationEndType.Aggregation, new[] { 0, int.MaxValue })]
        public IList<ClassB> Bs { get; set; }
    }
}
