using System.Collections.Generic;
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
        bool leb = false;
        int i = tr.pos;

        if (i == 0 || tr.code[i - 1].ToString() == "\n") leb = true;
        while (i != 0)
        {
            if (tr.code[i-1 ].ToString() == "\n")
            {
                leb = true;
                break;
            }
            if (char.IsWhiteSpace(tr.code[i-1]))
            {
                i--;
                continue;
            }
            break;
        }
        if (!tr.EOF && (char.IsLetter(tr.code[tr.pos]) || tr.code[tr.pos] == '_'))
        {
            value += tr.ReadAny();
            while (!tr.EOF && (char.IsLetterOrDigit(tr.code[tr.pos]) || tr.code[tr.pos] == '_'))
            {
                value += tr.ReadAny();
            }

            TokenType type = TokenType.Identifier;
            if (leb)
            {
                if (tr.EOF)
                {
                    type = TokenType.Label;

                }
                while (!tr.EOF)
                {
                    if (tr.EOL)
                    {
                        type = TokenType.Label;
                        break;
                    }
                    if (char.IsWhiteSpace(tr.code[tr.pos]))
                    {
                        tr.ReadAny();
                        continue;
                    }
                    break;
                }
            }



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

