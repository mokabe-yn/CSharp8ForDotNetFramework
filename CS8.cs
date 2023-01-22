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


// not support list.
// * Span<T> -> package "System.Memory"


#nullable enable
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
            ParameterName = parameterName; // requires C#8
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
}

// Index, Range
namespace System {
    internal readonly struct Index : IEquatable<Index> {
        private readonly int _value;
        public Index(int value, bool fromEnd = false) {
            if (value < 0) throw new ArgumentException(nameof(value));
            _value = fromEnd ? ~value : value;
        }

        // properties
        public int Value => IsFromEnd ? ~_value : _value;
        public bool IsFromEnd => _value < 0;
        static public Index Start => new Index(0, false);
        static public Index End => new Index(0, true);

        // core method
        public int GetOffset(int length) {
            if (IsFromEnd) {
                return length - Value;
            } else {
                return _value;
            }
        }
        static public Index FromStart(int value) => new Index(value, false);
        static public Index FromEnd(int value) => new Index(value, true);

        // support method
        public bool Equals(Index other) {
            return _value == other._value;
        }
        public override bool Equals(object value) {
            if (value is Index v) {
                return Equals(v);
            } else {
                return false;
            }
        }
        public override int GetHashCode() {
            return _value.GetHashCode();
        }
        public override string ToString() {
            return IsFromEnd ? $"^{Value}" : $"{_value}";
        }
        // operators
        public static implicit operator Index(int value) => FromStart(value);
    }

    internal readonly struct Range : IEquatable<Range> {
        public Range(Index start, Index end) {
            Start = start;
            End = end;
        }

        // properties
        public Index Start { get; }
        public Index End { get; }
        public static Range All => new Range(Index.Start, Index.End);

        // core method
        public (int Offset, int Length) GetOffsetAndLength(int length) {
            int start = Start.GetOffset(length);
            int end = End.GetOffset(length);
            int l = end - start;

            if (start > length) throw new ArgumentOutOfRangeException();
            if (end > length) throw new ArgumentOutOfRangeException();
            if (l < 0) throw new ArgumentOutOfRangeException();

            return (start, l);
        }
        public static Range StartAt(Index start) => new Range(start, Index.End);
        public static Range EndAt(Index end) => new Range(Index.Start, end);

        // support method
        public bool Equals(Range other) {
            return
                Start.Equals(other.Start) &&
                End.Equals(other.End) &&
                true;
        }
        public override bool Equals(object value) {
            if (value is Range v) {
                return Equals(v);
            } else {
                return false;
            }
        }
        public override int GetHashCode() {
            return new CSharp8ForDotNetFramework.__hashcode_cs8()
                .Combine(Start)
                .Combine(End)
                ;
        }
        public override string ToString() {
            return $"{Start}..{End}";
        }
    }

    namespace Runtime.CompilerServices {
        static class RuntimeHelpers {
            public static T[] GetSubArray<T>(T[] array, Range range) {
                (int offset, int length) = range.GetOffsetAndLength(array.Length);
                T[] ret = new T[length];

                for (int iret = 0, isrc = offset; iret < length; iret++, isrc++) {
                    ret[iret] = array[isrc];
                }
                return ret;
            }
        }

    }
}

// IAsyncEnumerable, and requested functions from compiler.
namespace System {
    namespace Collections.Generic {
        internal interface IAsyncEnumerable<out T> {
            public IAsyncEnumerator<T> GetAsyncEnumerator(System.Threading.CancellationToken cancellationToken = default);
        }
        internal interface IAsyncEnumerator<out T> : IAsyncDisposable {
            public T Current { get; }
            public System.Threading.Tasks.ValueTask<bool> MoveNextAsync();
        }
    }
    internal interface IAsyncDisposable {
        public System.Threading.Tasks.ValueTask DisposeAsync();
    }
}
namespace System.Threading.Tasks {
    namespace Sources {
        internal struct ManualResetValueTaskSourceCore<TResult> {
            public bool RunContinuationsAsynchronously { get; set; }
            public short Version { get; private set; }
            public TResult GetResult(short token) {
                ValidateToken(token);
                ValidateCompleted();
                ExceptionInfo?.Throw();
                return ValueResult;
            }
            public System.Threading.Tasks.Sources.ValueTaskSourceStatus GetStatus(short token) {
                ValidateToken(token);
                if (!Completed) return ValueTaskSourceStatus.Pending;
                if (ExceptionInfo is System.Runtime.ExceptionServices.ExceptionDispatchInfo e) {
                    if(e.SourceException is System.OperationCanceledException) {
                        return ValueTaskSourceStatus.Canceled;
                    } else {
                        return ValueTaskSourceStatus.Faulted;
                    }
                } else {
                    return ValueTaskSourceStatus.Succeeded;
                }
            }
            public void OnCompleted(Action<object?> continuation, object? state, short token, System.Threading.Tasks.Sources.ValueTaskSourceOnCompletedFlags flags) {
                ValidateToken(token);
                if (Continuation != null) {
                    throw new InvalidOperationException();
                }
                Continuation = continuation;
                ContinuationArgument = state;
            }
            public void Reset() {
                Version++;
                Completed = false;
                ExceptionInfo = null;
                Continuation = null;
                ContinuationArgument = null;
            }
            public void SetException(Exception error) {
                ExceptionInfo = System.Runtime.ExceptionServices.ExceptionDispatchInfo.Capture(error);
                Complete();
            }
            public void SetResult(TResult result) {
                ValueResult = result;
                Complete();
            }

