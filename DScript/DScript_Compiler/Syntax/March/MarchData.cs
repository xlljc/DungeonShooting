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
            TryMarch,
        }
        
        public MarchDataType DataType;
        public string Code;
        public MarchType MarchType;
        public MarchData[] MarchDatas;

        /// <summary>
        /// 匹配字符
        /// </summary>
        public MarchData(string code)
        {
            Code = code;
            DataType = MarchDataType.Code;
        }

        /// <summary>
        /// 根据枚举类型匹配
        /// </summary>
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
            DataType = MarchDataType.TryMarch;
        }
    }
}