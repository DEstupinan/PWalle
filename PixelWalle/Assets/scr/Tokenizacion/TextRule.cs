using System.Collections.Generic;
public class TextRule : ITokenRule
{
    public bool Match(TokenReader tr, out Token token)
    {
        CodeLocation l = tr.Location;
        if (!tr.EOF && tr.code[tr.pos] == '"')
        {
            tr.ReadAny();
            string value = "";
            while (!tr.EOF && tr.code[tr.pos] != '"')
            {
                value += tr.ReadAny();
            }
            if (!tr.EOF && tr.code[tr.pos] == '"') tr.ReadAny();
            else tr.err.Add(new Error(tr.Location, ErrorType.Expected, "Expected " +'"'+" after string " ) );
            token = new Token(TokenType.Text, value, l);
            return true;
        }
        token = null;
        return false;
        

    }
}