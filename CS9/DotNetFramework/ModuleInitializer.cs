using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Runtime.CompilerServices;

namespace CS9 {
    [TestClass]
    public class ModuleInitializer {
        internal static bool _initial_function = false;

        [TestMethod]
        public void TestMethod1() {
            Assert.IsTrue(_initial_function);
        }
    }
    class Initial {
        [ModuleInitializer]
        internal static void Func() => ModuleInitializer._initial_function = true;
    }
}
