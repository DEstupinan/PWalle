using System.Collections.Generic;
public class Parser
{

    public List<Token> tokens;
    public Enviroment Env;
    public TokenStream TS;
    public Parser(List<Token> tokens, Enviroment env)
    {
        this.tokens = tokens;
        Env = env;

        TS = new TokenStream(tokens);
    }

    public Expresion ParseExpresion(List<Error> err)
    {
        return Disyuntiva();

        Expresion Disyuntiva()
        {
            Expresion left = Copulativa();
            while (TS.Match(new string[] { "||" }))

            {
                ExpresionBinaria r = new Or(TS.Previous().Location);

                Expresion right = Copulativa();
                r.Left = left;
                r.Right = right;

                left = r;
            }
            return left;
        }
        Expresion Copulativa()
        {
            Expresion left = Igualdad();
            while (TS.Match(new string[] { "&&" }))

            {
                ExpresionBinaria r = new And(TS.Previous().Location);
                Expresion right = Igualdad();
                r.Left = left;
                r.Right = right;

                left = r;
            }
            return left;
        }
        Expresion Igualdad()
        {
            Expresion left = Comparación();
            while (TS.Match(new string[] { "==", "!=" }))

            {
                ExpresionBinaria r = new Igual(TS.Previous().Location);
                switch (TS.Previous().Value.ToString())
                {
                    case "!=": r = new Desigual(TS.Previous().Location); break;
                    default: break;
                }
                Expresion right = Comparación();
                r.Left = left;
                r.Right = right;

                left = r;
            }
            return left;
        }
        Expresion Comparación()
        {
            Expresion left = Termino();
            while (TS.Match(new string[] { "<", "<=", ">", ">=" }))

            {
                ExpresionBinaria r = new Mayor(TS.Previous().Location);
                switch (TS.Previous().Value.ToString())
                {
                    case "<": r = new Menor(TS.Previous().Location); break;
                    case ">=": r = new MayorIgual(TS.Previous().Location); break;
                    case "<=": r = new MenorIgual(TS.Previous().Location); break;
                    default: break;
                }
                Expresion right = Termino();
                r.Left = left;
                r.Right = right;

                left = r;
            }
            return left;
        }
        Expresion Termino()
        {
            Expresion left = Factor();
            while (TS.Match(new string[] { "+", "-" }))

            {
                ExpresionBinaria r = new Suma(TS.Previous().Location);
                switch (TS.Previous().Value.ToString())
                {
                    case "-": r = new Resta(TS.Previous().Location); break;
                    default: break;
                }
                Expresion right = Factor();
                r.Left = left;
                r.Right = right;

                left = r;
            }
            return left;
        }
        Expresion Factor()
        {
            Expresion left = Elev();
            while (TS.Match(new string[] { "*", "/", "%" }))

            {
                ExpresionBinaria r = new Mult(TS.Previous().Location);
                switch (TS.Previous().Value.ToString())
                {
                    case "/": r = new Div(TS.Previous().Location); break;
                    case "%": r = new Mod(TS.Previous().Location); break;
                    default: break;
                }
                Expresion right = Elev();
                r.Left = left;
                r.Right = right;

                left = r;
            }
            return left;
        }
        Expresion Elev()
        {
            Expresion left = Unary();
            while (TS.Match(new string[] { "**" }))

            {
                ExpresionBinaria r = new Pow(TS.Previous().Location);
                Expresion right = Unary();
                r.Left = left;
                r.Right = right;

                left = r;
            }
            return left;
        }
        Expresion Unary()
        {

            if (TS.Match(new string[] { "-", "!" }))

            {
                ExpresionUnary r = new Negative(TS.Previous().Location);


                switch (TS.Previous().Value.ToString())
                {
                    case "!": r = new LogicNegative(TS.Previous().Location); break;
                    default: break;
                }
                Expresion right = Unary();
                r.Right = right;
                right = r;
                return right;

            }
            return Primario();
        }
        Expresion Primario()
        {
            if (TS.Peek().Type == TokenType.Keyword)
            {
                return ParseFunction();
            }
            if (TS.Peek().Type == TokenType.Number)
            {
                Number Left = new Number(int.Parse(TS.Peek().Value), TS.Peek().Location);
                TS.Advance();
                return Left;

            }
            if (TS.Peek().Type == TokenType.Text)
            {
                Text Left = new Text(TS.Peek().Value, TS.Peek().Location);
                TS.Advance();
                return Left;

            }
            if (TS.Peek().Type == TokenType.Identifier)
            {
                Literal Left = new Literal(TS.Peek().Value.ToString(), Env, TS.Peek().Location);
                TS.Advance();
                return Left;

            }
            if (TS.Match(new string[] { "(" }))

            {
                Expresion left = ParseExpresion(err);
                TS.Consume(")", "Expect ')' after expression.", err);
                return left;
            }
            err.Add(new Error(TS.Peek().Location, ErrorType.Expected, "Expect expression"));
            return null;
        }
        Expresion ParseFunction()
        {
            CodeLocation l = TS.Peek().Location;
            string name = TS.Peek().Value;
            TS.Advance();
            List<Expresion> Arguments = new List<Expresion>();
            TS.Consume("(", "Expected '('", err);
            while (true)
            {
                if (!TS.Match(new string[] { ")" }))
                {
                    Arguments.Add(ParseExpresion(err));
                    if (!TS.Match(new string[] { "," }))
                    {
                        TS.Consume(")", "Expected ')'", err);


                    }

                }
                break;
            }
            return new ReturnFunctionCall(l, Arguments, name, Env);
        }
    }

}
public class TokenStream
{
    private List<Token> tokens;
    private int position;
    public TokenStream(List<Token> tokens)
    {
        this.tokens = tokens;
        position = 0;
    }
    public bool Match(string[] types)
    {
        foreach (string x in types)
        {
            if (Check(x))
            {
                Advance();
                return true;

            }
        }
        return false;
    }
    public Token Consume(string type, string mensaje, List<Error> err)
    {
        if (Check(type)) return Advance();

        err.Add(new Error(Peek().Location, ErrorType.Expected, mensaje));
        return null;
    }
    public Token Consume(TokenType type, string mensaje, List<Error> err)
    {
        if (Check(type)) return Advance();

        err.Add(new Error(Peek().Location, ErrorType.Expected, mensaje));
        return null;
    }

    public bool Check(string type)
    {
        if (IsAtEnd()) return false;
        return Peek().Value.ToString() == type;

    }
    public bool Check(TokenType type)
    {
        if (IsAtEnd()) return false;
        return Peek().Type == type;

    }


    public Token Peek()
    {
        return tokens[position];
    }
    public Token Previous()
    {
        return tokens[position - 1];
    }
    public Token Advance()
    {
        if (!IsAtEnd()) position++;
        return Previous();
    }
    public bool IsAtEnd()
    {
        return Peek().Type == TokenType.EndOfFile;

    }


}
