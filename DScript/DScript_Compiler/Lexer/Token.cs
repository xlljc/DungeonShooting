namespace DScript.Compiler
{
    public class Token
    {
        public override string ToString()
        {
            return $"{nameof(Code)}: {Code}, {nameof(Index)}: {Index}, {nameof(Length)}: {Length}";
        }

        public Token(string code, int index, int length)
        {
            Code = code;
            Index = index;
            Length = length;
        }

        public string Code;
        public int Index;
        public int Length;
    }
}