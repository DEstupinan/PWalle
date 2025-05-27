/*public class Parser
{
    private List<Token> tokens;
    private int indiceToken;
    public Parser(List<Token> tokens)
    {
        this.tokens = tokens;
        this.indiceToken = 0;
    }

    public void Parsear()
    {
        Expresion();
    }
    private void Expresion()
    {
        Termino();
        while (indiceToken < tokens.Count && (tokens[indiceToken].Tipo == TokenType.Suma || tokens[indiceToken].Tipo == TokenType.Resta))
        {
            if (tokens[indiceToken].Tipo == TokenType.Suma)
            {
                indiceToken++;
                Termino();
            }
            else if (tokens[indiceToken].Tipo == TokenType.Resta)
            {
                indiceToken++;
                Termino();
            }
        }
    }
    private void Termino()
    {
        Factor();
        while (indiceToken < tokens.Count && (tokens[indiceToken].Tipo == TokenType.Multiplicacion || tokens[indiceToken].Tipo == TokenType.Division))
        {
            if (tokens[indiceToken].Tipo == TokenType.Multiplicacion)
            {
                indiceToken++;
                Factor();
            }
            else if (tokens[indiceToken].Tipo == TokenType.Division)
            {
                indiceToken++;
                Factor();
            }
        }
    }
    private void Factor()
    {
        if (tokens[indiceToken].Tipo == TokenType.Numero)
        {
            Console.WriteLine("Número: " + tokens[indiceToken].Valor);
            indiceToken++;
        }
        else if (tokens[indiceToken].Tipo == TokenType.Variable)
        {
            Console.WriteLine("Variable: " + tokens[indiceToken].Valor);
            indiceToken++;
        }
    }
}*/