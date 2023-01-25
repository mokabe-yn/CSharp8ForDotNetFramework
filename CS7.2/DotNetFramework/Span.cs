using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace CS7_2 {
    [TestClass]
    public class Span {
        public void CanStackAlloc() {
            Span<int> stack = stackalloc int[8];
        }
        [TestMethod]
        public void Write() {
            int[] array = new int[] { 0, 1, 2, 3, 4 };
            var s = array.AsSpan().Slice(1, 2);
            s[0] = 10;
            CollectionAssert.AreEqual(array, new int[] { 0, 10, 2, 3, 4 });
            Span<int> stack = stackalloc int[8];
        }
    }
}
