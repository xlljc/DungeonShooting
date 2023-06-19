

using System;
using System.Collections.Generic;
using Godot;

public partial class ImageCanvas
{
    public class AreaPlaceholder
    {
        public AreaPlaceholder(int start, int end)
        {
            Start = start;
            End = end;
        }

        public int Start { get; }
        public int End { get; }

        public int Width => End - Start + 1;
    }
    
    /// <summary>
    /// 同一帧下将队列里的image绘制到指定画布下最大消耗时间, 如果绘制的时间超过了这个值, 则队列中后面的image将会放到下一帧绘制
    /// </summary>
    public static float MaxHandlerTime { get; set; } = 4f;
    
    /// <summary>
    /// 渲染窗口
    /// </summary>
    public static SubViewport RenderViewport { get; private set; }

    /// <summary>
    /// 渲染窗口大小
    /// </summary>
    public static Vector2I RenderViewportSize { get; private set; }
    
    //预渲染队列
    private static readonly Queue<ImageRenderData> _queueItems = new Queue<ImageRenderData>();
    //渲染中的队列
    private static readonly Queue<ImageRenderData> _drawingQueueItems = new Queue<ImageRenderData>();
    //负责渲染的Sprite回收堆栈
    private static readonly Stack<ImageRenderSprite> _renderSpriteStack = new Stack<ImageRenderSprite>();

    private static readonly List<AreaPlaceholder> _placeholders = new List<AreaPlaceholder>();
    private static ViewportTexture _viewportTexture;

    /// <summary>
    /// 初始化 viewport
    /// </summary>
    public static void Init(Node root)
    {
        RenderViewportSize = new Vector2I(1024, 185);
        RenderViewport = new SubViewport();
        RenderViewport.Name = "ImageCanvasViewport";
        RenderViewport.Size = RenderViewportSize;
        RenderViewport.RenderTargetUpdateMode = SubViewport.UpdateMode.Always;
        RenderViewport.TransparentBg = true;
        RenderViewport.CanvasItemDefaultTextureFilter = Viewport.DefaultCanvasItemTextureFilter.Nearest;
        var camera = new Camera2D();
        camera.Name = "ImageCanvasCamera";
        camera.AnchorMode = Camera2D.AnchorModeEnum.FixedTopLeft;
        RenderViewport.AddChild(camera);
        _viewportTexture = RenderViewport.GetTexture();

        root.AddChild(RenderViewport);
        RenderingServer.FramePostDraw += OnFramePostDraw;
    }

    private static AreaPlaceholder FindNotchPlaceholder(int width)
    {
        if (_placeholders.Count == 0)
        {
            var result = new AreaPlaceholder(0, width - 1);
            _placeholders.Add(result);
            return result;
        }

        for (var i = 0; i < _placeholders.Count; i++)
        {
            if (i == _placeholders.Count - 1) //最后一个
            {
                var item = _placeholders[i];
                var end = item.End + width;
                if (end < RenderViewportSize.X)
                {
                    var result = new AreaPlaceholder(item.End + 1, end);
                    _placeholders.Add(result);
                    return result;
                }
            }
            else if (i == 0) //第一个
            {
                var item = _placeholders[i];
                var end = width - 1;
                if (end < item.Start)
                {
                    var result = new AreaPlaceholder(0, end);
                    _placeholders.Insert(0, result);
                    return result;
                }
            }
            else //中间
            {
                var prev = _placeholders[i - 1];
                var next = _placeholders[i];
                var end = prev.End + width;
                if (end < next.Start + 1)
                {
                    var result = new AreaPlaceholder(prev.End + 1, end);
                    _placeholders.Insert(i, result);
                    return result;
                }
            }
        }

        return null;
    }

    private static void RemovePlaceholder(AreaPlaceholder placeholder)
    {
        if (!_placeholders.Remove(placeholder))
        {
            GD.PrintErr("移除 AreaPlaceholder 失败!");
        }
    }
    
    private static ImageRenderSprite GetRenderSprite(Vector2 position)
    {
        ImageRenderSprite renderSprite;
        if (_renderSpriteStack.Count > 0)
        {
            renderSprite = _renderSpriteStack.Pop();
        }
        else
        {
            renderSprite = new ImageRenderSprite();
        }

        renderSprite.Sprite.Position = position;
        RenderViewport.AddChild(renderSprite.Sprite);
        return renderSprite;
    }

    private static void ReclaimRenderSprite(ImageRenderSprite renderSprite)
    {
        RenderViewport.RemoveChild(renderSprite.Sprite);
        _renderSpriteStack.Push(renderSprite);
    }

