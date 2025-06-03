public abstract class ExpresionBinaria : Expresion
{
    public Expresion Left;
    public Expresion Right;
    public ExpresionBinaria(CodeLocation location) : base(location) { }

      public override bool Check()
    {
        bool right = Right.Check();
        bool left = Left.Check();
        if (Right.Type != ExpressionType.Number || Left.Type != ExpressionType.Number)
        {
            Type = ExpressionType.Error;
            return false;
        }
        Type = ExpressionType.Number;
        return right && left;
    }
}