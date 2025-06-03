public abstract class ASTNode
{
    public CodeLocation Location { get; set; }
    public abstract bool Check();
    public ASTNode(CodeLocation location)
    {
        Location = location;
    }
}