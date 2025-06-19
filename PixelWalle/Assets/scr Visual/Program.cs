using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System;
using TMPro;
public class Program : MonoBehaviour
{
    public TMP_InputField codeInput;
    public TMP_InputField size;
    public Pintar pintar;
    public TMP_Text error;
    public Lexer lexer;
    Enviroment enviroment;
    WProgram program;
    Parser parser;
    void Start()
    {
      
    }

  

    public void Compile()
    {   
        int s=int.Parse(size.text);
        
        pintar.board.InitializeBoard(s);
        
        pintar.walle.Reset();
        error.text="";
        lexer = new Lexer();
        enviroment = new Enviroment(pintar);
        program = new WProgram(enviroment);
        string code = codeInput.text;
        List<Token> token = lexer.Tokenize(code, program.Errors);
        parser = new Parser(token, enviroment);
        program.P = parser;
       



        program.Sentenciar();
        if (program.Errors.Count() > 0)
        {
            foreach (Error x in program.Errors)
            {
                error.text += x.ToString() + "\n";
                Console.WriteLine(x);

            }
        }
        else
        {
            program.EjecutarTag();
            program.Ejecutar();
            foreach (Error x in program.Errors)
            {
                error.text += x.ToString() + "\n";
                Console.WriteLine(x);
            }
        }
    }
}
