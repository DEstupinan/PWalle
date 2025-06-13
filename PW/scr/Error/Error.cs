public class Error
    {
        public ErrorType Type { get; private set; }

        public string Argument { get; private set; }

        public CodeLocation Location {get; private set;}

        public Error(CodeLocation location, ErrorType type, string argument)
        {
            Type=type;
            Argument = argument;
            Location = location;
        }
        public override string ToString() => $"[Row.{Location.Line},Col.{Location.Column}] {Argument}";
    }

    public enum ErrorType
    {
        None,
        Expected,
        Invalid,
        Unknown,
    }