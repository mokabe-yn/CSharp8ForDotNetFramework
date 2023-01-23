C#10
===

このライブラリでは、以下の機能をサポートしています。


呼び出し元情報
----
呼び出し元が与えた引数の式を文字列として取得する。
`CallerLineNumber`, `CallerFilePath`, `CallerMemberName` は C#5からあるが、
`CallerArgumentExpression` がC#10で追加された。


usage:
```
// using System.Runtime.CompilerServices;
static void DebugPrint<T>(
    T? value,
    [CallerLineNumber] int line = 0,
    [CallerFilePath] string? file = null,
    [CallerMemberName] string? from_member= null,
    [CallerArgumentExpression("value")] string? expression = null) {...}
```

