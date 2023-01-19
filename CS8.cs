#nullable enable

// TODO:
// * async streams

// not support list.
// * Span<T> -> package "System.Memory"

// Nullable support Attributes

using Microsoft.VisualStudio.TestTools.UnitTesting;

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
    internal static class TaskAsyncEnumerableExtensions {
        internal static System.Runtime.CompilerServices.ConfiguredAsyncDisposable ConfigureAwait(this System.IAsyncDisposable source, bool continueOnCapturedContext) => throw new NotImplementedException();
        internal static System.Runtime.CompilerServices.ConfiguredCancelableAsyncEnumerable<T> ConfigureAwait<T>(this System.Collections.Generic.IAsyncEnumerable<T> source, bool continueOnCapturedContext) => throw new NotImplementedException();
    }

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
    /* INTERNAL */ readonly struct ConfiguredAsyncDisposable {
        public System.Runtime.CompilerServices.ConfiguredValueTaskAwaitable DisposeAsync() => throw new NotImplementedException();
    }
    /* INTERNAL */ readonly struct ConfiguredCancelableAsyncEnumerable<T> {
        public readonly struct Enumerator {
            public T Current { get; }
            public System.Runtime.CompilerServices.ConfiguredValueTaskAwaitable DisposeAsync() => throw new NotImplementedException();
            public System.Runtime.CompilerServices.ConfiguredValueTaskAwaitable<bool> MoveNextAsync() => throw new NotImplementedException();
        }

        public System.Runtime.CompilerServices.ConfiguredCancelableAsyncEnumerable<T> ConfigureAwait(bool continueOnCapturedContext) => throw new NotImplementedException();
        public System.Runtime.CompilerServices.ConfiguredCancelableAsyncEnumerable<T>.Enumerator GetAsyncEnumerator() => throw new NotImplementedException();
        public System.Runtime.CompilerServices.ConfiguredCancelableAsyncEnumerable<T> WithCancellation(System.Threading.CancellationToken cancellationToken) => throw new NotImplementedException();
    }

    internal struct AsyncIteratorMethodBuilder {
        System.Threading.Tasks.Task? _st;
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
