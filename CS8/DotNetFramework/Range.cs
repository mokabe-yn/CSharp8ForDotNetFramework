using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace CS8 {
    [TestClass]
    public class Range {
        static int[] A => new int[] { 0, 1, 2, 3, 4, 5 };
        [TestMethod]
        public void Useable() {
            var s__ = ..;
            var s0_ = 0..;
            var s1_ = ^0..;
            var s_0 = ..0;
            var s00 = 0..0;
            var s10 = ^0..0;
            var s_1 = ..^0;
            var s01 = 0..^0;
            var s11 = ^0..^0;
        }

        [TestMethod]
        public void All() {
            CollectionAssert.AreEqual(new[] { 0, 1, 2, 3, 4, 5 }, A[..]);
        }
        [TestMethod]
        public void FromStart() {
            CollectionAssert.AreEqual(new[] { 0, 1, 2, 3, 4, 5 }, A[0..]);
        }
        [TestMethod]
        public void ToEnd() {
            CollectionAssert.AreEqual(new[] { 0, 1, 2, 3, 4, 5 }, A[..^0]);
        }
        [TestMethod]
        public void FromFirst() {
            CollectionAssert.AreEqual(new[] { 1, 2, 3, 4, 5 }, A[1..]);
        }
        [TestMethod]
        public void ToLast() {
            CollectionAssert.AreEqual(new[] { 0, 1, 2, 3, 4 }, A[..^1]);
        }
        [TestMethod]
        public void Middle() {
            CollectionAssert.AreEqual(new[] { 1, 2, 3, 4 }, A[1..^1]);
        }
        [TestMethod]
        public void Single() {
            CollectionAssert.AreEqual(new[] { 1 }, A[1..2]);
        }
        [TestMethod]
        public void Empty() {
            CollectionAssert.AreEqual(new int[] { }, A[1..1]);
        }
        [TestMethod]
        public void Minus() {
            Assert.ThrowsException<IndexOutOfRangeException>(A[1..0]);
        }
        [TestMethod]
        public void OutOfRange() {
            Assert.ThrowsException<IndexOutOfRangeException>(A[8..8]);
        }

        // ToString
        [TestMethod]
        public void ToString1() {
            Assert.AreEqual("0..^0", ..);
        }
        [TestMethod]
        public void ToString2() {
            Assert.AreEqual("3..4", 3..4);
        }
        [TestMethod]
        public void ToString3() {
            Assert.AreEqual("^3..^4", ^3..^4);
        }

    }
}
