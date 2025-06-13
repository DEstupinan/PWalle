public abstract class ExpresionBinaria : Expresion
{
    public Expresion Left;
    public Expresion Right;
    public ExpresionBinaria(CodeLocation location) : base(location) { }

      public override bool Check(List<Error> err)
    {
        bool right = Right.Check(err);
        bool left = Left.Check(err);
        if (Right.Type != ExpressionType.Number || Left.Type != ExpressionType.Number)
        {
             err.Add(new Error(Location, ErrorType.Invalid, "The operands must be numbers"));
            Type = ExpressionType.Error;
            return false;
        }
        Type = ExpressionType.Number;
        return right && left;
    }
}