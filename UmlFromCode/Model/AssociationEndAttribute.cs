using System;

namespace UmlFromCode.Model
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class AssociationEndAttribute : UmlAttribute
    {
        public AssociationEndAttribute()
        {
        }

        public AssociationEndAttribute(Type otherEnd, string otherProperty, AssociationEndType type = AssociationEndType.None, int[] cardinality = null)
        {
            this.OtherEnd = otherEnd;
            this.OtherProperty = otherProperty;
            this.Type = type;
            this.Cardinality = cardinality;
        }

        public AssociationEndAttribute(Type otherEnd, string otherProperty, int[] cardinality)
        {
            this.OtherEnd = otherEnd;
            this.OtherProperty = otherProperty;
            this.Cardinality = cardinality;
        }

        public string Name { get; set; }

        public Type OtherEnd { get; set; }

        public string OtherProperty { get; set; }

        public AssociationEndType Type { get; set; } = AssociationEndType.None;

        public int[] Cardinality { get; set; }

        public string[] Diagrams { get; set; }
    }
}
