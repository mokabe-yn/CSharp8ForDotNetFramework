using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace CS7_2 {
    [TestClass]
    public class Span {
        public void CanStackAlloc() {
            // not supported.
            // Span<int> stack = stackalloc int[8];
        }
        [TestMethod]
        public void Write() {
            int[] array = new int[] { 0, 1, 2, 3, 4 };
            var s = array.AsSpan().Slice(1, 2);
            s[0] = 10;
            CollectionAssert.AreEqual(array, new int[] { 0, 10, 2, 3, 4 });
        }
        [TestMethod]
        public void Span1() {
            int[] array = new int[] { 0, 1, 2, 3, 4, 5, 6 };
            var s = array.AsSpan().Slice(1, 3);
            CollectionAssert.AreEqual(s.ToArray(), new int[] { 1, 2, 3 });
        }
        [TestMethod]
        public void Span2() {
            int[] array = new int[] { 0, 1, 2, 3, 4, 5, 6 };
            var s = array.AsSpan().Slice(3, 3);
            CollectionAssert.AreEqual(s.ToArray(), new int[] { 3, 4, 5 });
        }
    }
    [TestClass]
    public class SpanEdgeCase {
        [TestMethod]
        public void Empty1() {
            int[] array = new int[] { 0, 1, 2, 3, 4, 5, 6 };
            var s = array.AsSpan().Slice(0, 0);
            Assert.IsTrue(s.IsEmpty);
        }
        [TestMethod]
        public void Empty2() {
            int[] array = new int[] { 0, 1, 2, 3, 4, 5, 6 };
            var s = array.AsSpan().Slice(7, 0);
            Assert.IsTrue(s.IsEmpty);
        }
        [TestMethod]
        public void Empty3() {
            int[] array = new int[] { 0, 1, 2, 3, 4, 5, 6 };
            var s = array.AsSpan().Slice(7);
            Assert.IsTrue(s.IsEmpty);
        }
    }
    [TestClass]
    public class SpanThrows {
        readonly int[] array = new int[] { 0, 1, 2, 3, 4, 5 };

        [TestMethod]
        public void OutOfRange1() {
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => array.AsSpan().Slice(7));
        }
        [TestMethod]
        public void OutOfRange2() {
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => array.AsSpan().Slice(-1, 0));
        }
        [TestMethod]
        public void OutOfRange3() {
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => array.AsSpan().Slice(7, 0));
        }
        [TestMethod]
        public void OutOfRange4() {
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => array.AsSpan().Slice(6, 1));
        }
        [TestMethod]
        public void OutOfRange5() {
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => array.AsSpan().Slice(5, 2));
        }

    }
}
