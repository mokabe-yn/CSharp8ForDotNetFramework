// CSharp8ForDotNetFramework for C#8
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

namespace System {
    // The slow Span.
    internal readonly ref struct Span<T> {
        readonly T[] _ref;
        readonly int _start;
        readonly int _length;

        public bool IsEmpty => throw new NotImplementedException();
        //Pinnable<T>
        public Span<T> Slice(int start) {
            throw new NotImplementedException();
        }
        public Span<T> Slice(int start, int length) {
            throw new NotImplementedException();
        }
        public T this[int index] {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }
        public Span(T[] array) => throw new NotImplementedException();
        public Span(T[] array, int start, int length) => throw new NotImplementedException();
        // `Span<T> _ = stackalloc T[N]` is not supported.
        // because require unsafe block.
        //public unsafe Span(void* pointer, int length) => throw new NotImplementedException();

        public T[] ToArray() => throw new NotImplementedException();
    }
    // The slow Span.
    internal readonly ref struct ReadOnlySpan<T> {
        readonly T[] _ref;
        readonly int _start;
        readonly int _length;

        public bool IsEmpty => throw new NotImplementedException();
        //Pinnable<T>
        public ReadOnlySpan<T> Slice(int start) {
            throw new NotImplementedException();
        }
        public ReadOnlySpan<T> Slice(int start, int length) {
            throw new NotImplementedException();
        }
        public T this[int index] {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }
        public ReadOnlySpan(T[] array) => throw new NotImplementedException();
        public ReadOnlySpan(T[] array, int start, int length) => throw new NotImplementedException();
        // `Span<T> _ = stackalloc T[N]` is not supported.
        // because require unsafe block.
        //public unsafe Span(void* pointer, int length) => throw new NotImplementedException();

        public T[] ToArray() => throw new NotImplementedException();
        public new string ToString() {
            throw new NotImplementedException();
        }
    }
    internal static class _____ {
        public static Span<T> AsSpan<T>(this T[] array) {
            throw new NotImplementedException();
        }
        public static Span<T> AsSpan<T>(this T[] array, int start) {
            throw new NotImplementedException();
        }
        public static ReadOnlySpan<char> AsSpan(this string array) {
            throw new NotImplementedException();
        }
        public static ReadOnlySpan<char> AsSpan(this string array, int start) {
            throw new NotImplementedException();
        }
    }
}

