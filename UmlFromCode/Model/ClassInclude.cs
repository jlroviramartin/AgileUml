using System;

namespace UmlFromCode.Model
{
    /// <summary>
    /// Class members to include.
    /// </summary>
    [Flags]
    public enum ClassInclude
    {
        None = 0,
        All = Constructors | Methods | Properties | Fields | Extensions | Associations,
        Constructors = 1,
        Methods = 2,
        Properties = 4,
        Fields = 8,
        Extensions = 16,
        Associations = 32,
    }
}
