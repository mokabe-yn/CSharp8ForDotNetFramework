using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace CS8 {
    [TestClass]
    public class Index {
        static int[] A => new int[] { 0, 1, 2, 3, 4, 5 };
        [TestMethod]
        public void LastElement() {
            Assert.AreEqual(5, A[^1]);
        }
        [TestMethod]
        public void FrontElement() {
            System.Index i = 2;
            Assert.AreEqual(2, A[i]);
        }

        [TestMethod]
        public void BackElement() {
            System.Index i = ^2;
            Assert.AreEqual(4, A[i]);
        }
        [TestMethod]
        public void OutOfRange() {
            Assert.ThrowsException<IndexOutOfRangeException>(() => A[^0]);
        }
    }
}
