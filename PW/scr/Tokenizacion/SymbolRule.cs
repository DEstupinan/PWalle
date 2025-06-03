public class SymbolRule : ITokenRule
{
    private readonly Dictionary<string, SymbolType> _symbols;
    public SymbolRule(Dictionary<string, SymbolType> symbols)
    {
        _symbols = new Dictionary<string, SymbolType>(symbols);
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