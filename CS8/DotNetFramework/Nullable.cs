using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

// プロジェクトの設定をしてもわざわざこちらでも enable にする必要がある
#nullable enable 

namespace CS8 {
    [TestClass]
    public class Nullable {
        [TestMethod]
        public void TestMethod1() {
            string? a = null;
            string b = a;
        }
    }
}
