namespace BoO_UV;

public static class MauiProgram
{
	public static List<Object> objectList { get; set; } = new List<Object>();
	public static int roundingPrecision { get; set; } = 2;
	public static Player player = new Player();

    public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

		return builder.Build();
	}
}
