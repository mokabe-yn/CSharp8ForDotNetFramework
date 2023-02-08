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

ローカル変数の初期化抑止
------------------------
C#ではすべての変数を0初期化する仕様になっている。
stackalloc配列で0初期化を無駄と割り切れる場合、0初期化分のコストが無駄になる。

この属性を付けた関数はローカル変数の0初期化をしない。
unsafeなのでunsafe関数でしか利用できない。

usage:
```
[SkipLocalsInit]
string ToString(uint value) {
    if (value == 0) return "0";
    // 桁数が少ない場合、配列の先頭のほうは参照しない
    char* first = stackalloc char[10];
    char* p = first + 10;
    int length = 0;
    while (value != 0) {
        p--;
        *p = (char)(value % 10 + '0');
        value /= 10;
        length++;
    }
    return new string(p, 0, length);
}
```
