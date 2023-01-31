C#9
===

このライブラリでは、以下の機能をサポートしています。


Init専用プロパティ
----
`new Class(){ Property = 0 };`のように、
初期化時だけ代入を認める特殊なプロパティ。

モジュール初期化子
----
この属性をつけた関数は、静的コンストラクタのように最初に1回だけ呼ばれる。

usage: 
```
class A {
    [ModuleInitializer] internal void InitMethod1() {}
    [ModuleInitializer] internal void InitMethod2() {}
}
```

アノテーション属性
---
C#8で標準で`Nullable`用意されているが、それだけでは`Nullable`の機能を完全に生かすことはできない。
`bool TryGetValue(out string? res)` など、返り値が`true` なら `res` はNotNullであるようなケースを表現できない。
以下に挙げる属性を(主にメソッドに)加えることで、そのようなケースを補完する。
これらの属性は `System.Diagnostics.CodeAnalysis` 名前空間に属している。

これらの属性を付けたことによる効果は、関数定義側と呼び出し側両方に適用される。
例えば `AllowNull` では、
- 関数定義側は入力がnullであることを考慮しなくてはならない。
- 関数呼び出し側は入力にnullを入力にすることが許される。
の影響がある。


### MemberNotNull
このメンバが呼ばれたら、`Members` のメンバはNotNullであることが保証される。

usage:
```
[MemberNotNull("Target")]void Init(string value) { Target = value; }
```


### MemberNotNullWhen
このメンバが呼ばれて返り値が `ReturnValue`なら、`Members` のメンバはNotNullであることが保証される。

usage:
```
[MemberNotNullWhen(true, "Target")]bool TryInit(string value) {
    var c=Create(value);
    if (c.Available){
        Target = c;
        return true;
    }else{
        Target = null;
        return false;
    }
}
```
