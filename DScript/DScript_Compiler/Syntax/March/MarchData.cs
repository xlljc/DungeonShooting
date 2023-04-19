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
        
        /// <summary>
        /// 匹配类型
        /// </summary>
        public MarchDataType MarchType;
        
        /// <summary>
        /// 匹配单词
        /// </summary>
        public string Code;
        /// <summary>
        /// 根据 MarchType 枚举匹配
        /// </summary>
        public MarchType Type;
        /// <summary>
        /// 尝试匹配
        /// </summary>
        public MarchData[] TryMarch;

        private MarchData()
        {
        }
        
        /// <summary>
        /// 匹配字符
        /// </summary>
        public static MarchData FromCode(string code)
        {
            var marchData = new MarchData();
            marchData.Code = code;
            marchData.MarchType = MarchDataType.Code;
            return marchData;
        }

        /// <summary>
        /// 根据枚举类型匹配
        /// </summary>
        public static MarchData FromType(MarchType type)
        {
            var marchData = new MarchData();
            marchData.Type = type;
            marchData.MarchType = MarchDataType.MarchType;
            return marchData;
        }

        /// <summary>
        /// 尝试匹配 marchData
        /// </summary>
        public static MarchData FromTryMarch(params MarchData[] marchArray)
        {
            var marchData = new MarchData();
            marchData.TryMarch = marchArray;
            marchData.MarchType = MarchDataType.TryMarch;
            return marchData;
        }
    }
}