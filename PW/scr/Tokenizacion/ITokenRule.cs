public interface ITokenRule
{
     bool Match(TokenReader tr, out Token token);
}