    private static void OnFramePostDraw()
    {
        //最大处理时间, 如果超过了, 那就下一帧再画其它的吧
        float step1Time;
        float step2Time;
        if (_drawingQueueItems.Count == 0)
        {
            step1Time = 0;
            step2Time = MaxHandlerTime;
        }
        else if (_queueItems.Count == 0)
        {
            step1Time = MaxHandlerTime;
            step2Time = 0;
        }
        else
        {
            step1Time = step2Time = MaxHandlerTime / 2;
        }

        //上一帧绘制的image
        if (_drawingQueueItems.Count > 0)
        {
            var redrawCanvas = new HashSet<ImageCanvas>();
            List<ImageRenderData> callDrawingCompleteList = null;
            using (var image = _viewportTexture.GetImage())
            {
                var startTime = DateTime.Now;
                //File.WriteAllBytes("d:/image.png", image.SavePngToBuffer());
                //绘制完成需要调用回调的列表
                var index = 0;
                do
                {
                    var item = _drawingQueueItems.Dequeue();
                    if (!item.ImageCanvas.IsDestroyed)
                    {
                        redrawCanvas.Add(item.ImageCanvas);
                        //处理绘图
                        HandleDrawing(index, image, item);
                        index++;
                        if (item.OnDrawingComplete != null)
                        {
                            if (callDrawingCompleteList == null)
                            {
                                callDrawingCompleteList = new List<ImageRenderData>();
                            }
                            callDrawingCompleteList.Add(item);
                        }
                    }

                    //移除站位符
                    if (item.AreaPlaceholder != null)
                    {
                        RemovePlaceholder(item.AreaPlaceholder);
                        item.AreaPlaceholder = null;
                    }

                    //回收 RenderSprite
                    if (item.RenderSprite != null)
                    {
                        ReclaimRenderSprite(item.RenderSprite);
                        item.RenderSprite = null;
                    }
                } while (_drawingQueueItems.Count > 0 && (DateTime.Now - startTime).TotalMilliseconds < step1Time);

                GD.Print($"当前帧绘制完成数量: {index}, 绘制队列数量: {_drawingQueueItems.Count}, 用时: {(DateTime.Now - startTime).TotalMilliseconds}毫秒");
                
            }

            //重绘画布
            foreach (var drawCanvas in redrawCanvas)
            {
                drawCanvas.Redraw();
            }
            //调用完成回调
            if (callDrawingCompleteList != null)
            {
                foreach (var imageRenderData in callDrawingCompleteList)
                {
                    try
                    {
                        imageRenderData.OnDrawingComplete();
                    }
                    catch (Exception e)
                    {
                        GD.PrintErr("在ImageCanvas中调用回调OnDrawingComplete()发生异常: " + e);
                    }
                }
            }
        }

        //处理下一批image
        if (_queueItems.Count > 0)
        {
            var startTime = DateTime.Now;
            List<ImageRenderData> retryList = null;
            var index = 0;
            //执行绘制操作
            do
            {
                var item = _queueItems.Dequeue();
                if (!item.ImageCanvas.IsDestroyed)
                {
                    //排队绘制
                    if (HandleEnqueueDrawing(index, item))
                    {
                        index++;
                    }
                    else //添加失败
                    {
                        if (retryList == null)
                        {
                            retryList = new List<ImageRenderData>();
                        }
                        retryList.Add(item);
                    }
                }

            } while (_queueItems.Count > 0 && (DateTime.Now - startTime).TotalMilliseconds < step2Time);

            if (retryList != null)
            {
                foreach (var renderData in retryList)
                {
                    _queueItems.Enqueue(renderData);
                }
            }
            
            GD.Print($"当前帧进入绘制绘队列数量: {index}, 待绘制队列数量: {_queueItems.Count}, 绘制队列数量: {_drawingQueueItems.Count}, 用时: {(DateTime.Now - startTime).TotalMilliseconds}毫秒");
        }
    }

    private static void HandleDrawing(int index, Image image, ImageRenderData item)
    {
        //截取Viewport像素点
        item.ImageCanvas._canvas.BlendRect(image,
            new Rect2I(item.AreaPlaceholder.Start, 0,item.RenderWidth, item.RenderHeight),
            new Vector2I(item.X - item.RenderOffsetX, item.Y - item.RenderOffsetY)
        );

        item.SrcImage.Dispose();
        item.SrcImage = null;
    }
    
    //处理排队绘制
    private static bool HandleEnqueueDrawing(int index, ImageRenderData item)
    {
        var placeholder = FindNotchPlaceholder(item.RenderWidth);
        if (placeholder == null) //没有找到合适的位置
        {
            return false;
        }

        item.AreaPlaceholder = placeholder;

        item.RenderX = placeholder.Start + item.RenderOffsetX;
        item.RenderY = item.RenderOffsetY;
        var renderSprite = GetRenderSprite(new Vector2(item.RenderX, item.RenderY));
        item.RenderSprite = renderSprite;
        if (item.RotationGreaterThanPi) //角度大于180度
        {
            item.SrcImage.FlipX();
            if (!item.FlipY)
            {
                item.SrcImage.FlipY();
            }
        }
        else
        {
            if (item.FlipY)
            {
                item.SrcImage.FlipY();
            }
        }
        renderSprite.Sprite.Offset = new Vector2(-item.CenterX, -item.CenterY);
        
        //设置旋转
        renderSprite.Sprite.Rotation = item.Rotation;

        renderSprite.SetImage(item.SrcImage);
        _drawingQueueItems.Enqueue(item);
        return true;
    }
}