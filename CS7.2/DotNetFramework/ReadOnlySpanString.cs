using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace CS7_2 {
    [TestClass]
    public class ReadOnlySpanString {
        [TestMethod]
        public void Use() {
            ReadOnlySpan<char> _ = "012345".AsSpan();
        }
        [TestMethod]
        public void Slice1() {
            Assert.AreEqual("012345".AsSpan().Slice(3).ToString(), "345");
        }
        [TestMethod]
        public void Slice2() {
            Assert.AreEqual("012345".AsSpan().Slice(2, 3).ToString(), "234");
        }
        [TestMethod]
        public void Slice3() {
            Assert.AreEqual("012345".AsSpan(1).Slice(2, 3).ToString(), "345");
        }
    }
    [TestClass]
    public class DualReadOnlySpanString {
        [TestMethod]
        public void Dual() {
            Assert.AreEqual("012345".AsSpan().Slice(1, 3).Slice(2, 1).ToString(), "3");
        }
    }
    [TestClass]
    public class ReadOnlySpanStringEdgeCase {
        [TestMethod]
        public void SliceEdgeCase1() {
            Assert.AreEqual("012345".AsSpan().Slice(0, 0).ToString(), "");
        }
        [TestMethod]
        public void SliceEdgeCase2() {
            Assert.AreEqual("012345".AsSpan().Slice(6).ToString(), "");
        }
        [TestMethod]
        public void SliceEdgeCase3() {
            Assert.AreEqual("012345".AsSpan().Slice(6, 0).ToString(), "");
        }
        [TestMethod]
        public void AsSpanEdgeCase1() {
            _ = "012345".AsSpan(6);
        }
    }
    [TestClass]
    public class ReadOnlySpanStringThrows {
        [TestMethod]
        public void SliceOutOfRange1() {
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => "012345".AsSpan().Slice(-1));
        }
        [TestMethod]
        public void SliceOutOfRange2() {
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => "012345".AsSpan().Slice(7));
        }
        [TestMethod]
        public void SliceOutOfRange3() {
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => "012345".AsSpan().Slice(-1, 2));
        }
        [TestMethod]
        public void SliceOutOfRange4() {
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => "012345".AsSpan().Slice(5, 2));
        }
        [TestMethod]
        public void SliceOutOfRange5() {
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => "012345".AsSpan().Slice(6, 1));
        }
        [TestMethod]
        public void SliceOutOfRange6() {
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => "012345".AsSpan().Slice(7, 0));
        }
        [TestMethod]
        public void SpanRange1() {
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => "012345".AsSpan(-1));
        }
        [TestMethod]
        public void SpanRange2() {
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => "012345".AsSpan(7));
        }
    }
}
