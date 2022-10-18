namespace DScript.Compiler
{

    /// <summary>
    /// 匹配token的数据
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

        /// <summary>
        /// 尝试匹配 marchDatas
        /// </summary>
        public MarchData(params MarchData[] marchDatas)
        {
            MarchDatas = marchDatas;
            DataType = MarchDataType.NotEssential;
        }
    }
}