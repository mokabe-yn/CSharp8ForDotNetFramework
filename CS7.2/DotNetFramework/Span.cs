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
        [TestMethod]
        public void Span3() {
            int[] array = new int[] { 0, 1, 2, 3, 4, 5, 6 };
            var s = array.AsSpan().Slice(0);
            CollectionAssert.AreEqual(s.ToArray(), new int[] { 0, 1, 2, 3, 4, 5, 6 });
        }
        [TestMethod]
        public void Span4() {
            int[] array = new int[] { 0, 1, 2, 3, 4, 5, 6 };
            var s = array.AsSpan().Slice(1);
            CollectionAssert.AreEqual(s.ToArray(), new int[] { 1, 2, 3, 4, 5, 6 });
        }
        [TestMethod]
        public void Span5() {
            int[] array = new int[] { 0, 1, 2, 3, 4, 5, 6 };
            var s = array.AsSpan(1).Slice(1);
            CollectionAssert.AreEqual(s.ToArray(), new int[] { 2, 3, 4, 5, 6 });
        }
    }
    [TestClass]
    public class DualSpan {
        [TestMethod]
        public void Dual() {
            int[] array = new int[] { 0, 1, 2, 3, 4, 5, 6 };
            var s1 = array.AsSpan().Slice(1, 5); // 1,2,3,4,5
            var s2 = s1.Slice(1, 3); // 2,3,4
            CollectionAssert.AreEqual(s2.ToArray(), new int[] { 2, 3, 4 });
        }
        [TestMethod]
        public void DualWrite() {
            int[] array = new int[] { 0, 1, 2, 3, 4, 5, 6 };
            var s1 = array.AsSpan().Slice(1, 5); // 1,2,3,4,5
            var s2 = s1.Slice(1, 3); // 2,3,4
            s2[0] = 10;
            CollectionAssert.AreEqual(array, new int[] { 0, 1, 10, 3, 4, 5, 6 });
        }
    }
    [TestClass]
    public class SpanEdgeCase {
        [TestMethod]
        public void Empty1() {
            int[] array = new int[] { 0, 1, 2, 3, 4, 5 };
            var s = array.AsSpan().Slice(0, 0);
            Assert.IsTrue(s.IsEmpty);
        }
        [TestMethod]
        public void Empty2() {
            int[] array = new int[] { 0, 1, 2, 3, 4, 5 };
            var s = array.AsSpan().Slice(6, 0);
            Assert.IsTrue(s.IsEmpty);
        }
        [TestMethod]
        public void Empty3() {
            int[] array = new int[] { 0, 1, 2, 3, 4, 5 };
            var s = array.AsSpan().Slice(6);
            Assert.IsTrue(s.IsEmpty);
        }
        [TestMethod]
        public void AsSpanEdgeCase1() {
            int[] array = new int[] { 0, 1, 2, 3, 4, 5 };
            _ = array.AsSpan(6);
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
        [TestMethod]
        public void SpanRange1() {
            int[] array = new int[] { 0, 1, 2, 3, 4, 5 };
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => array.AsSpan(1-));
        }
        [TestMethod]
        public void SpanRange2() {
            int[] array = new int[] { 0, 1, 2, 3, 4, 5 };
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => array.AsSpan(1 -));
        }
    }
}
