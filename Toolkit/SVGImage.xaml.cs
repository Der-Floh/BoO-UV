using SkiaSharp.Views.Maui;
using SkiaSharp;
using System.Reflection;
using Microsoft.Maui.Graphics;
using System.IO;
using SkiaSharp.Extended.Svg;
using SKSvg = SkiaSharp.Extended.Svg.SKSvg;

namespace Toolkit;

public static class AssemblyHelper
{
    private static Dictionary<string, Assembly> cached = new Dictionary<string, Assembly>();
    private static Dictionary<string, string[]> assemblyResources = new Dictionary<string, string[]>();

    public static bool AddAssembly(in string assemblyName)
    {
        try
        {
            var currentAssembly = Assembly.Load(assemblyName);
            cached.Add(assemblyName, currentAssembly);
            assemblyResources.Add(assemblyName, currentAssembly.GetManifestResourceNames());
            return true;
        }
        catch
        {
            return false;
        }
    }

    public static bool ExistsFileInCachesAssemblys(in string fileName)
    {
        foreach (var item in cached)
        {
            if (ExistsFile(fileName, item.Key))
            {
                return true;
            }
        }

        return false;
    }

    public static bool GetFileFromCachesAssemblys(in string fileName, out Stream fileStream)
    {
        foreach (var item in cached)
        {
            if (GetFile(fileName, item.Key, out fileStream))
            {
                return true;
            }
        }

        fileStream = null;
        return false;
    }

    public static bool GetFile(in string fileName, in string assemblyName, out Stream stream)
    {
        if (assemblyResources.ContainsKey(assemblyName))
        {
            string path = fileName.Replace('\\', '.');

            for (int i = 0; i < assemblyResources[assemblyName].Length; i++)
            {
                if (assemblyResources[assemblyName][i].Contains(path))
                {
                    stream = cached[assemblyName].GetManifestResourceStream(assemblyResources[assemblyName][i]);
                    return true;
                }
            }
        }

        stream = null;
        return false;
    }

    public static bool ExistsFile(in string fileName, in string assemblyName)
    {
        if (assemblyResources.ContainsKey(assemblyName))
        {
            string path = fileName.Replace('\\', '.');

            for (int i = 0; i < assemblyResources[assemblyName].Length; i++)
            {
                if (assemblyResources[assemblyName][i].Contains(path))
                {
                    return true;
                }
            }
        }

        return false;
    }
}

public partial class SVGImage : ContentView
{

    private enum ImageMode
    {
        NON,
        PNG,
        SVG
    }

    public static string _gloabalDefaultAssemblyName = typeof(SVGImage).Namespace;
    public static string GloabalDefaultAssemblyName
    {
        get => _gloabalDefaultAssemblyName;
        set
        {
            _gloabalDefaultAssemblyName = value;
            AssemblyHelper.AddAssembly(value);
        }
    }

    public static readonly BindableProperty AssemblyNameProperty = BindableProperty.Create(nameof(AssemblyName), typeof(string), typeof(SVGImage), _gloabalDefaultAssemblyName);

    public string AssemblyName
    {
        get => (string)GetValue(AssemblyNameProperty);
        set => SetValue(AssemblyNameProperty, value);
    }

    public static readonly BindableProperty SourceProperty = BindableProperty.Create(nameof(Source), typeof(string), typeof(SVGImage), default(string));

    public string Source
    {
        get => (string)GetValue(SourceProperty);
        set => SetValue(SourceProperty, value);
    }

    public static readonly BindableProperty TintColorProperty = BindableProperty.Create(nameof(TintColor), typeof(Color), typeof(SVGImage), null);

    public Color TintColor
    {
        get => (Color)GetValue(TintColorProperty);
        set => SetValue(TintColorProperty, value);
    }

    public static readonly BindableProperty HorizontalImageProperty = BindableProperty.Create(nameof(HorizontalImage), typeof(LayoutAlignment), typeof(SVGImage), LayoutAlignment.Center);

    public LayoutAlignment HorizontalImage
    {
        get => (LayoutAlignment)GetValue(HorizontalImageProperty);
        set => SetValue(HorizontalImageProperty, value);
    }