            // private members
            bool Completed { get; set; }
            TResult ValueResult { get; set; }
            Action<object?>? Continuation { get; set; }
            object? ContinuationArgument { get; set; }
            System.Runtime.ExceptionServices.ExceptionDispatchInfo? ExceptionInfo { get; set; }
            void ValidateToken(short token) {
                if (Version != token) {
                    throw new InvalidOperationException();
                }
            }
            void Complete() {
                Completed = true;
                Continuation?.Invoke(ContinuationArgument);
            }
            void ValidateCompleted() {
                if (!Completed) {
                    throw new InvalidOperationException();
                }
            }
        }
    }
}
namespace System.Runtime.CompilerServices {

    internal struct AsyncIteratorMethodBuilder {
        public void MoveNext<TStateMachine>(ref TStateMachine stateMachine) where TStateMachine : System.Runtime.CompilerServices.IAsyncStateMachine {
            stateMachine.MoveNext();
        }

        public void AwaitOnCompleted<TAwaiter, TStateMachine>(ref TAwaiter awaiter, ref TStateMachine stateMachine)
            where TAwaiter : System.Runtime.CompilerServices.INotifyCompletion
            where TStateMachine : System.Runtime.CompilerServices.IAsyncStateMachine {

            try {
                stateMachine.MoveNext();
            }catch(Exception e) {
                var einfo = System.Runtime.ExceptionServices.ExceptionDispatchInfo.Capture(e);
                System.Threading.ThreadPool.QueueUserWorkItem(state => ((System.Runtime.ExceptionServices.ExceptionDispatchInfo)state!).Throw(), einfo);
            }
        }
        public void AwaitUnsafeOnCompleted<TAwaiter, TStateMachine>(ref TAwaiter awaiter, ref TStateMachine stateMachine)
            where TAwaiter : System.Runtime.CompilerServices.ICriticalNotifyCompletion
            where TStateMachine : System.Runtime.CompilerServices.IAsyncStateMachine {
            AwaitOnCompleted(ref awaiter, ref stateMachine);
        }
        public void Complete() { }
        public static System.Runtime.CompilerServices.AsyncIteratorMethodBuilder Create() => default;
    }

}

// Valuetask
namespace System.Threading.Tasks {
    internal struct ValueTask {
        private enum Mode {
            None,
            Source,
            Task,
            Result,
        }
        Mode _mode;
        object? _obj;

        public ValueTask(System.Threading.Tasks.Sources.IValueTaskSource source, short token) {
            _mode = Mode.Source;
            _obj = ValueTuple.Create(source, token);
        }
        public ValueTask(System.Threading.Tasks.Task task) {
            _mode = Mode.Task;
            _obj = task;
        }
        public Awaiter GetAwaiter() => new Awaiter(in this);
        public struct Awaiter : System.Runtime.CompilerServices.INotifyCompletion {
            Mode _mode;
            object? _obj;
            public Awaiter(in ValueTask parent) {
                _mode = parent._mode;
                _obj = parent._obj;
            }
            public bool IsCompleted {
                get {
                    switch (_mode) {
                        case Mode.None:
                            return true;  // default(ValueTask)
                        case Mode.Source:
                            var (source, token) = (ValueTuple<System.Threading.Tasks.Sources.IValueTaskSource, short>)_obj!;
                            return source.GetStatus(token) != Sources.ValueTaskSourceStatus.Pending;
                        case Mode.Task:
                            var task = (System.Threading.Tasks.Task)_obj!;
                            return task.IsCompleted;
                        default:
                            throw new InvalidOperationException();
                    }
                }
            }
            public void OnCompleted(Action continuation) {
                switch (_mode) {
                    case Mode.Source:
                        var (source, token) = (ValueTuple<System.Threading.Tasks.Sources.IValueTaskSource, short>)_obj!;
                        source.OnCompleted(_ => continuation(), null, token, Sources.ValueTaskSourceOnCompletedFlags.None);
                        break;
                    case Mode.Task:
                        var task = (System.Threading.Tasks.Task)_obj!;
                        task.GetAwaiter().OnCompleted(continuation);
                        break;
                    default:
                        throw new InvalidOperationException();
                }
            }
            public void GetResult() {
            }
        }
    }
    internal struct ValueTask<TResult> {
        private enum Mode {
            None,
            Source,
            Task,
            Result,
        }
        Mode _mode;
        object? _obj;

