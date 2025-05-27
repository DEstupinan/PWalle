public class Lexer
{
    Dictionary<string, string> symbols = new Dictionary<string, string>();
    Dictionary<string, string> keywords = new Dictionary<string, string>();
    private List<ITokenRule> _rules;
    public Lexer()
    {
        AddtoDictionary(symbols, "+", "Sum");
        AddtoDictionary(symbols, "-", "Rest");
        AddtoDictionary(symbols, "*", "Mul");
        AddtoDictionary(symbols, "/", "Div");
        AddtoDictionary(symbols, "%", "Mod");
        AddtoDictionary(symbols, "(", "PaI");
        AddtoDictionary(symbols, ")", "PaD");
        AddtoDictionary(symbols, "<", "Men");
        AddtoDictionary(symbols, ">", "May");
        AddtoDictionary(symbols, "==", "Igu");
        AddtoDictionary(symbols, "[", "CorI");
        AddtoDictionary(symbols, "]", "CorD");
        AddtoDictionary(symbols, "&&", "And");
        AddtoDictionary(symbols, "||", "Or");
        AddtoDictionary(symbols, "**", "Pot");
        AddtoDictionary(symbols, "<=", "menig");
        AddtoDictionary(symbols, ">=", "mayotr ig");
        AddtoDictionary(symbols, "<-", "Assign");
       
        AddtoDictionary(keywords, "GoTo", "Sum");

        _rules = new List<ITokenRule>
        {
            new NumberRule(),
             new IdeKeyRule(keywords),
            new SymbolRule(symbols)
        };

    }
    private void AddtoDictionary(Dictionary<string, string> target, string key, string elemen)
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
