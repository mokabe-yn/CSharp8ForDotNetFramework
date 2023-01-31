C#7.2
===
このライブラリでは、以下の機能をサポートしています。


Span
---
配列の一部だけを参照する。

Span構造体自体は以下のコードと等価。
```
ref struct Span<T>{
    ref T[] _first_ptr;
    int _length;
}
```
ただし、C#7.2時点ではref フィールド自体が存在しない。
そのためいろいろごまかした実装になっている。

Span自体が "本物" ではないため、パフォーマンスが犠牲になっている。
このライブラリでこの機能を使う利点は、
.NET系と同じコードを使えることのみになる。

usage:
```
int[] a = new int[]{0,1,2,3};
var s = a.AsSpan().Slice(1,3);
s[0]=10; // a[1]=10; と等価
```
usage:
```
string str = "teststring";
var s = str.AsSpan().Slice(2,6);
return s.ToString(); // "ststri"
```
### staticallocについて
このライブラリでは `stackalloc` をサポートしない。

`Span<int> s = stackalloc int[5];` のコードは、
`Span<T>`に `Span(void* firstpointer, int length)` コンストラクタが存在することを要求する。

引数にポインタがあるため、unsafeであることが必須になる。
このライブラリは「1ファイルをプロジェクトに追加するだけで使えるようになる」がコンセプトであるため、
プロジェクト設定にunsafeを強制することとは相反する。

よってこの機能はサポートしない。
