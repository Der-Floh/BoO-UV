using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Shapes;
using System.Reflection;

namespace BoO_UV;

public sealed partial class Blackmarket : ContentPage
{
	public Blackmarket()
	{
		InitializeComponent();
        MarketContentFrame.StrokeShape = new RoundRectangle
        {
            CornerRadius = new CornerRadius(10, 10, 10, 10)
        };

        int i = 0;
        foreach (BlackmarketContentView contentView in Globals.blackmarketContents)
        {
            contentView.graphicsView.StartHoverInteraction += GraphicsView_StartHoverInteraction;
            MarketContentUpgradesGrid.Add(contentView, 0, i);
            i++;
        }
    }

    private void GraphicsView_StartHoverInteraction(object sender, TouchEventArgs e)
    {
        GraphicsView graphicsView = (GraphicsView)sender;
        BlackmarketContentView contentView = (BlackmarketContentView)graphicsView.Parent.Parent;
        RogerDialogueLabel.Text = contentView.description;
    }
}