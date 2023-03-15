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
    public UiNode2_c1 c1
    {
        get
        {
            if (_c1 == null) _c1 = new UiNode2_c1(GetNodeOrNull<Control>("c1"));
            return _c1;
        }
    }
    public UiNode3_c2 c2
    {
        get
        {
            if (_c2 == null) _c2 = new UiNode3_c2(GetNodeOrNull<Control>("c2"));
            return _c2;
        }
    }

    private UiNode2_c1 _c1;
    private UiNode3_c2 _c2;
    
    public class UiNode1_c11 : IUiNode<Control, UiNode1_c11>
    {
        public Control Instance { get; }

        public UiNode1_c11(Control node)
        {
            Instance = node;
        }

        public UiNode1_c11 Clone()
        {
            return new UiNode1_c11((Control)Instance.Duplicate());
        }
    }
    
    public class UiNode2_c1 : IUiNode<Control, UiNode2_c1>
    {
        public Control Instance { get; }

        public UiNode1_c11 c11
        {
            get
            {
                if (_c11 == null) _c11 = new UiNode1_c11(Instance.GetNodeOrNull<Control>("c11"));
                return _c11;
            }
        }

        private UiNode1_c11 _c11;

        public UiNode2_c1(Control node)
        {
            Instance = node;
        }

        public UiNode2_c1 Clone()
        {
            return new UiNode2_c1((Control)Instance.Duplicate());
        }
    }
    
    public class UiNode3_c2 : IUiNode<Control, UiNode3_c2>
    {
        public Control Instance { get; }

        public UiNode3_c2(Control node)
        {
            Instance = node;
        }

        public UiNode3_c2 Clone()
        {
            return new UiNode3_c2((Control)Instance.Duplicate());
        }
    }
}