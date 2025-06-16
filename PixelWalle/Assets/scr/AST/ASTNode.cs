using System.Collections.Generic;
public abstract class ASTNode
{
    public CodeLocation Location { get; set; }
    public abstract bool Check(List<Error> err);
    public ASTNode(CodeLocation location)
    {
        Location = location;
    }
}