// CSharp8ForDotNetFramework for C#7.2
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




// The slow Span.
namespace System {
    internal readonly ref struct Span<T> {
        readonly T[] _ref;
        readonly int _start;
        readonly int _length;

        public bool IsEmpty => _length == 0;
        public Span<T> Slice(int start) {
            return new Span<T>(_ref, _start + start, _length - start);
        }
        public Span<T> Slice(int start, int length) {
            return new Span<T>(_ref, _start + start, length);
        }
        public T this[int index] {
            get {
                if (index < 0) throw new ArgumentOutOfRangeException(nameof(index));
                if (index >= _length) throw new ArgumentOutOfRangeException(nameof(index));
                return _ref[_start + index];
            }
            set {
                if (index < 0) throw new ArgumentOutOfRangeException(nameof(index));
                if (index >= _length) throw new ArgumentOutOfRangeException(nameof(index));
                _ref[_start + index] = value;
            }
        }
        public Span(T[] array) : this(array, 0, array.Length) { }
        public Span(T[] array, int start, int length) {
            if (start < 0) throw new ArgumentOutOfRangeException(nameof(start));
            if (length < 0) throw new ArgumentOutOfRangeException(nameof(length));
            if (start > array.Length) throw new ArgumentOutOfRangeException(nameof(start));
            if (start + length > array.Length) throw new ArgumentOutOfRangeException(nameof(length));
            _ref = array;
            _start = start;
            _length = length;
        }
        // `Span<T> _ = stackalloc T[N]` is not supported.
        // because require unsafe block.
        //public unsafe Span(void* pointer, int length) => throw new NotImplementedException();

        public T[] ToArray() {
            if (_length == 0) return Array.Empty<T>();
            T[] ret = new T[_length];
            for(int srci=_start, dsti = 0; dsti < _length; srci++, dsti++) {
                ret[dsti] = _ref[srci];
            }
            return ret;
        }
    }
    internal readonly ref struct ReadOnlySpan<T> {
        readonly T[] _ref;
        readonly int _start;
        readonly int _length;

        public bool IsEmpty => _length == 0;
        public ReadOnlySpan<T> Slice(int start) {
            return new ReadOnlySpan<T>(_ref, _start + start, _length - start);
        }
        public ReadOnlySpan<T> Slice(int start, int length) {
            return new ReadOnlySpan<T>(_ref, _start + start, length);
        }
        public T this[int index] {
            get {
                if (index < 0) throw new ArgumentOutOfRangeException(nameof(index));
                if (index >= _length) throw new ArgumentOutOfRangeException(nameof(index));
                return _ref[_start + index];
            }
        }
        public ReadOnlySpan(T[] array) : this(array, 0, array.Length) { }
        public ReadOnlySpan(T[] array, int start, int length) {
            if (start < 0) throw new ArgumentOutOfRangeException(nameof(start));
            if (length < 0) throw new ArgumentOutOfRangeException(nameof(length));
            if (start > array.Length) throw new ArgumentOutOfRangeException(nameof(start));
            if (start + length > array.Length) throw new ArgumentOutOfRangeException(nameof(length));
            _ref = array;
            _start = start;
            _length = length;
        }
        // `ReadOnlySpan<T> _ = stackalloc T[N]` is not supported.
        // because require unsafe block.
        //public unsafe ReadOnlySpan(void* pointer, int length) => throw new NotImplementedException();

        public T[] ToArray() {
            if (_length == 0) return Array.Empty<T>();
            T[] ret = new T[_length];
            for (int srci = _start, dsti = 0; dsti < _length; srci++, dsti++) {
                ret[dsti] = _ref[srci];
            }
            return ret;
        }

        public override string ToString() {
            if (typeof(T) == typeof(char)) {
                return string.Join("", ToArray());
            }
            return $"System.ReadOnlySpan<{typeof(T).Name}>[{_length}]";
        }
    }
    internal static class CSharp8ForDotNetFrameworkSpanExtension {
        public static Span<T> AsSpan<T>(this T[] array) {
            return new Span<T>(array);
        }
        public static Span<T> AsSpan<T>(this T[] array, int start) {
            return new Span<T>(array, start, array.Length - start);
        }

        private static readonly System.Runtime.CompilerServices.ConditionalWeakTable<string, char[]> _chararray_cache_table = new System.Runtime.CompilerServices.ConditionalWeakTable<string, char[]>();
        private static char[] _chararray_cache(string text) {
            if(_chararray_cache_table.TryGetValue(text, out char[] cache)) {
                return cache;
            } else {
                char[] value = System.Linq.Enumerable.ToArray(text);
                _chararray_cache_table.Add(text, value);
                return value;
            }
        }

        public static ReadOnlySpan<char> AsSpan(this string text) {
            return new ReadOnlySpan<char>(_chararray_cache(text));
        }
        public static ReadOnlySpan<char> AsSpan(this string text, int start) {
            return new ReadOnlySpan<char>(_chararray_cache(text), start, text.Length - start);
        }
    }
}