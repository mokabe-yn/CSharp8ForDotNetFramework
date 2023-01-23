using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Runtime.CompilerServices;
using System;
using System.Linq.Expressions;


// プロジェクトの設定をしてもわざわざこちらでも enable にする必要がある
#nullable enable

namespace CS10 {
    [TestClass]
    public class CallerArgumentExpression {
        static public string Expression(int value, [CallerArgumentExpression("value")] string? expression = null) {
            return expression!;
        }

        [TestMethod]
        public void Expression() {
            Assert.AreEqual(Expression(42), "42");
        }
    }
}