    public static readonly BindableProperty VerticalImageProperty = BindableProperty.Create(nameof(VerticalImage), typeof(LayoutAlignment), typeof(SVGImage), LayoutAlignment.Center);

    public LayoutAlignment VerticalImage
    {
        get => (LayoutAlignment)GetValue(VerticalImageProperty);
        set => SetValue(VerticalImageProperty, value);
    }

    public SVGImage()
    {
        InitializeComponent();

        SetValue(AssemblyNameProperty, _gloabalDefaultAssemblyName);
        PropertyChanged += SVGImage_PropertyChanged;
        SizeChanged += SVGImage_SizeChanged;
        skCanvas.PaintSurface += SkCanvas_PaintSurface;
    }

    private void SVGImage_SizeChanged(object sender, EventArgs e)
    {
        skCanvas.InvalidateSurface();
    }

    private void SVGImage_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(Source))
            resourceNeedUpdate = true;

        if (e.PropertyName == nameof(AssemblyName))
        {
            AssemblyHelper.AddAssembly(AssemblyName);
        }
        skCanvas.InvalidateSurface();
    }

    private void SkCanvas_PaintSurface(object sender, SKPaintSurfaceEventArgs e)
    {
        if (Source == null)
            return;

        ImageMode mode = GetImageMode();
        switch (mode)
        {
            case ImageMode.PNG:
                DrawPNG(e);
                break;

            case ImageMode.SVG:
                DrawSVG(e);
                break;
        }
    }

    private ImageMode GetImageMode()
    {
        if (string.IsNullOrEmpty(Source))
            return ImageMode.NON;

        string name = Path.GetFileName(Source).ToLower();
        if (name.Contains(".png"))
            return ImageMode.PNG;

        return ImageMode.SVG;
    }

    private SKBitmap lastBitmap = null;
    private bool resourceNeedUpdate = false;
    private void LoadResource()
    {
        if (!resourceNeedUpdate)
            return;

        ImageMode mode = GetImageMode();
        if (mode == ImageMode.PNG)
        {
            if (AssemblyHelper.ExistsFileInCachesAssemblys(Source))
                if (AssemblyHelper.GetFileFromCachesAssemblys(Source, out Stream stream))
                {
                    if (lastBitmap != null)
                        lastBitmap.Dispose();

                    lastBitmap = SKBitmap.Decode(stream);
                    stream.Close();
                }

        }
        else if (mode == ImageMode.SVG)
        {
            if (AssemblyHelper.ExistsFileInCachesAssemblys(Source))
                if (AssemblyHelper.GetFileFromCachesAssemblys(Source, out Stream stream))
                {
                    if (lastBitmap == null)
                        lastSVG = new SKSvg();
                    else
                        lastSVG.Picture.Dispose();

                    lastSVG.Load(stream);
                    stream.Close();
                }
        }

        resourceNeedUpdate = false;

    }
    private void DrawPNG(SKPaintSurfaceEventArgs e)
    {
        try
        {
            var surface = e.Surface;

            var canvas = surface.Canvas;

            canvas.Clear();

            if (string.IsNullOrEmpty(Source) || string.IsNullOrEmpty(AssemblyName))
                return;

            if (AssemblyHelper.ExistsFileInCachesAssemblys(Source))
            {
                // resourceNeedUpdate = true;
                LoadResource();

                var skImageInfo = e.Info;
                canvas.Translate(skImageInfo.Width / 2f, skImageInfo.Height / 2f);

                //asd
                var MidY = (float)lastBitmap.Height / 2;
                var MidX = (float)lastBitmap.Width / 2;
                float xRatio = skImageInfo.Width / (float)lastBitmap.Width;
                float yRatio = skImageInfo.Height / (float)lastBitmap.Height;

                float ratio = Math.Min(xRatio, yRatio);

                float svgHeight = skImageInfo.Height / ratio;
                float svgHeighthalf = (svgHeight / 2);

                float svgWidth = skImageInfo.Width / ratio;
                float svgWidthhalf = (svgWidth / 2);

                float yPos = -MidY;
                float xPos = -MidX;

                float scaleX = ratio;
                float scaleY = ratio;

                switch (VerticalImage)
                {
                    case LayoutAlignment.Start:
                        yPos = -svgHeighthalf;
                        break;

                    case LayoutAlignment.End:
                        yPos = svgHeighthalf - lastBitmap.Height;
                        break;
                    case LayoutAlignment.Fill:
                        scaleY = yRatio;
                        break;
                }

                switch (HorizontalImage)
                {
                    case LayoutAlignment.Start:
                        xPos = -svgWidthhalf;
                        break;

                    case LayoutAlignment.End:
                        xPos = svgWidthhalf - lastBitmap.Width;
                        break;
                    case LayoutAlignment.Fill:
                        scaleX = xRatio;
                        break;
                }

                canvas.Scale(scaleX, scaleY);

                canvas.Translate(xPos, yPos); //Center

                var paint = new SKPaint();
                paint.IsAntialias = true;
                if (TintColor != null)
                {
                    paint.ColorFilter = SKColorFilter.CreateBlendMode(
                        TintColor.ToSKColor(),       // the color, also `(SKColor)0xFFFF0000` is valid
                        SKBlendMode.SrcATop); // use the source color
                    canvas.DrawBitmap(lastBitmap, SKPoint.Empty, paint);
                }
                else
                    canvas.DrawBitmap(lastBitmap, SKPoint.Empty, paint);

                surface.Dispose();
                canvas.Dispose();
                System.GC.Collect();

            }
        }
        catch (Exception exc)
        {
            Console.WriteLine("OnPaintSurface Exception: " + exc);
        }
    }

    private SKSvg lastSVG = null;
    private void DrawSVG(SKPaintSurfaceEventArgs e)
    {
        try
        {
            var surface = e.Surface;
            var canvas = surface.Canvas;

            canvas.Clear();


            if (string.IsNullOrEmpty(Source) || string.IsNullOrEmpty(AssemblyName))
                return;

            if (AssemblyHelper.ExistsFileInCachesAssemblys(Source))
            {
                LoadResource();

                var skImageInfo = e.Info;
                canvas.Translate(skImageInfo.Width / 2f, skImageInfo.Height / 2f);

                var skRect = lastSVG.ViewBox;
                var MidY = lastSVG.CanvasSize.Height / 2;
                var MidX = lastSVG.CanvasSize.Width / 2;
                float xRatio = skImageInfo.Width / lastSVG.CanvasSize.Width;
                float yRatio = skImageInfo.Height / lastSVG.CanvasSize.Height;

                float ratio = Math.Min(xRatio, yRatio);

                float svgHeight = skImageInfo.Height / ratio;
                float svgHeighthalf = (svgHeight / 2);

                float svgWidth = skImageInfo.Width / ratio;
                float svgWidthhalf = (svgWidth / 2);

                float yPos = -MidY;
                float xPos = -MidX;

                float scaleX = ratio;
                float scaleY = ratio;

                switch (VerticalImage)
                {
                    case LayoutAlignment.Start:
                        yPos = -svgHeighthalf;
                        break;

                    case LayoutAlignment.End:
                        yPos = svgHeighthalf - lastSVG.CanvasSize.Height;
                        break;
                    case LayoutAlignment.Fill:
                        scaleY = yRatio;
                        break;
                }

                switch (HorizontalImage)
                {
                    case LayoutAlignment.Start:
                        xPos = -svgWidthhalf;
                        break;

                    case LayoutAlignment.End:
                        xPos = svgWidthhalf - lastSVG.CanvasSize.Width;
                        break;
                    case LayoutAlignment.Fill:
                        scaleX = xRatio;
                        break;
                }

                canvas.Scale(scaleX, scaleY);

                canvas.Translate(xPos, yPos); //Center

                var paint = new SKPaint();
                paint.IsAntialias = true;
                if (TintColor != null)
                {
                    paint.ColorFilter = SKColorFilter.CreateBlendMode(
                        TintColor.ToSKColor(),       // the color, also `(SKColor)0xFFFF0000` is valid
                        SKBlendMode.SrcATop); // use the source color
                    canvas.DrawPicture(lastSVG.Picture, paint);
                }
                else
                    canvas.DrawPicture(lastSVG.Picture, paint);

                surface.Dispose();
                canvas.Dispose();
                System.GC.Collect();
            }
        }
        catch (Exception exc)
        {
            Console.WriteLine("OnPaintSurface Exception: " + exc);
        }
    }
}