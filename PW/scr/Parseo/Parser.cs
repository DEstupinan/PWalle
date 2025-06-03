public class Parser
{

    private List<Token> tokens;
    private int indiceToken;
    public Parser(List<Token> tokens)
    {
        this.tokens = tokens;
        this.indiceToken = 0;
    }
    private string ct
    {
        get { return tokens[indiceToken].Value.ToString(); }
    }
    private CodeLocation cl
    {
        get { return tokens[indiceToken].Location; }
    }

    public Expresion ParseLogic()
    {
        return FirstLogic();
    }
    private Expresion FirstLogic()
    {
        Expresion left = SecondLogic();
        while (indiceToken < tokens.Count && ct == "||")
        {
            ExpresionBinaria r = new Or(cl);
            r.Left = left;
            indiceToken++;
            Expresion right = SecondLogic();
            r.Right = right;
            r.Calculate();
            left.Value = r.Value;
        }
        return left;
    }

    private Expresion SecondLogic()
    {
        Expresion left = BasicLogic();
        while (indiceToken < tokens.Count && ct == "&&")
        {
            ExpresionBinaria r = new And(cl);
            r.Left = left;
            indiceToken++;
            Expresion right = BasicLogic();
            r.Right = right;
            r.Calculate();
            left.Value = r.Value;
        }
        return left;
    }
    private Expresion BasicLogic()
    {
        Expresion left = ParseExpresion();
        while (indiceToken < tokens.Count && (ct == "<" || ct == "<=" || ct == ">" || ct == ">=" || ct == "=="))
        {

            ExpresionBinaria r = new Igual(cl);
            switch (ct)
            {
                case ">": r = new Mayor(cl); break;
                case "<": r = new Menor(cl); break;
                case ">=": r = new MayorIgual(cl); break;
                case "<=": r = new MenorIgual(cl); break;
                default: break;
            }
            r.Left = left;
            indiceToken++;
            Expresion right = ParseExpresion();
            r.Right = right;
            r.Calculate();
            left.Value = r.Value;
        }
        return left;
    }
    public Expresion ParseExpresion()
    {
        return BasicExpresion();
    }
    private Expresion BasicExpresion()
    {
        Expresion left = Termino();
        while (indiceToken < tokens.Count && (ct == "+" || ct == "-"))
        {
            ExpresionBinaria r = new Suma(cl);
            if (ct == "-")
            {
                r = new Resta(cl);
            }
            r.Left = left;
            indiceToken++;
            Expresion right = Termino();
            r.Right = right;
            r.Calculate();
            left.Value = r.Value;
        }
        return left;
    }
    private Expresion Termino()
    {
        Expresion left = Potencia();
        while (indiceToken < tokens.Count && (ct == "*" || ct == "/" || ct == "%"))
        {
            ExpresionBinaria r = new Mult(cl);
            r = new Mult(cl);
            if (ct == "/")
            {
                r = new Div(cl);
            }
            if (ct == "%")
            {
                r = new Mod(cl);
            }
            r.Left = left;
            indiceToken++;
            Expresion right = Potencia();
            r.Right = right;
            r.Calculate();
            left.Value = r.Value;
        }
        return left;
    }
    private Expresion Potencia()
    {
        Expresion left = Factor();
        while (indiceToken < tokens.Count && ct == "**")
        {
            ExpresionBinaria r = new Pow(cl);
            r.Left = left;
            indiceToken++;
            Expresion right = Factor();
            r.Right = right;
            r.Calculate();
            left.Value = r.Value;
        }
        return left;
    }

    private Expresion Factor()
    {
        if (tokens[indiceToken].Type == TokenType.Number)
        {
            Number Left = new Number(double.Parse(tokens[indiceToken].Value), cl);
            indiceToken++;
            return Left;
        }
        return null;
    }
}