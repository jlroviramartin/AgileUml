using AgileUml.Model;

namespace UmlFromCode.TestModel
{
    public class ClassB
    {
        public ClassB()
        {
        }

        public void Method(ClassA a, ClassB b)
        {
        }

        public ClassA PropertyA { get; set; }
        public ClassA fieldA;

        public object PropertyO { get; set; }
        public object fieldO;

        [AssociationEnd(typeof(ClassA), nameof(ClassA.B),
            Name = "AB",
            Cardinality = new[] { 0, 1 })]
        public ClassA A { get; set; }
    }
}
