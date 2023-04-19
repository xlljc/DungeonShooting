using System;
using System.Collections.Generic;
using DScript.Exception;

namespace DScript.Generate
{
    /// <summary>
    /// 描述类数据
    /// </summary>
    public class ClassDescribe : Describe
    {
        /// <summary>
        /// 包含的字段
        /// </summary>
        public Dictionary<string, FieldDescribe> Fields { get; } = new Dictionary<string, FieldDescribe>();
    
        /// <summary>
        /// 包含的get属性
        /// </summary>
        public Dictionary<string, GetPropertyDescribe> GetProperties { get; } =
            new Dictionary<string, GetPropertyDescribe>();

        /// <summary>
        /// 包含的set属性
        /// </summary>
        public Dictionary<string, SetPropertyDescribe> SetProperties { get; } =
            new Dictionary<string, SetPropertyDescribe>();

        /// <summary>
        /// 包含的函数
        /// </summary>
        public Dictionary<string, List<MethodDescribe>> Methods { get; } = new Dictionary<string, List<MethodDescribe>>();

        /// <summary>
        /// 父类
        /// </summary>
        public ClassDescribe BaseClass { get; set; }

        private static Dictionary<Type, ClassDescribe> _classDescribeMap = new Dictionary<Type, ClassDescribe>();

        /// <summary>
        /// 根据一个type创建一个 ClassDescribe, 并自动解析 type 中的成员
        /// </summary>
        public static ClassDescribe CreateFromType(string name, Type type)
        {
            if (_classDescribeMap.TryGetValue(type, out ClassDescribe classDescribe))
            {
                classDescribe.Name = name;
                return classDescribe;
            }

            classDescribe = new ClassDescribe(name, type);
            _classDescribeMap.Add(type, classDescribe);
            return classDescribe;
        }
    
        public ClassDescribe(string name): base(name)
        {
        
        }


        private ClassDescribe(string name, Type type) : this(name)
        {
            var fields = type.GetFields(InstanceBindFlags);
            var propertyInfos = type.GetProperties(InstanceBindFlags);
            var methodInfos = type.GetMethods(InstanceBindFlags);

            //解析字段
            foreach (var item in fields)
            {
                if (!item.IsSpecialName && !item.Name.EndsWith("k__BackingField") && !IsExclude(item.Name))
                {
                    var temp = new FieldDescribe(item.Name);
                    temp.IsPublic = item.IsPublic;
                    temp.IsStatic = item.IsPublic;
                    AddFields(temp);
                }
            }

            //解析属性
            foreach (var item in propertyInfos)
            {
                if (!item.IsSpecialName && !item.Name.EndsWith("k__BackingField") && !IsExclude(item.Name))
                {
                    if (item.GetMethod != null)
                    {
                        var temp = new GetPropertyDescribe(item.Name);
                        temp.IsPublic = item.GetMethod.IsPublic;
                        temp.IsStatic = item.GetMethod.IsPublic;
                        AddGetProperty(temp);
                    }

                    if (item.SetMethod != null)
                    {
                        var temp = new SetPropertyDescribe(item.Name);
                        temp.IsPublic = item.SetMethod.IsPublic;
                        temp.IsStatic = item.SetMethod.IsPublic;
                        AddSetProperty(temp);
                    }

                }
            }
        
            //解析函数
            foreach (var item in methodInfos)
            {
                if (!item.IsSpecialName && !item.Name.EndsWith("k__BackingField") && !IsExclude(item.Name))
                {
                    var temp = new MethodDescribe(item.Name);
                    temp.IsPublic = temp.IsPublic;
                    var paramsArr = item.GetParameters();
                    temp.ParamLength = paramsArr.Length;
                    temp.IsStatic = item.IsPublic;
                    if (paramsArr.Length > 0)
                    {
                        temp.IsDynamicParam = paramsArr[paramsArr.Length - 1].GetCustomAttributes(typeof(ParamArrayAttribute), false).Length > 0;
                    }
                    AddMethod(temp);
                }
            }

            //父类
            if (type.BaseType != null && type.BaseType != typeof(object))
            {
                BaseClass = CreateFromType(type.BaseType.Name, type.BaseType);
            }
        }


        /// <summary>
        /// 添加一个字段
        /// </summary>
        /// <exception cref="RepeatMemberException"></exception>
        public void AddFields(FieldDescribe fieldDescribe)
        {
            if (Fields.ContainsKey(fieldDescribe.Name))
            {
                throw new RepeatMemberException("Repeat defined field: " + fieldDescribe.Name);
            }

            Fields.Add(fieldDescribe.Name, fieldDescribe);
        }

        /// <summary>
        /// 添加一个get属性
        /// </summary>
        /// <exception cref="RepeatMemberException"></exception>
        public void AddGetProperty(GetPropertyDescribe getPropertyDescribe)
        {
            if (GetProperties.ContainsKey(getPropertyDescribe.Name))
            {
                throw new RepeatMemberException("Repeat defined get property: " + getPropertyDescribe.Name);
            }

            GetProperties.Add(getPropertyDescribe.Name, getPropertyDescribe);
        }

        /// <summary>
        /// 添加一个set属性
        /// </summary>
        /// <exception cref="RepeatMemberException"></exception>
        public void AddSetProperty(SetPropertyDescribe setPropertyDescribe)
        {
            if (SetProperties.ContainsKey(setPropertyDescribe.Name))
            {
                throw new RepeatMemberException("Repeat defined set property: " + setPropertyDescribe.Name);
            }

            SetProperties.Add(setPropertyDescribe.Name, setPropertyDescribe);
        }

        /// <summary>
        /// 添加一个函数
        /// </summary>
        /// <exception cref="RepeatMemberException"></exception>
        public void AddMethod(MethodDescribe methodDescribe)
        {
            List<MethodDescribe> list;
            if (Methods.ContainsKey(methodDescribe.Name))
            {
                list = Methods[methodDescribe.Name];
            }
            else
            {
                list = new List<MethodDescribe>();
                Methods.Add(methodDescribe.Name, list);
            }

            bool flag = false;
            //检查重载

            int maxLen = 16;
            int minLen = 0;
            foreach (var item in list)
            {
                if (item.IsDynamicParam)
                {
                    maxLen = item.ParamLength - 1;
                }
                else
                {
                    minLen = Math.Max(minLen, item.ParamLength + 1);
                }
            }

            if (minLen <= methodDescribe.ParamLength && methodDescribe.ParamLength <= maxLen)
            {
                list.Add(methodDescribe);
            }
            else
            {
                throw new RepeatMemberException("Function overloading fails: " + methodDescribe.Name);
            }
        
        }

    }
}