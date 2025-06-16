using System.Collections.Generic;
public class Lexer
{
    Dictionary<string, SymbolType> symbols = new Dictionary<string, SymbolType>();
    Dictionary<string, KeywType> keywords = new Dictionary<string, KeywType>();
    private List<ITokenRule> _rules;
    public Lexer()
    {

        AddtoDictionary(symbols, "+", SymbolType.Sum);
        AddtoDictionary(symbols, "-", SymbolType.Rest);
        AddtoDictionary(symbols, "*", SymbolType.Mul);
        AddtoDictionary(symbols, "/", SymbolType.Di);
        AddtoDictionary(symbols, "%", SymbolType.Mo);
        AddtoDictionary(symbols, "**", SymbolType.Po);
        AddtoDictionary(symbols, "(", SymbolType.ParentIzq);
        AddtoDictionary(symbols, ")", SymbolType.ParentDer);
        AddtoDictionary(symbols, "[", SymbolType.CorchIzq);
        AddtoDictionary(symbols, "]", SymbolType.CorchDer);
        AddtoDictionary(symbols, "<-", SymbolType.Assign);
        AddtoDictionary(symbols, "<", SymbolType.Menorq);
        AddtoDictionary(symbols, "<=", SymbolType.MenorIgual);
        AddtoDictionary(symbols, ">", SymbolType.Mayorq);
        AddtoDictionary(symbols, ">=", SymbolType.MayorIgual);
        AddtoDictionary(symbols, "==", SymbolType.Igual);
        AddtoDictionary(symbols, "&&", SymbolType.And);
        AddtoDictionary(symbols, "||", SymbolType.Or);
        AddtoDictionary(symbols, "!=", SymbolType.Desigual);
        AddtoDictionary(symbols, "!", SymbolType.Neg);
        AddtoDictionary(symbols, ",", SymbolType.Coma);
        AddtoDictionary(symbols, '"'.ToString(), SymbolType.Comilla);
        AddtoDictionaryK(keywords, "GoTo", KeywType.Goto);
        AddtoDictionaryK(keywords, "ChangeColor", KeywType.Goto);
        AddtoDictionaryK(keywords, "DrawLine", KeywType.Goto);
        AddtoDictionaryK(keywords, "DrawCircle", KeywType.Goto);
        AddtoDictionaryK(keywords, "DrawRectangle", KeywType.Goto);
        AddtoDictionaryK(keywords, "Fill", KeywType.Goto);
        AddtoDictionaryK(keywords, "Size", KeywType.Goto);
        AddtoDictionaryK(keywords, "Spawn", KeywType.Goto);
        AddtoDictionaryK(keywords, "GetActualX", KeywType.Goto);
        AddtoDictionaryK(keywords, "GetActualY", KeywType.Goto);
        AddtoDictionaryK(keywords, "GetCanvasSize", KeywType.Goto);
        AddtoDictionaryK(keywords, "GetColorCount", KeywType.Goto);
        AddtoDictionaryK(keywords, "IsBrushColor", KeywType.Goto);
        AddtoDictionaryK(keywords, "IsCanvasColor", KeywType.Goto);
        AddtoDictionaryK(keywords, "IsBrushSize", KeywType.Goto);


        _rules = new List<ITokenRule>
        {
            new NumberRule(),
            new TextRule(),
            new IdeKeyRule(keywords),
            new SymbolRule(symbols),

        };

    }
    private void AddtoDictionary(Dictionary<string, SymbolType> target, string key, SymbolType elemen)
    {
        target[key] = elemen;
    }
    private void AddtoDictionaryK(Dictionary<string, KeywType> target, string key, KeywType elemen)
    {
        target[key] = elemen;
    }

    public List<Token> Tokenize( string input, List<Error> err)
    {
        var tokens = new List<Token>();
        TokenReader TR = new TokenReader( input, err);

        while (!TR.EOF)
        {
            if (TR.EOL)
            {
                tokens.Add(new Token(TokenType.EndOfLine, "", TR.Location));
                TR.ReadAny();
                continue;
            }
            if (char.IsWhiteSpace(TR.code[TR.pos]))
            {
                TR.ReadAny();
                continue;
            }

            bool matched = false;

            foreach (var rule in _rules)
            {
                if (rule.Match(TR, out Token token))
                {
                    tokens.Add(token);

                    matched = true;
                    break;
                }
            }

            if (!matched)
            {

                tokens.Add(new Token(TokenType.Unknown, input[TR.pos].ToString(), TR.Location));
                err.Add(new Error(TR.Location, ErrorType.Invalid, "Invalid character"));
                TR.ReadAny();
            }
        }
        tokens.Add(new Token(TokenType.EndOfLine, "", TR.Location));
        tokens.Add(new Token(TokenType.EndOfFile, "", TR.Location));
        return tokens;
    }

}
public class TokenReader

{
    
    public string code;
    public int pos;
    int line;
    int lastLB;
    public List<Error> err;

    public TokenReader( string code, List<Error> err)
    {
        
        this.code = code;
        this.pos = 0;
        this.line = 1;
        this.lastLB = -1;
        this.err = err;
    }

    public CodeLocation Location
    {
        get
        {
            return new CodeLocation
            {
                
                Line = line,
                Column = pos - lastLB
            };
        }
    }
    public bool EOF
    {
        get { return pos >= code.Length; }
    }

    public bool EOL
    {
        get { return EOF || code[pos] == '\n'; }
    }
    public bool MatchCode(string prefix)
    {
        if (pos + prefix.Length > code.Length)
            return false;
        for (int i = 0; i < prefix.Length; i++)
            if (code[pos + i] != prefix[i])
                return false;
        pos += prefix.Length;
        return true;

    }


    public char ReadAny()
    {


        if (EOL)
        {
            line++;
            lastLB = pos;
        }

        return code[pos++];

    }
}
