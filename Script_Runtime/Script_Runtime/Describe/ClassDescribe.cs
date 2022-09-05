
using System;
using System.Collections.Generic;

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
        
    }
    
}