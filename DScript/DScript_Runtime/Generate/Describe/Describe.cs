
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace DScript.Generate
{
    public abstract class Describe
    {

        private static List<string> ExcludeMember = new List<string>();

        protected const BindingFlags InstanceBindFlags =
            BindingFlags.Instance | BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly;

        public Describe(string name)
        {
            Name = name;
        }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; protected set; }

        /// <summary>
        /// 返回成员是否被排除
        /// </summary>
        public bool IsExclude(string member)
        {
            return ExcludeMember.Contains(member);
        }

        /// <summary>
        /// 静态构造, 初始化
        /// </summary>
        static Describe()
        {
            ExcludeMember.Add("ToString");
            ExcludeMember.Add("GetType");
            ExcludeMember.Add("Equals");
            ExcludeMember.Add("GetHashCode");

            var type = typeof(OldIObject);
            var fields = type.GetFields(InstanceBindFlags)
                .Where(m => !m.IsSpecialName).ToArray();
            var propertyInfos = type.GetProperties(InstanceBindFlags)
                .Where(m => !m.IsSpecialName).ToArray();
            var methods = type.GetMethods(InstanceBindFlags)
                .Where(m => !m.IsSpecialName).ToArray();
            foreach (var item in fields)
            {
                if (!item.Name.EndsWith("k__BackingField") && !ExcludeMember.Contains(item.Name))
                {
                    ExcludeMember.Add(item.Name);
                }
            }

            foreach (var item in propertyInfos)
            {
                if (!item.Name.EndsWith("k__BackingField") && !ExcludeMember.Contains(item.Name))
                {
                    ExcludeMember.Add(item.Name);
                }
            }

            foreach (var item in methods)
            {
                if (!item.Name.EndsWith("k__BackingField") && !ExcludeMember.Contains(item.Name))
                {
                    ExcludeMember.Add(item.Name);
                }
            }
        }
    }
}