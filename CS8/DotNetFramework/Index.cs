using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace CS8 {
    [TestClass]
    public class Index {
        static int[] A => new int[] { 1, 2, 3, 4, 5, 6 };
        [TestMethod]
        public void LastElement() {
            Assert.AreEqual(6, A[^1]);
        }
    }
}
