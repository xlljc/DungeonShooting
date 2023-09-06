#if TOOLS
using System;
using System.Collections.Generic;
using Godot;

namespace Plugin;

/// <summary>
/// 场景监听器, 一旦当前节点内容发生改变, 则直接调用 SceneNodeChangeEvent 事件
/// </summary>
public class NodeMonitor
{
    private class SceneNode : IEquatable<SceneNode>
    {
        public SceneNode(string type, string name, string scriptPath)
        {
            Type = type;
            Name = name;
            ScriptPath = scriptPath;
        }

        /// <summary>
        /// 节点类型
        /// </summary>
        public string Type;
        /// <summary>
        /// 节点名称
        /// </summary>
        public string Name;
        /// <summary>
        /// 节点脚本路径
        /// </summary>
        public string ScriptPath;
        /// <summary>
        /// 子节点
        /// </summary>
        public List<SceneNode> Children = new List<SceneNode>();

        public bool Equals(SceneNode other)
        {
            if (other == null)
            {
                return false;
            }

            if (other.Name != Name || other.Type != Type || other.ScriptPath != ScriptPath)
            {
                return false;
            }

            if (other.Children.Count != Children.Count)
            {
                return false;
            }

            for (var i = 0; i < Children.Count; i++)
            {
                if (!Children[i].Equals(other.Children[i]))
                {
                    return false;
                }
            }

            return true;
        }
    }

    /// <summary>
    /// 场景节点有变化时的回调事件
    /// </summary>
    public event Action<Node> SceneNodeChangeEvent;

    private SceneNode _sceneNode;
    private Node _targetNode;


    private double _checkTreeTimer = 0;

    /// <summary>
    /// 更新监听的节点
    /// </summary>
    public void ChangeCurrentNode(Node node)
    {
        //更新前检查旧的节点是否发生改变
        if (node != _targetNode && _targetNode != null)
        {
            try
            {
                var tempNode = ParseNodeTree(_targetNode);
                if (!_sceneNode.Equals(tempNode))
                {
                    OnSceneNodeChange(_targetNode);
                }
            }
            catch (Exception e)
            {
                //检查节点存在报错, 直接跳过该节点的检查
                GD.Print(e.Message);
            }
        }

        if (node != null)
        {
            _sceneNode = ParseNodeTree(node);
            _targetNode = node;
        }
        else
        {
            _targetNode = null;
        }
        _checkTreeTimer = 0;
    }

    public void Process(float delta)
    {
        if (_targetNode == null)
            return;
        
        _checkTreeTimer += delta;
        if (_checkTreeTimer >= 5) //5秒检查一次
        {
            try
            {
                var tempNode = ParseNodeTree(_targetNode);
                if (!_sceneNode.Equals(tempNode))
                {
                    OnSceneNodeChange(_targetNode);
                    _sceneNode = tempNode;
                }
            }
            catch (Exception e)
            {
                //检查节点存在报错, 直接跳过该节点的检查
                GD.Print(e.Message);
                _targetNode = null;
            }
            _checkTreeTimer = 0;
        }
    }
    
    
    private SceneNode ParseNodeTree(Node node)
    {
        var script = node.GetScript().As<CSharpScript>();
        string scriptPath;
        if (script == null)
        {
            scriptPath = null;
        }
        else
        {
            scriptPath = script.ResourcePath;
        }
        var uiNode = new SceneNode(node.GetType().FullName, node.Name, scriptPath);
        var count = node.GetChildCount();
        for (var i = 0; i < count; i++)
        {
            uiNode.Children.Add(ParseNodeTree(node.GetChild(i)));
        }

        return uiNode;
    }

    private void OnSceneNodeChange(Node node)
    {
        if (SceneNodeChangeEvent != null)
        {
            SceneNodeChangeEvent(node);
        }
    }
}
#endif