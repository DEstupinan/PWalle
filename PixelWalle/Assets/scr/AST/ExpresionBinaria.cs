public abstract class ExpresionBinaria : Expresion
{
    public Expresion Left;
    public Expresion Right;
    public ExpresionBinaria(CodeLocation location) : base(location) { }

    
}