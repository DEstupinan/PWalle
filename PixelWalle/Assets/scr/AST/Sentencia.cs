using System.Collections.Generic;
public abstract class Statement : ASTNode
{
    
    public abstract void Execute(List<Error> err);
    public Statement(CodeLocation location) : base(location) { }
}