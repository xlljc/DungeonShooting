using Godot;

namespace UI;


/*
 Test
    c1
        c11
    c2
 */
public abstract partial class Test : UiBase
{
    public UiNode2_c1 c1 { get; private set; }
    public UiNode3_c2 c2 { get; private set; }
    
    public class UiNode1_c11
    {
        public Control Instance { get; private set; }

        public UiNode1_c11(Control node)
        {
            Instance = node;
        }

        public UiNode1_c11 Clone()
        {
            return new UiNode1_c11((Control)Instance.Duplicate());
        }
    }
    
    public class UiNode2_c1
    {
        public Control Instance { get; private set; }
        public UiNode1_c11 c11 { get; private set; }

        public UiNode2_c1(Control node)
        {
            Instance = node;
            c11 = new UiNode1_c11(node.GetNode<Control>("c11"));
        }

        public UiNode2_c1 Clone()
        {
            return new UiNode2_c1((Control)Instance.Duplicate());
        }
    }
    
    public class UiNode3_c2
    {
        public Control Instance { get; private set; }

        public UiNode3_c2(Control node)
        {
            Instance = node;
        }

        public UiNode3_c2 Clone()
        {
            return new UiNode3_c2((Control)Instance.Duplicate());
        }
    }

    public sealed override void _Ready()
    {
        c1 = new UiNode2_c1(GetNode<Control>("c1"));
        c2 = new UiNode3_c2(GetNode<Control>("c2"));
    }
}