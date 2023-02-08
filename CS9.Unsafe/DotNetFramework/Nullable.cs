using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics.CodeAnalysis;
using System;


// プロジェクトの設定をしてもわざわざこちらでも enable にする必要がある
#nullable enable

namespace CS9 {
    // 警告なしでコンパイルが通ればいいので [TestClass] は不要
    public class Nullable {
        string? Target { get; set; }

        [MemberNotNull(nameof(Target))]
        bool TargetForceNotNull { get; set; }
        [MemberNotNull(nameof(Target))]
        void SetTarget() => Target = "";


        [MemberNotNullWhen(true, nameof(Target))]
        bool Cond { get; set; }
        [MemberNotNullWhen(true, nameof(Target))]
        bool TrySetTarget(bool b) {
            if (b) {
                Target = "";
                return true;
            } else {
                return false;
            }

        }


        public void MemberNotNull1() {
            bool a = TargetForceNotNull;
            string s = Target;
        }
        public void MemberNotNull2() {
            SetTarget();
            string s = Target;
        }
        public void MemberNotNullWhen1() {
            if (Cond) {
                string s = Target;
            }
        }
        public void MemberNotNullWhen2() {
            if (TrySetTarget(true)) {
                string s = Target;
            }
        }

    }
}
