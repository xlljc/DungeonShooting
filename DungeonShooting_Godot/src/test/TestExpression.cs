using Godot;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public partial class TestExpression : Node2D
{
    [Export(PropertyHint.Expression)]
    public string Str;

    public override void _Ready()
    {
        //var expressions = Pretreatment(Str);
        
    }

    
}
