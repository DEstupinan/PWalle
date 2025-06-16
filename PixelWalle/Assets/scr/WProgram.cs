using System.Collections.Generic;
public class WProgram
{
    public List<Error> Errors = new List<Error>();
    public int index;
    public bool haderror = false;

    public List<Statement> stmt = new List<Statement>();
    public Enviroment Env;

    public Dictionary<string, int> Labels = new Dictionary<string, int>();
    public Parser P;

    public WProgram(Enviroment env)
    {
        Env = env;
    }

    public void EjecutarTag()
    {
        index = 0;
        if (!(stmt[0] is FunctionDeclaration f) || f.Name != "Spawn")
        {
            Errors.Add(new Error(stmt[0].Location, ErrorType.Invalid, "Expected Spawn command"));
            haderror = true;
        }
        while (index < stmt.Count)
        {

            if (stmt[index] is Label)
            {
                if (stmt[index].Check(Errors) && !haderror) stmt[index].Execute(Errors);
                else haderror = true;
            }
            index++;
        }

    }
    public void Ejecutar()
    {
        index = 0;


        while (index < stmt.Count)
        {

            if (!(stmt[index] is Label))
            {
                if (stmt[index].Check(Errors) && !haderror) stmt[index].Execute(Errors);
                else haderror = true;
            }
            index++;
        }

    }

    public void Sentenciar()
    {
        while (!P.TS.IsAtEnd())
        {
            Statement r = statement();
            if (r != null) stmt.Add(r);
        }

    }
    private Statement statement()
    {
        if (P.TS.Match(new string[] { "GoTo" })) return GoToStatement(Errors);
        if (Env.functions.ContainsKey(P.TS.Peek().Value)) return FunctionStatement(Errors);
        if (P.TS.Check(TokenType.Label)) return TagStatement(Errors);
        if (P.TS.Check(TokenType.Identifier)) return expresionStatement(Errors);

        if (P.TS.Peek().Type != TokenType.EndOfLine) Errors.Add(new Error(P.TS.Peek().Location, ErrorType.Invalid, "Semantic error"));
        while (P.TS.Peek().Type != TokenType.EndOfLine)
        {
            P.TS.Advance();
        }
        P.TS.Advance();
        return null;
    }

    private Statement GoToStatement(List<Error> err)
    {
        CodeLocation l = P.TS.Previous().Location;
        P.TS.Consume("[", "Expected '['", err);
        P.TS.Consume(TokenType.Identifier, "Expected identifier", err);
        string label = P.TS.Previous().Value.ToString();
        P.TS.Consume("]", "Expected ']'", err);

        P.TS.Consume("(", "Expected '('", err);
        Expresion left = P.ParseExpresion(err);
        P.TS.Consume(")", "Expected ')'", err);

        P.TS.Consume(TokenType.EndOfLine, "Expected end of line", err);
        return new GoTo(l, left, label, this);

    }
    private Statement FunctionStatement(List<Error> err)
    {
        CodeLocation l = P.TS.Peek().Location;
        string name = P.TS.Peek().Value;
        P.TS.Advance();
        List<Expresion> Arguments = new List<Expresion>();
        P.TS.Consume("(", "Expected '('", err);
        while (true)
        {
            if (!P.TS.Match(new string[] { ")" }))
            {
                Arguments.Add(P.ParseExpresion(err));
                if (!P.TS.Match(new string[] { "," }))
                {
                    P.TS.Consume(")", "Expected ')'", err);
                    P.TS.Consume(TokenType.EndOfLine, "Expected end of line", err);
                    break;
                }
            }
            else
            {
                P.TS.Consume(TokenType.EndOfLine, "Expected end of line", err);
                break;
            }

        }
        return new FunctionDeclaration(l, Arguments, name, Env);
    }

    private Statement TagStatement(List<Error> err)
    {
        CodeLocation l = P.TS.Peek().Location;
        string name = P.TS.Peek().Value;
        P.TS.Advance();
        P.TS.Consume(TokenType.EndOfLine, "Expected end of line", err);
        return new Label(l, name, this);
    }

    private Statement expresionStatement(List<Error> err)
    {
        string name = P.TS.Peek().Value;
        CodeLocation l = P.TS.Peek().Location;


        P.TS.Consume(TokenType.Identifier, "Expected identifier", err);
        P.TS.Consume("<-", "Expected '<-'", err);
        Expresion expr = P.ParseExpresion(err);
        P.TS.Consume(TokenType.EndOfLine, "Expected end of line", err);
        return new Declaration(l, name, expr, Env);


    }
}