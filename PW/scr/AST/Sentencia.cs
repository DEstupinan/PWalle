public abstract class Statement : ASTNode
{
    
    public abstract void Execute();
    public Statement(CodeLocation location) : base(location) { }
}