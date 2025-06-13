public class Literal : ExpresionUnitaria
{
    public Enviroment Env;
    public string Name;
    public override ExpressionType Type
    {
        get
        {
            return ExpressionType.Text;
        }
        set { }
    }

    public override object Value { get; set; }

    public Literal(string name, Enviroment env, CodeLocation location) : base(location)
    {
        Name = name;
        Env = env;
      
    }

    public override bool Check(List<Error> err)
    {
        return true;
    }
    public override void Calculate()
    {
        Value=Env.Get(Name);
    }
}