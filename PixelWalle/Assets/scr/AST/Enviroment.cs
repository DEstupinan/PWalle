using System.Collections.Generic;
public class Enviroment
{
    public Dictionary<string, object> variables = new Dictionary<string, object>();
    public Dictionary<string, Function> functions = new Dictionary<string, Function>();
    public Dictionary<string, ReturnFunction> returnfunctions = new Dictionary<string, ReturnFunction>();
    public Pintar p;
    public Enviroment(Pintar pintar)
    {
        this.p = pintar;
        AddD("Spawn",new Spawn(p));
        AddD("ChangeColor",new ChangeColor(p));
        AddD("DrawCircle",new DrawCircle(p));
        AddD("DrawLine",new DrawLine(p));
        AddD("DrawRectangle",new DrawRectangle(p));
        AddD("Fill",new Fill(p));
        AddD("Size",new Size(p));
        
        AddR( "GetActualX",new GetActualX(p));
        AddR( "GetActualY",new GetActualY(p));
        AddR( "GetCanvasSize",new GetCanvasSize(p));
        AddR( "GetColorCount",new GetColorCount(p));
        AddR( "IsBrushColor",new IsBrushColor(p));
        AddR( "IsCanvasColor",new IsCanvasColor(p));
        AddR( "IsBrushSize",new IsBrushSize(p));
    }
    private void AddD( string key, Function f)
    {
        functions[key] = f;
    }
    private void AddR( string key, ReturnFunction f)
    {
        returnfunctions[key] = f;
    }
    

    public object Get(string key)
    {
        if (variables.ContainsKey(key)) return variables[key];
        return null;
    }
}