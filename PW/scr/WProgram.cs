public class WProgram
{
    public List<Error> Errors = new List<Error>();
    public int index;
    public List<Statement> stmt = new List<Statement>();
    public Enviroment Env;

    public Dictionary<string, int> Labels = new Dictionary<string, int>();
    public Parser P;

    public WProgram(Enviroment env)
    {
        Env = env;
    }
    Check
    public void Ejecutar()
    {
        index = 0;

        while (index < stmt.Count)
        {
            stmt[index].Execute();
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
        if (P.TS.Match(new string[] { "GoTo" }))
        {
            return bucleStatement(Errors);
        }
        return expresionStatement(Errors);
    }

    private Statement bucleStatement(List<Error> err)
    {
        CodeLocation l = P.TS.Peek().Location;
        P.TS.Consume("[", "Expected '['", err);
        P.TS.Consume(TokenType.Identifier, "Expected identifier", err);
        string label = P.TS.Previous().Value.ToString();
        P.TS.Consume("]", "Expected ']'", err);

        P.TS.Consume("(", "Expected '('", err);
        Expresion left = P.ParseExpresion(err);
        P.TS.Consume(")", "Expected ')'", err);

        P.TS.Consume(TokenType.EndOfLine, "Expected end of line", err);
        return new GoTo(l, left, label, this);

        /*  P.TS.Consume(TokenType.Identifier, "Expected identifier", err);
          P.TS.Consume("[", "Expected '['", err);
          P.TS.Consume(TokenType.Identifier, "Expected identifier", err);
          if (P.TS.Match(new string[] { "[" }))
          {
              if (P.TS.Peek().Type == TokenType.Identifier)
              {
                  string label = P.TS.Peek().Value.ToString();
                  P.TS.Advance();
                  if (P.TS.Check("]"))
                  {
                      P.TS.Advance();
                      if (P.TS.Check("("))
                      {
                          P.TS.Advance();

                          Expresion left = P.ParseExpresion(err);

                          //if (left.Value is bool)
                          // {
                          if (P.TS.Check(")"))
                          {
                              P.TS.Advance();
                              return new GoTo(l, left, label, this);
                          }
                          err.Add(new Error(P.TS.Peek().Location, ErrorType.Expected, "Expected ')'"));
                          return null;
                          // }
                          // err.Add(new Error(P.TS.Peek().Location, ErrorType.Expected, "Expected bool expresion"));
                          // return null;

                      }
                      err.Add(new Error(P.TS.Peek().Location, ErrorType.Expected, "Expected '(' "));
                      return null;
                  }
                  err.Add(new Error(P.TS.Peek().Location, ErrorType.Expected, "Expected ']'"));
                  return null;
              }
              err.Add(new Error(P.TS.Peek().Location, ErrorType.Expected, "Expected label"));
              return null;
          }
          err.Add(new Error(P.TS.Peek().Location, ErrorType.Expected, "Expected '['"));

          return null;*/
    }

    private Statement expresionStatement(List<Error> err)
    {
        string name = P.TS.Peek().Value;
        CodeLocation l = P.TS.Peek().Location;
        if (P.TS.Peek().Type == TokenType.Label)
        {
            P.TS.Advance();
            Labels[name] = stmt.Count();
            P.TS.Consume(TokenType.EndOfLine, "Expected end of line", err);
            return new Label(l, name, this);
        }
        if (P.TS.Peek().Type == TokenType.Identifier)
        {
            P.TS.Advance();
            P.TS.Consume("<-", "Expected '<-'", err);
            Expresion expr = P.ParseExpresion(err);
            P.TS.Consume(TokenType.EndOfLine, "Expected end of line", err);
            return new Declaration(l, name, expr, Env);
        }
        P.TS.Advance();
        return null;
        /*  if (P.TS.Peek().Type == TokenType.Identifier)
          {

              P.TS.Advance();

              if (P.TS.Match(new string[] { "<-" }))
              {
                  Expresion expr = P.ParseExpresion();
                  if (expr != null)
                  {
                      if (P.TS.Peek().Type != TokenType.EndOfLine) { };
                      P.TS.Advance();
                      return new Declaration(l, name, expr, Env);
                  }
                  else
                  {
                      err.Add(new Error(l, ErrorType.Expected, "Expected expression after '<-'"));
                      return null;
                  }
              }
              else
              {
                  err.Add(new Error(l, ErrorType.Expected, $"Expected '<-' after identifier '{name}'"));
                  return null;
              }
          }
          P.TS.Advance();
          return null;
  */

    }
}