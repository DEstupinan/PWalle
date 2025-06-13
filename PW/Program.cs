Lexer lexer=new Lexer();
Enviroment enviroment=new Enviroment();
WProgram program=new WProgram(enviroment);
List<Token> e=lexer.Tokenize("ee", File.ReadAllText("./code"),program.Errors  );
Parser parser=new Parser(e,enviroment);

program.P=parser;


 

/*for (int i=0; i<e.Count();i++)
{
    Console.WriteLine(e[i]);
}*/
//program.Ejecutar();
program.Sentenciar();
if(program.Errors.Count()>0)
{
    foreach(Error x in program.Errors)
    {
        Console.WriteLine(x);
    }
}
else program.Ejecutar();



