namespace DScript.Compiler
{
    public class Token
    {
        public override string ToString()
        {
            return $"{nameof(Code)}: {Code}, {nameof(Index)}: {Index}, {nameof(Length)}: {Length}, {nameof(Row)}: {Row}, {nameof(Column)}: {Column}, {nameof(Type)}: {Type}";
        }

        public Token(string code, int index, int length, int row, int column, TokenType type)
        {
            Code = code;
            Index = index;
            Length = length;
            Row = row;
            Column = column;
            Type = type;
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

        public int Row;
        public int Column;
        public TokenType Type;
    }
}