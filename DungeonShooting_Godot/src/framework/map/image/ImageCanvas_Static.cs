

using System;
using System.Collections.Generic;
using Godot;

public partial class ImageCanvas
{
    
    /// <summary>
    /// 同一帧下将队列里的image绘制到指定画布下最大消耗时间, 如果绘制的时间超过了这个值, 则队列中后面的image将会放到下一帧绘制
    /// </summary>
    public static float MaxHandlerTime { get; set; } = 4f;
    
    /// <summary>
    /// 渲染窗口
    /// </summary>
    public static SubViewport RenderViewport { get; private set; }
    
    /// <summary>
    /// 渲染偏移位置
    /// </summary>
    public static Vector2 RenderOffset { get; set; }

    //预渲染队列
    private static readonly Queue<ImageRenderData> _enqueueItems = new Queue<ImageRenderData>();
    //渲染中的队列
    private static readonly Queue<ImageRenderData> _drawingEnqueueItems = new Queue<ImageRenderData>();

    private static readonly Stack<ImageRenderSprite> _renderSpriteStack = new Stack<ImageRenderSprite>();

    public static void Init(SubViewport renderViewport, Vector2 renderOffset)
    {
        RenderViewport = renderViewport;
        RenderOffset = renderOffset;
        RenderingServer.FramePostDraw += OnFramePostDraw;
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
        //上一帧绘制的image
        if (_drawingEnqueueItems.Count > 0)
        {
            var redrawCanvas = new HashSet<ImageCanvas>();
            var viewportTexture = RenderViewport.GetTexture();
            using (var image = viewportTexture.GetImage())
            {
                var num = 0;
                do
                {
                    var item = _drawingEnqueueItems.Dequeue();
                    if (!item.ImageCanvas.IsDestroyed)
                    {
                        redrawCanvas.Add(item.ImageCanvas);
                        //截取Viewport像素点
                        item.ImageCanvas._canvas.BlendRect(image,
                            new Rect2I(0, 0, item.RenderWidth, item.RenderHeight),
                            new Vector2I(item.X - item.RenderOffsetX, item.Y - item.RenderOffsetY)
                        );

                        item.SrcImage.Dispose();
                        item.SrcImage = null;
                        num++;
                    }

                    //回收 RenderSprite
                    if (item.RenderSprite != null)
                    {
                        ReclaimRenderSprite(item.RenderSprite);
                    }
                } while (_drawingEnqueueItems.Count > 0);

                GD.Print($"当前帧绘制完成数量: {num}, 绘制队列数量: {_drawingEnqueueItems.Count}");
            }

            //重绘画布
            foreach (var drawCanvas in redrawCanvas)
            {
                drawCanvas.Redraw();
            }
        }

        //处理下一批image
        if (_enqueueItems.Count > 0)
        {
            var startTime = DateTime.Now;

            var num = 0;
            //执行绘制操作
            //绘制的总时间不能超过MaxHandlerTime, 如果超过了, 那就下一帧再画其它的吧
            do
            {
                var item = _enqueueItems.Dequeue();
                if (!item.ImageCanvas.IsDestroyed)
                {

                    var cosAngle = Mathf.Cos(item.Rotation);
                    var sinAngle = Mathf.Sin(item.Rotation);
                    if (cosAngle == 0)
                    {
                        cosAngle = 1e-6f;
                    }

                    if (sinAngle == 0)
                    {
                        sinAngle = 1e-6f;
                    }

                    var width = item.SrcImage.GetWidth();
                    var height = item.SrcImage.GetHeight();
                    //旋转后的图片宽高
                    item.RenderWidth = Mathf.CeilToInt(width * Mathf.Abs(cosAngle) + height * sinAngle) + 2;
                    item.RenderHeight = Mathf.CeilToInt(width * sinAngle + height * Mathf.Abs(cosAngle)) + 2;

                    item.RenderOffsetX =
                        (int)((item.CenterX / sinAngle +
                                        (0.5f * item.RenderWidth * sinAngle - 0.5f * item.RenderHeight * cosAngle +
                                         0.5f * height) /
                                        cosAngle -
                                        (-0.5f * item.RenderWidth * cosAngle - 0.5f * item.RenderHeight * sinAngle +
                                         0.5f * width) /
                                        sinAngle - item.CenterY / cosAngle) /
                        (cosAngle / sinAngle + sinAngle / cosAngle)) + 1;


                    item.RenderOffsetY =
                        (int)((item.CenterX / cosAngle -
                                  (-0.5f * item.RenderWidth * cosAngle - 0.5f * item.RenderHeight * sinAngle + 0.5f * width) /
                                  cosAngle -
                                  (0.5f * item.RenderWidth * sinAngle - 0.5f * item.RenderHeight * cosAngle + 0.5f * height) /
                                  sinAngle + item.CenterY / sinAngle) /
                              (sinAngle / cosAngle + cosAngle / sinAngle)) + 1;

                    var renderSprite = GetRenderSprite(new Vector2(0 + item.RenderOffsetX, 0 + item.RenderOffsetY));
                    item.RenderSprite = renderSprite;
                    //设置绘制信息
                    renderSprite.Sprite.Offset = new Vector2(item.CenterX, item.CenterY);
                    renderSprite.Sprite.Rotation = item.Rotation;

                    renderSprite.SetImage(item.SrcImage);
                    _drawingEnqueueItems.Enqueue(item);
                    num++;
                }

            } while (_enqueueItems.Count > 0 && (DateTime.Now - startTime).TotalMilliseconds < MaxHandlerTime);

            GD.Print($"当前帧进入绘制绘队列数量: {num}, 待绘制队列数量: {_enqueueItems.Count}, 绘制队列数量: {_drawingEnqueueItems.Count}");
        }
    }
}