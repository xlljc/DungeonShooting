namespace DScript.Compiler
{
    /// <summary>
    /// 匹配token的数据, 可尝试匹配 string 或者 MarchType
    /// </summary>
    internal class MarchData
    {
        public bool IsCode;
        public string Code;
        public MarchType MarchType;

        public MarchData(string code)
        {
            Code = code;
            IsCode = true;
        }

        public MarchData(MarchType type)
        {
            MarchType = type;
            IsCode = false;
        }

        public MarchData(params MarchData[] marchDatas)
        {
            
        }
    }
}