public abstract class Expresion : ASTNode
{
    public abstract void Calculate();
    public abstract ExpressionType Type { get; set; }
    public abstract object Value { get; set; }
    public Expresion(CodeLocation location) : base(location) { }
}