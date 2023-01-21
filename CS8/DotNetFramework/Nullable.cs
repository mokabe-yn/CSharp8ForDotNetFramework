using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics.CodeAnalysis;
using System;


// プロジェクトの設定をしてもわざわざこちらでも enable にする必要がある
#nullable enable 

namespace CS8 {
    // 警告なしでコンパイルが通ればいいので [TestClass] は不要
    public class Nullable {
        // Nullable フロー解析

        static void AllowNull([AllowNull] string _) { }
        // Disallow のテスト不可
        static void MaybeNull([MaybeNull] out string s) => s = null;
        static void NotNull([NotNull] out string? s) => s = "";
        static bool MaybeNullWhen([MaybeNullWhen(false)] out string s) {
            s = null;
            return false;
        }
        static bool NotNullWhen([NotNullWhen(true)] out string? s) {
            s = null;
            return false;
        }
        [return: NotNullIfNotNull("s")]
        static string? NotNullIfNotNull(string? s) => s;

        [DoesNotReturn]
        static string? DoesNotReturn(string? s) => throw new Exception();

        static string? DoesNotReturnIf([DoesNotReturnIf(false)] bool b) {
            if (!b) throw new Exception();
            return null;
        }

        public void AllowNull() {
            AllowNull(null);
        }
        public void MaybeNull() {
            MaybeNull(out string? s);
        }
        public void NotNull() {
            NotNull(out string s);
        }
        public void MaybeNullWhen() {
            if (MaybeNullWhen(out string? s)) {
                string ss = s;
            }
        }
        public void NotNullWhen() {
            if (NotNullWhen(out string? s)) {
                string ss = s;
            }
        }
        public void NotNullIfNotNull() {
            string s = NotNullIfNotNull("");
        }
        public void DoesNotReturn() {
            string s = DoesNotReturn("");
        }
        public void DoesNotReturnIf() {
            string s = DoesNotReturnIf(false);
        }
    }
}
