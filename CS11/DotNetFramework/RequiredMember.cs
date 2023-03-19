using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Runtime.CompilerServices;
using System;
using System.Linq.Expressions;


// プロジェクトの設定をしてもわざわざこちらでも enable にする必要がある
#nullable enable

namespace CS11 {
    public class RequiredMember {
        public void RequiredProperty() {
            _ = new C() { Property = 1 };
        }
    }

    file class C {
        public required int Property { get; init; }
    }
}
