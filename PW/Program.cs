Lexer lexer=new Lexer();
List<Token> e=lexer.Tokenize("ee", File.ReadAllText("./code")  );
Parser parser=new Parser(e);
/*for (int i=0; i<e.Count();i++)
{
    Console.WriteLine(e[i]);
}*/
Console.WriteLine(parser.ParseLogic().Value.ToString());