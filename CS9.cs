// Nullable support Attributes
namespace System.Diagnostics.CodeAnalysis {
    [System.AttributeUsage(
        System.AttributeTargets.Method |
        System.AttributeTargets.Property, AllowMultiple = true, Inherited = false)]
    internal sealed class MemberNotNullAttribute : Attribute {
        public string[] Members { get; }
        public MemberNotNullAttribute(string member) :
            this(new string[] { member }) { }
        public MemberNotNullAttribute(string[] members) {
            Members = members;
        }
    }
    [System.AttributeUsage(
        System.AttributeTargets.Method |
        System.AttributeTargets.Property, AllowMultiple = true, Inherited = false)]
    internal sealed class MemberNotNullWhenAttribute : Attribute {
        public bool ReturnValue { get; }
        public string[] Members { get; }
        public MemberNotNullWhenAttribute(bool returnValue, string member) :
            this(returnValue, new string[] { member }) { }
        public MemberNotNullWhenAttribute(bool returnValue, string[] members) {
            ReturnValue = returnValue;
            Members = members;
        }
    }
}
