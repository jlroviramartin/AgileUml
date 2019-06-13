// Copyright 2019 Jose Luis Rovira Martin
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//     http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
namespace UmlFromCode.PlantUml
{
    /// <summary>
    /// This class prints PlantUml diagrams.
    /// </summary>
    public interface IPlantUmlPrinter
    {
        /// <summary>The current diagram.</summary>
        string Diagram { get; }

        /// <summary>The class diagram.</summary>
        string Class { get; }

        /// <summary>
        /// This method starts a new diagram called <code>name</code>.
        /// </summary>
        /// <param name="name">Name of the diagram.</param>
        void BeginDiagram(string name);

        /// <summary>
        /// This method ends the current diagram.
        /// </summary>
        void EndDiagram();

        /// <summary>
        /// This method starts a new class called <code>name</code>.
        /// It must be called inside a diagram.
        /// </summary>
        /// <param name="classType">The type of the class.</param>
        /// <param name="name">The name of the class.</param>
        /// <param name="generics">The generic arguments of the class.</param>
        /// <param name="stereotypes">The stereotypes of the class.</param>
        void BeginClass(ClassType classType, string name, string generics = null, string[] stereotypes = null);

        /// <summary>
        /// This method ends the current class.
        /// It must be called inside a diagram.
        /// </summary>
        void EndClass();

        /// <summary>
        /// This method prints a constructor.
        /// </summary>
        /// <param name="modifiers">The modifiers of the constructor.</param>
        /// <param name="type">The type of the constructor.</param>
        /// <param name="arguments">The arguments of the constructor.</param>
        void PrintConstructor(Modifiers modifiers, string type, string arguments);

        /// <summary>
        /// This method prints a method.
        /// </summary>
        /// <param name="modifiers">The modifiers of the method.</param>
        /// <param name="type">The return type of the method.</param>
        /// <param name="name">The name of the method.</param>
        /// <param name="arguments">The arguments of the method.</param>
        void PrintMethod(Modifiers modifiers, string type, string name, string arguments);

        /// <summary>
        /// This method prints a field.
        /// </summary>
        /// <param name="modifiers">The modifiers of the field.</param>
        /// <param name="type">The return type of the field.</param>
        /// <param name="name">The name of the field.</param>
        void PrintField(Modifiers modifiers, string type, string name);

        /// <summary>
        /// This method prints an association between the <code>endType1</code>
        /// and <code>endType2</code> types.
        /// </summary>
        /// <param name="endName1">The name of the 1st association end.</param>
        /// <param name="endCardinality1">The cardinality of the 1st association end.</param>
        /// <param name="endType1">The type of the 1st association end.</param>
        /// <param name="endName2">The name of the 2nd association end.</param>
        /// <param name="endCardinality2">The cardinality of the 2nd association end.</param>
        /// <param name="endType2">The type of the 2nd association end.</param>
        /// <param name="name">The name of the association.</param>
        /// <param name="size">The size of the association line.</param>
        /// <param name="pattern">The pattern of association line.</param>
        void PrintAssociation(string endName1, string endCardinality1, EndType endType1,
                              string endName2, string endCardinality2, EndType endType2,
                              string name, int size = 2, LinePattern pattern = LinePattern.Solid);

        /// <summary>
        /// This method prints a generalization between the <code>general</code>
        /// and <code>spefific</code> types.
        /// </summary>
        /// <param name="general">The general type of the generalization.</param>
        /// <param name="specific">The specific type of the generalization.</param>
        /// <param name="name">The name of the generalization.</param>
        /// <param name="size">The size of the generalization line.</param>
        /// <param name="pattern">The pattern of generalization line.</param>
        void PrintGeneralization(string general, string specific,
                                 string name, int size = 2, LinePattern pattern = LinePattern.Solid);


        /// <summary>
        /// This method is used to mark and object as being processed by the printer, for the current diagram.
        /// </summary>
        /// <param name="obj">Object to mark.</param>
        void CheckInDiagram(object obj);

        /// <summary>
        /// This method is used to test if an object has been processed by the printer, for the current diagram.
        /// </summary>
        /// <param name="obj">Object to test.</param>
        /// <returns>Returns <code>true</code> if the object has been marked. <code>false</code> otherwise.</returns>
        bool IsCheckedInDiagram(object obj);
    }
}