        public ValueTask(System.Threading.Tasks.Sources.IValueTaskSource<TResult> source, short token) {
            _mode = Mode.Source;
            _obj = ValueTuple.Create(source, token);
        }
        public ValueTask(System.Threading.Tasks.Task<TResult> task) {
            _mode = Mode.Task;
            _obj = task;
        }
        public ValueTask(TResult result) {
            _mode = Mode.Result;
            _obj = result;
        }
        public Awaiter GetAwaiter() => new Awaiter(in this);
        public struct Awaiter : System.Runtime.CompilerServices.INotifyCompletion {
            Mode _mode;
            object? _obj;
            public Awaiter(in ValueTask<TResult> parent) {
                _mode = parent._mode;
                _obj = parent._obj;
            }
            public bool IsCompleted {
                get {
                    switch (_mode) {
                        case Mode.None:
                            return true; // default(ValueTask)
                        case Mode.Source:
                            var (source, token) = (ValueTuple<System.Threading.Tasks.Sources.IValueTaskSource<TResult>, short>)_obj!;
                            return source.GetStatus(token) != Sources.ValueTaskSourceStatus.Pending;
                        case Mode.Task:
                            var task = (System.Threading.Tasks.Task<TResult>)_obj!;
                            return task.IsCompleted;
                        case Mode.Result:
                            var result = (TResult)_obj!;
                            return true;
                        default:
                            throw new InvalidOperationException();
                    }
                }
            }
            public void OnCompleted(Action continuation) {
                switch (_mode) {
                    case Mode.Source:
                        var (source, token) = (ValueTuple<System.Threading.Tasks.Sources.IValueTaskSource<TResult>, short>)_obj!;
                        source.OnCompleted(_ => continuation(), null, token, Sources.ValueTaskSourceOnCompletedFlags.None);
                        break;
                    case Mode.Task:
                        var task = (System.Threading.Tasks.Task<TResult>)_obj!;
                        task.GetAwaiter().OnCompleted(continuation);
                        break;
                    case Mode.Result:
                        var result = (TResult)_obj!;
                        break;
                    default:
                        throw new InvalidOperationException();
                }
            }
            public TResult GetResult() {
                switch (_mode) {
                    case Mode.Source:
                        var (source, token) = (ValueTuple<System.Threading.Tasks.Sources.IValueTaskSource<TResult>, short>)_obj!;
                        return source.GetResult(token);
                    case Mode.Task:
                        var task = (System.Threading.Tasks.Task<TResult>)_obj!;
                        return task.Result;
                    case Mode.Result:
                        var result = (TResult)_obj!;
                        return result;
                    default:
                        throw new InvalidOperationException();
                }
            }
        }
    }
    namespace Sources {
        internal enum ValueTaskSourceOnCompletedFlags {
            None,
            UseSchedulingContext,
            FlowExecutionContext,
        }
        internal enum ValueTaskSourceStatus {
            Pending,
            Succeeded,
            Faulted,
            Canceled,
        }
        internal interface IValueTaskSource {
            void GetResult(short token);
            ValueTaskSourceStatus GetStatus(short token);
            void OnCompleted(Action<object?> continuation, object? state, short token, ValueTaskSourceOnCompletedFlags flags);
        }
        internal interface IValueTaskSource<out TResult> {
            TResult GetResult(short token);
            ValueTaskSourceStatus GetStatus(short token);
            void OnCompleted(Action<object?> continuation, object? state, short token, ValueTaskSourceOnCompletedFlags flags);
        }
    }
}

namespace CSharp8ForDotNetFramework {
    // INTERNAL hash utility.
    // usage:
    //     public override int GetHashCode() {
    //         return new CSharp8ForDotNetFramework.__hashcode_cs8()
    //             .Combine(_field1)
    //             .Combine(_field1)
    //             .Combine(_field1)
    //             .Final();
    //     }
    /* INTERNAL */ struct __hashcode_cs8 {
        int _value;
        public __hashcode_cs8 Combine<T>(T obj) where T : struct {
            _value ^= _value << 13;
            _value ^= _value >> 17;
            _value ^= _value << 5;
            _value ^= obj.GetHashCode();
            return this;
        }
        // requires C#8 readonly method.
        public readonly int Final() => _value;
        public static implicit operator int(__hashcode_cs8 @this) {
            return @this.Final();
        }
    }
}
