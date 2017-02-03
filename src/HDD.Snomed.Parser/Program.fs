module LexerProgram

open Microsoft.FSharp.Text.Lexing

let ParseSnomed x = 
    let lexbuf = LexBuffer<_>.FromString x
    try
    let y = Parser.start Lexer.tokenstream lexbuf
    y
    with _ex -> failwithf "%s\n-------------------\n%s" _ex.Message _ex.StackTrace
