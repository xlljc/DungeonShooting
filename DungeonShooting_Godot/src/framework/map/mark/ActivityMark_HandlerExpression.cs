
using System;
using Godot;

public partial class ActivityMark
{
    /// <summary>
    /// 根据预制表达式创建物体并返回
    /// </summary>
    /// <param name="type">物体类型</param>
    /// <param name="expressionFieldName">预制表达式字段名称, 注意是字段名称, 而不是内容</param>
    public ActivityObjectResult<T> CreateActivityObjectFromExpression<T>(ActivityIdPrefix.ActivityPrefixType type, string expressionFieldName) where T : ActivityObject
    {
        if (_currentExpression.TryGetValue(expressionFieldName, out var expressionData))
        {
            if (expressionData.Id == "null")
            {
                return null;
            }
            var id = ActivityIdPrefix.GetNameByPrefixType(type) + expressionData.Id;
            var activityObject = ActivityObject.Create<T>(id);
            if (activityObject == null)
            {
                return null;
            }

            HandlerExpressionArgs(type, activityObject, expressionData);
            return new ActivityObjectResult<T>(activityObject, expressionData);
        }

        GD.PrintErr("未找到表达式字段: " + expressionFieldName + ", 请检查是否有该字段或者该字段加上了[ActivityExpression]标记");
        return null;
    }

    private void HandlerExpressionArgs(ActivityIdPrefix.ActivityPrefixType type, ActivityObject instance, ActivityExpressionData expressionData)
    {
        switch (type)
        {
            case ActivityIdPrefix.ActivityPrefixType.NonePrefix:
                break;
            case ActivityIdPrefix.ActivityPrefixType.Player:
                break;
            case ActivityIdPrefix.ActivityPrefixType.Test:
                break;
            case ActivityIdPrefix.ActivityPrefixType.Role:
                break;
            case ActivityIdPrefix.ActivityPrefixType.Enemy:
                break;
            case ActivityIdPrefix.ActivityPrefixType.Weapon:
            {
                var weapon = (Weapon)instance;
                //当前弹夹弹药量
                if (expressionData.Args.TryGetValue("CurrAmmon", out var currAmmon))
                {
                    weapon.SetCurrAmmo(int.Parse(currAmmon));
                }
                //备用弹药量
                if (expressionData.Args.TryGetValue("ResidueAmmo", out var residueAmmo))
                {
                    weapon.SetResidueAmmo(int.Parse(residueAmmo));   
                }
            }
                break;
            case ActivityIdPrefix.ActivityPrefixType.Bullet:
                break;
            case ActivityIdPrefix.ActivityPrefixType.Shell:
                break;
            case ActivityIdPrefix.ActivityPrefixType.Other:
                break;
        }
    }
}