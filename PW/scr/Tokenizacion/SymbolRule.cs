public class SymbolRule : ITokenRule
{
    private readonly Dictionary<string, string> _symbols;
    public SymbolRule(Dictionary<string, string> symbols)
    {
        _symbols = new Dictionary<string, string>(symbols);
    }
    public bool Match(TokenReader tr, out Token token)
    {
        CodeLocation l = tr.Location;
        foreach (var sy in _symbols.Keys.OrderByDescending(k => k.Length))
            if (tr.MatchCode(sy))
            {
                token = new Token(TokenType.Symbol, sy, l);
                return true;
            }
        token = null;
        return false;
    }
}