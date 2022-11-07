using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Shapes;
using System.Reflection;

namespace BoO_UV;

public partial class Blackmarket : ContentPage
{
	public Blackmarket()
	{
		InitializeComponent();
        Appearing += Blackmarket_Appearing;
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

        /*
        var assembly = Assembly.GetExecutingAssembly();
        Stream stream = assembly.GetManifestResourceStream("BoO_UV.Resources.Images.blackmarket.blackmarket_roger_dialogue.svg");

        RogerDialogueImage.Source = ImageSource.FromStream(() => stream);
        */
    }

    private void GraphicsView_StartHoverInteraction(object sender, TouchEventArgs e)
    {
        GraphicsView graphicsView = (GraphicsView)sender;
        BlackmarketContentView contentView = (BlackmarketContentView)graphicsView.Parent.Parent;
        RogerDialogueLabel.Text = contentView.description;
    }

    private void Blackmarket_Appearing(object sender, EventArgs e)
    {
        foreach (BlackmarketContentView contentView in Globals.blackmarketContents)
        {
            if (contentView.amount == 0)
                continue;
            double percent = 0;
            switch (contentView.upgradeType)
            {
                case UpgradeType.damage:
                    Globals.player.RemoveUpgrade(new Upgrade { type = UpgradeType.damage, rarity = 0 }); break;
                case UpgradeType.attackSpeed:
                    Globals.player.RemoveUpgrade(new Upgrade { type = UpgradeType.attackSpeed, rarity = 0 }); break;
                case UpgradeType.critChance: break;
                case UpgradeType.critDamage: break;
                case UpgradeType.hp:
                    Globals.player.RemoveUpgrade(new Upgrade { type = UpgradeType.hp, rarity = 1 }); break;
                case UpgradeType.pierce: break;
                case UpgradeType.bounce: break;
                case UpgradeType.cooldown:
                    percent += contentView.value / 100.0;
                    Globals.player.cooldown += percent; break;
                case UpgradeType.area:
                    percent += contentView.value / 100.0;
                    Globals.player.area -= percent; break;
                case UpgradeType.resurrect: break;
                case UpgradeType.dash:
                    Globals.player.RemoveUpgrade(new Upgrade { type = UpgradeType.dash, rarity = 1 }); break;
                case UpgradeType.moveSpeed:
                    percent += contentView.value / 100.0;
                    Globals.player.moveSpeed -= percent; break;
                case UpgradeType.pickupRange:
                    percent += contentView.value / 100.0;
                    Globals.player.pickupRange -= percent; break;
            }
        }
    }

    private void Blackmarket_Disappearing(object sender, EventArgs e)
    {
        foreach (BlackmarketContentView contentView in Globals.blackmarketContents)
        {
            if (contentView.amount == 0)
                continue;
            double percent = 0;
            switch (contentView.upgradeType)
            {
                case UpgradeType.damage:
                        Globals.player.AddUpgrade(new Upgrade { type = UpgradeType.damage, rarity = 0 }); break;
                case UpgradeType.attackSpeed:
                        Globals.player.AddUpgrade(new Upgrade { type = UpgradeType.attackSpeed, rarity = 0 }); break;
                case UpgradeType.critChance: break;
                case UpgradeType.critDamage: break;
                case UpgradeType.hp:
                        Globals.player.AddUpgrade(new Upgrade { type = UpgradeType.hp, rarity = 1 }); break;
                case UpgradeType.pierce: break;
                case UpgradeType.bounce: break;
                case UpgradeType.cooldown:
                        percent += contentView.value / 100.0;
                    Globals.player.cooldown -= percent; break;
                case UpgradeType.area:
                        percent += contentView.value / 100.0;
                    Globals.player.area += percent; break;
                case UpgradeType.resurrect: break;
                case UpgradeType.dash:
                        Globals.player.AddUpgrade(new Upgrade { type = UpgradeType.dash, rarity = 1 }); break;
                case UpgradeType.moveSpeed:
                        percent += contentView.value / 100.0;
                    Globals.player.moveSpeed += percent; break;
                case UpgradeType.pickupRange:
                        percent += contentView.value / 100.0;
                    Globals.player.pickupRange += percent; break;
            }
        }
    }
}