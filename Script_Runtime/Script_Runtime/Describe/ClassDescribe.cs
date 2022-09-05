
using System;
using System.Collections.Generic;
using System.Linq;

public class ClassDescribe : Describe
{
    public Dictionary<string, FieldDescribe> Fields { get; } = new Dictionary<string, FieldDescribe>();

    public Dictionary<string, GetPropertyDescribe> GetProperties { get; } =
        new Dictionary<string, GetPropertyDescribe>();

    public Dictionary<string, SetPropertyDescribe> SetProperties { get; } =
        new Dictionary<string, SetPropertyDescribe>();

    public Dictionary<string, List<MethodDescribe>> Methods { get; } = new Dictionary<string, List<MethodDescribe>>();


    public ClassDescribe(string name): base(name)
    {
        
    }

    public ClassDescribe(string name, Type type) : this(name)
    {
        var fields = type.GetFields(InstanceBindFlags);
        var propertyInfos = type.GetProperties(InstanceBindFlags);
        var methodInfos = type.GetMethods(InstanceBindFlags);

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

    }


    public void AddFields(FieldDescribe fieldDescribe)
    {
        if (Fields.ContainsKey(fieldDescribe.Name))
        {
            throw new RepeatMemberException("Repeat defined field: " + fieldDescribe.Name);
        }

        Fields.Add(fieldDescribe.Name, fieldDescribe);
    }

    public void AddGetProperty(GetPropertyDescribe getPropertyDescribe)
    {
        if (GetProperties.ContainsKey(getPropertyDescribe.Name))
        {
            throw new RepeatMemberException("Repeat defined get property: " + getPropertyDescribe.Name);
        }

        GetProperties.Add(getPropertyDescribe.Name, getPropertyDescribe);
    }

    public void AddSetProperty(SetPropertyDescribe setPropertyDescribe)
    {
        if (SetProperties.ContainsKey(setPropertyDescribe.Name))
        {
            throw new RepeatMemberException("Repeat defined set property: " + setPropertyDescribe.Name);
        }

        SetProperties.Add(setPropertyDescribe.Name, setPropertyDescribe);
    }

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