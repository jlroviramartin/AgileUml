using System;

namespace UmlFromCode.Model
{
    /// <summary>
    /// Namespace members to include.
    /// </summary>
    [Flags]
    public enum NamespaceInclude
    {
        None = 0,
        All = Classes | Interfaces | Structures | Enums | Delegates,
        Classes = 1,
        Interfaces = 2,
        Structures = 4,
        Enums = 8,
        Delegates = 16,
    }
}
