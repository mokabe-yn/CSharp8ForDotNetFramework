C#8
===


このライブラリでは、以下の機能をサポートしています。


インデックス
---
`array[^3]` のように、配列の後ろからアクセスする機能。
`^0` は配列末尾の次の要素を示す。

`array[^0]`は範囲外アクセスとなる。

範囲処理
---
`array[3..6]` のように、配列の一部を切り出す機能。
この式の結果は、`new T[]{array[3], array[4], array[5], }` と等価である。

`array[3..^2]` のようにアクセスしてもよい。


非同期イテレータブロック
---
イテレータブロックの拡張版。
ブロック中に `await` を使用できる。

```
async IAsyncEnumerable<T> Generator(...){
    yield return new T();
    await ...;
    yield return new T();
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

### AllowNull
入力の `T/T?` にかかわらず、入力として `null` を受け付ける。

### DisallowNull
入力の `T/T?` にかかわらず、入力として `null` を受け付けない。

### MaybeNull
入力の `T/T?` にかかわらず、返り値/ref として `null` を返す。

### NotNull
入力の `T/T?` にかかわらず、返り値/ref として `null` を返さない。

usage:
```
// 入力ではnullを受け付けるが、出力はNotNullを保証する
void SetValue([NotNull] ref string ?value){ value = "";}
```

### MaybeNullWhen
返り値が `ParameterValue` と同じなら、返り値/ref として `null` を返す。
そうでないなら、入力の `T/T?` の通り。

usage:
```
// falseなら valueがnullかもしれない
[return: MaybeNullWhen(false)] bool TryGetValue(out string value){...}
```

### NotNullWhen
返り値が `ParameterValue` と同じなら、返り値/ref として `null` を返さない。
そうでないなら、入力の `T/T?` の通り。

usage:
```
// trueなら valueがnullにはならない
[return: NotNullWhen(true)] bool TryGetValue(out string? value){...}
```

### NotNullIfNotNull
`ParameterName` の引数名が `null` でないなら `null` ではない。

usage:
```
// 入力がnullなら出力もそのままnullにする
[return: NotNullIfNotNull(true)] string? Reverse(string? value){
    if (value is null) return null;
    return ...:
}
```

usage:
```
// どちらかが NotNullなら返り値もNotNull
[return: NotNullIfNotNull("s1")]
[return: NotNullIfNotNull("s2")]
string? Or(string? s1, string? s2){
    return s1 ?? s2;
}
```

usage:
```
// src がNotNullなら dst もNotNull
void To(string? src, [NotNullIfNotNull("src")] out string? dst) { 
    dst = src; 
}
```

### DoesNotReturn
これをつけた関数は正常に値を返すことはない。

usage:
```
[DoesNotReturn]void ThrowHelper(Exception e) { throw e; }
```

usage:
```
[DoesNotReturn]void Abort() { ...; }
```


### DoesNotReturnIf
引数が `ParameterValue` と同じなら、正常に値を返すことはない。

usage:
```
void Assert([DoesNotReturnIf(false)]bool condition) { ...; }
```


その他
===

ValueTask
---

[非同期イテレータブロック](#非同期イテレータブロック) の実装に必要。
コンパイルを通すためだけの最低限の実装しかしていない。

可能なら、Microsoft製の[System.Threading.Tasks.Extensions](https://www.nuget.org/packages/System.Threading.Tasks.Extensions/)に置き換えるべき。
