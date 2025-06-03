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
        AddtoDictionaryK(keywords, "GoTo", KeywType.Goto);

        _rules = new List<ITokenRule>
        {
            new NumberRule(),
             new IdeKeyRule(keywords),
            new SymbolRule(symbols)
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

    public List<Token> Tokenize(string fileName, string input)
    {
        var tokens = new List<Token>();
        TokenReader TR = new TokenReader(fileName, input);

        while (!TR.EOF)
        {
            if (char.IsWhiteSpace(input[TR.pos]))
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
                TR.ReadAny();
            }
        }

        tokens.Add(new Token(TokenType.EndOfFile, string.Empty, TR.Location));
        return tokens;
    }

}
public class TokenReader

{
    string FileName;
    public string code;
    public int pos;
    int line;
    int lastLB;

    public TokenReader(string fileName, string code)
    {
        this.FileName = fileName;
        this.code = code;
        this.pos = 0;
        this.line = 1;
        this.lastLB = -1;
    }

    public CodeLocation Location
    {
        get
        {
            return new CodeLocation
            {
                File = FileName,
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
