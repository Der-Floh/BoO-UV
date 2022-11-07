using SkiaSharp.Views.Maui.Controls.Hosting;
using System.Reflection;
using Toolkit;

namespace BoO_UV;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>().UseSkiaSharp()
            .ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

		InitToolkit();

        return builder.Build();
	}

    static void InitToolkit()
    {
        SVGImage.GloabalDefaultAssemblyName = "BoO-UV"; // not BoO_UV
    }
}
