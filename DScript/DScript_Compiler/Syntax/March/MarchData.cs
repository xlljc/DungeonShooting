namespace DScript.Compiler
{

    /// <summary>
    /// 匹配token的数据, 可尝试匹配 string 或者 MarchType
    /// </summary>
    internal class MarchData
    {
        public enum MarchDataType
        {
            Code,
            MarchType,
            NotEssential,
        }
        
        public MarchDataType DataType;
        public string Code;
        public MarchType MarchType;
        public MarchData[] MarchDatas;

        public MarchData(string code)
        {
            Code = code;
            DataType = MarchDataType.Code;
        }

        public MarchData(MarchType type)
        {
            MarchType = type;
            DataType = MarchDataType.MarchType;
        }

        public MarchData(params MarchData[] marchDatas)
        {
            MarchDatas = marchDatas;
            DataType = MarchDataType.NotEssential;
        }
    }
}