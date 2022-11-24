//using SkiaSharp.Views.Maui.Controls.Hosting;
//using Toolkit;

namespace BoO_UV;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()//.UseSkiaSharp()
            .ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

        //SVGImage.GloabalDefaultAssemblyName = "BoO-UV";

        return builder.Build();
	}
}