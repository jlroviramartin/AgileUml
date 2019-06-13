using System.Linq;
using AgileUml.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UmlFromCode.Test
{
    [TestClass]
    public class ReflectionUtilsTest
    {
        [TestMethod]
        public void TestDecomposeFlag()
        {
            CollectionAssert.AreEqual(
                expected: new ClassInclude[] { ClassInclude.Constructors, ClassInclude.Methods, ClassInclude.Properties, ClassInclude.Fields, ClassInclude.Generalizations, ClassInclude.Associations, ClassInclude.Dependencies },
                actual: ClassInclude.All.DecomposeFlag().ToArray());

            CollectionAssert.AreEqual(
                expected: new ClassInclude[] { ClassInclude.Constructors, ClassInclude.Methods },
                actual: (ClassInclude.Constructors | ClassInclude.Methods).DecomposeFlag().ToArray());
        }
    }
}
