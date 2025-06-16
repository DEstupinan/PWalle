public class Token
{
    public string Value { get; private set; }
    public TokenType Type { get; private set; }
    public CodeLocation Location { get; private set; }
    public Token(TokenType type, string value, CodeLocation location)
    {
        this.Type = type;
        this.Value = value;
        this.Location = location;
    }
    public override string ToString() => $"{Type} ('{Value}') at {Location.Column}";
}

public struct CodeLocation
{

    
    public int Line;
    public int Column;
}


public enum TokenType
{
    Unknown,
    Number,
    Text,
    Keyword,
    Identifier,
    Label,
    Symbol,
    EndOfLine,
    EndOfFile,
}

