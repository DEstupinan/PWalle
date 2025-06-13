public class Enviroment
{
   public  Dictionary<string, object> variables=new Dictionary<string, object>() ;

    public object Get(string key)
    {
        if (variables.ContainsKey(key)) return variables[key];
        return null;
    }
}