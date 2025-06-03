public class NumberRule : ITokenRule
{
    public bool Match(TokenReader tr, out Token token)
    {
        CodeLocation l = tr.Location;
        string value = "";
        while (!tr.EOF && char.IsDigit(tr.code[tr.pos]))
        {
            value += tr.ReadAny();
        }
        if (value != "")
        {

            token = new Token(TokenType.Number, value, l);
            return true;
        }
        token = null;
        return false;
    }
}
