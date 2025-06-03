public class IdeKeyRule : ITokenRule
{
    private Dictionary<string, KeywType> _keywords;

    public IdeKeyRule(Dictionary<string, KeywType> keywords)
    {
        _keywords = new Dictionary<string, KeywType>(keywords);
    }

    public bool Match(TokenReader tr, out Token token)
    {
        string value = "";
        CodeLocation l = tr.Location;
        if (!tr.EOF && (char.IsLetter(tr.code[tr.pos]) || tr.code[tr.pos] == '_'))
        {
            value += tr.ReadAny();
            while (!tr.EOF && (char.IsLetterOrDigit(tr.code[tr.pos]) || tr.code[tr.pos] == '_'))
            {
                value += tr.ReadAny();
            }

            TokenType type = TokenType.Identifier;

            foreach (var ke in _keywords.Keys)
            {
                if (ke == value)
                {
                    type = TokenType.Keyword;
                    break;
                }
            }


            token = new Token(type, value, l);
            return true;
        }
        token = null;
        return false;
    }
}

