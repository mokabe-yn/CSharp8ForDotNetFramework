// CSharp8ForDotNetFramework for C#9
// Copyright 2023 Masayuki Okabe <okabe_m@hmi.aitech.ac.jp>
//
// Boost Software License - Version 1.0 - August 17th, 2003
//
// Permission is hereby granted, free of charge, to any person or organization
// obtaining a copy of the software and accompanying documentation covered by
// this license (the "Software") to use, reproduce, display, distribute,
// execute, and transmit the Software, and to prepare derivative works of the
// Software, and to permit third-parties to whom the Software is furnished to
// do so, all subject to the following:
//
// The copyright notices in the Software and this entire statement, including
// the above license grant, this restriction and the following disclaimer,
// must be included in all copies of the Software, in whole or in part, and
// all derivative works of the Software, unless such copies or derivative
// works are solely in the form of machine-executable object code generated by
// a source language processor.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE, TITLE AND NON-INFRINGEMENT. IN NO EVENT
// SHALL THE COPYRIGHT HOLDERS OR ANYONE DISTRIBUTING THE SOFTWARE BE LIABLE
// FOR ANY DAMAGES OR OTHER LIABILITY, WHETHER IN CONTRACT, TORT OR OTHERWISE,
// ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
// DEALINGS IN THE SOFTWARE.

// Nullable support Attributes


namespace System.Diagnostics.CodeAnalysis {
    [System.AttributeUsage(
        System.AttributeTargets.Method |
        System.AttributeTargets.Property, AllowMultiple = true, Inherited = false)]
    internal sealed class MemberNotNullAttribute : Attribute {
        public string[] Members { get; private init; } // force C#9
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


// "init" property
namespace System.Runtime.CompilerServices {
    internal static class IsExternalInit { }
}

// ModuleInitializer
namespace System.Runtime.CompilerServices {
    [System.AttributeUsage(System.AttributeTargets.Method, Inherited = false)]
    internal sealed class ModuleInitializerAttribute : Attribute { }
}

// SkipLocalsInit
namespace System.Runtime.CompilerServices {
    [System.AttributeUsage(
        System.AttributeTargets.Class |
        System.AttributeTargets.Constructor |
        System.AttributeTargets.Event |
        System.AttributeTargets.Interface |
        System.AttributeTargets.Method |
        System.AttributeTargets.Module |
        System.AttributeTargets.Property |
        System.AttributeTargets.Struct, Inherited = false)]
    internal sealed class SkipLocalsInitAttribute : Attribute { }
}
