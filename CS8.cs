// Nullable support Attributes
namespace System.Diagnostics.CodeAnalysis {
    [System.AttributeUsage(
        System.AttributeTargets.Field |
        System.AttributeTargets.Parameter |
        System.AttributeTargets.Property, Inherited = false)]
    internal sealed class AllowNullAttribute : Attribute { }
    [System.AttributeUsage(
        System.AttributeTargets.Field |
        System.AttributeTargets.Parameter |
        System.AttributeTargets.Property, Inherited = false)]
    internal sealed class DisallowNullAttribute : Attribute { }
    [System.AttributeUsage(
        System.AttributeTargets.Field |
        System.AttributeTargets.Parameter |
        System.AttributeTargets.Property |
        System.AttributeTargets.ReturnValue, Inherited = false)]
    internal sealed class MaybeNullAttribute : Attribute { }
    [System.AttributeUsage(
        System.AttributeTargets.Field |
        System.AttributeTargets.Parameter |
        System.AttributeTargets.Property |
        System.AttributeTargets.ReturnValue, Inherited = false)]
    internal sealed class NotNullAttribute : Attribute { }
    [System.AttributeUsage(
        System.AttributeTargets.Parameter, Inherited = false)]
    internal sealed class MaybeNullWhenAttribute : Attribute {
        public bool ParameterValue { get; }
        public MaybeNullWhenAttribute(bool parameterValue) {
            ParameterValue = parameterValue;
        }
    }
    [System.AttributeUsage(
        System.AttributeTargets.Parameter, Inherited = false)]
    internal sealed class NotNullWhenAttribute : Attribute {
        public bool ParameterValue { get; }
        public NotNullWhenAttribute(bool parameterValue) {
            ParameterValue = parameterValue;
        }
    }
    [System.AttributeUsage(
        System.AttributeTargets.Parameter |
        System.AttributeTargets.Property |
        System.AttributeTargets.ReturnValue, AllowMultiple = true, Inherited = false)]
    internal sealed class NotNullIfNotNullAttribute : Attribute {
        public string ParameterName { get; }
        public NotNullIfNotNullAttribute(string parameterName) {
            ParameterName = 0 switch { _ => parameterName }; // requires C#8
        }
    }
    [System.AttributeUsage(
        System.AttributeTargets.Method, Inherited = false)]
    internal sealed class DoesNotReturnAttribute : Attribute { }
    [System.AttributeUsage(
        System.AttributeTargets.Parameter, Inherited = false)]
    internal sealed class DoesNotReturnIfAttribute : Attribute {
        public bool ParameterValue { get; }
        public DoesNotReturnIfAttribute(bool parameterValue) {
            ParameterValue = parameterValue;
        }
    }
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
