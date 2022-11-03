using Microsoft.Maui.Controls;
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

    private void Blackmarket_Disappearing(object sender, EventArgs e)
    {
        foreach (BlackmarketContentView contentView in Globals.blackmarketContents)
        {
            double percent = 0;
            switch (contentView.upgradeType)
            {
                case UpgradeType.damage:
                    for (int i = 0; i < contentView.amount; i++)
                        Globals.player.AddUpgrade(new Upgrade { type = UpgradeType.damage, rarity = 0 }); break;
                case UpgradeType.attackSpeed:
                    for (int i = 0; i < contentView.amount; i++)
                        Globals.player.AddUpgrade(new Upgrade { type = UpgradeType.attackSpeed, rarity = 0 }); break;
                case UpgradeType.critChance: break;
                case UpgradeType.critDamage: break;
                case UpgradeType.hp:
                    for (int i = 0; i < contentView.amount; i++)
                        Globals.player.AddUpgrade(new Upgrade { type = UpgradeType.hp, rarity = 1 }); break;
                case UpgradeType.pierce: break;
                case UpgradeType.bounce: break;
                case UpgradeType.cooldown:
                    for (int i = 0; i < contentView.amount; i++)
                        percent += contentView.value / 100.0;
                    Globals.player.cooldown -= percent; break;
                case UpgradeType.area:
                    for (int i = 0; i < contentView.amount; i++)
                        percent += contentView.value / 100.0;
                    Globals.player.area += percent; break;
                case UpgradeType.resurrect: break;
                case UpgradeType.dash:
                    for (int i = 0; i < contentView.amount; i++)
                        Globals.player.AddUpgrade(new Upgrade { type = UpgradeType.dash, rarity = 1 }); break;
                case UpgradeType.moveSpeed:
                    for (int i = 0; i < contentView.amount; i++)
                        percent += contentView.value / 100.0;
                    Globals.player.moveSpeed += percent; break;
                case UpgradeType.pickupRange:
                    for (int i = 0; i < contentView.amount; i++)
                        percent += contentView.value / 100.0;
                    Globals.player.pickupRange += percent; break;
            }
        }
    }
}