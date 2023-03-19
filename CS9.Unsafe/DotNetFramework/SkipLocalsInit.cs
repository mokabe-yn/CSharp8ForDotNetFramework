using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Runtime.CompilerServices;

namespace CS9.Unsafe {
    // 警告なしでコンパイルが通ればいいので [TestClass] は不要
    public class SkipLocalsInit {
        internal static bool _initial_function = false;

        [SkipLocalsInit]
        unsafe public void TestMethod1() {
            // 「未初期化である」ことのテストは不可能なので、コンパイルが通ることを確かめるだけ
            int* p = stackalloc int[8];
        }
    }
}
