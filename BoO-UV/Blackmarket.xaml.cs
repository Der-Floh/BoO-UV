using Microsoft.Maui.Controls.Shapes;

namespace BoO_UV;

public partial class Blackmarket : ContentPage
{
	public Blackmarket()
	{
		InitializeComponent();
        Disappearing += Blackmarket_Disappearing;
        MarketContentFrame.StrokeShape = new RoundRectangle
        {
            CornerRadius = new CornerRadius(10, 10, 10, 10)
        };
    }

    private void Blackmarket_Disappearing(object sender, EventArgs e)
    {
        
    }
}