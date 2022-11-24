namespace BoO_UV;

public sealed partial class BlackmarketContentView : ContentView
{
    public int value
    {
        get { return _value; }
        set
        {
            _value = value;
            if (isPercent)
                ContentAmountLabel.Text = "+" + value.ToString() + "%";
            else
                ContentAmountLabel.Text = value.ToString();
        }
    }
    private int _value;
    public int amount
    {
        get { return _amount; }
        set
        {
            int add = value - _amount;
            if (_amount == maxAmount && add > 0) return;
            if (_amount == 0 && add < 0) return;

            _amount = value;
            if (add < 0)
                this.value -= amountAdd;
            else
                this.value += amountAdd;

            if (_amount == maxAmount)
                ContentAddDisableImage.IsVisible = true;
            else
                ContentAddDisableImage.IsVisible = false;
            if (_amount == 0)
                ContentRemoveDisableImage.IsVisible = true;
            else
                ContentRemoveDisableImage.IsVisible = false;
        }
    }
    private int _amount;
    public int maxAmount { get; set; }
    public int cost
    {
        get { return _cost; }
        set
        {
            _cost = value;
            /*ContentCostLabel.Text = value.ToString();*/
        }
    }
    private int _cost;
    public string typeText { get { return _typeText; } set { _typeText = value; ContentTypeLabel.Text = value; } }
    private string _typeText;
    public int amountAdd { get; set; }
    public bool isPercent
    {
        get { return _ispercent; }
        set
        {
            _ispercent = value;
            if (isPercent)
                ContentAmountLabel.Text = "+" + this.value.ToString() + "%";
            else
                ContentAmountLabel.Text = this.value.ToString();
        }
    }
    private bool _ispercent;
    public string description { get; set; }
    public GraphicsView graphicsView { get; set; }
    public UpgradeType upgradeType { get; set; }
    public BlackmarketContentView()
	{
		InitializeComponent();
        graphicsView = ContentGraphicsView;
        ContentAmountLabel.Text = value.ToString();
	}

    private void ContentRemoveButton_Clicked(object sender, EventArgs e)
	{
        amount--;
        Apply(true);
        SetVisualStateNormal(sender);
    }
    private void ContentAddButton_Clicked(object sender, EventArgs e)
    {
        amount++;
        Apply();
        SetVisualStateNormal(sender);
    }

    private void SetVisualStateNormal(object sender)
    {
        ImageButton button = (ImageButton)sender;
        Dispatcher.DispatchDelayed(TimeSpan.FromMilliseconds(25), () =>
        {
            VisualStateManager.GoToState(button, "Normal");
        });
    }

    private void Apply(bool remove = false)
    {
        double percent = 0;
        switch (upgradeType)
        {
            case UpgradeType.damage:
                if (remove)
                    Globals.player.RemoveUpgrade(new Upgrade { type = UpgradeType.damage, rarity = 0 });
                else
                    Globals.player.AddUpgrade(new Upgrade { type = UpgradeType.damage, rarity = 0 });
                break;
            case UpgradeType.attackSpeed:
                if (remove)
                    Globals.player.RemoveUpgrade(new Upgrade { type = UpgradeType.attackSpeed, rarity = 0 });
                else
                    Globals.player.AddUpgrade(new Upgrade { type = UpgradeType.attackSpeed, rarity = 0 });
                break;
            case UpgradeType.critChance: break;
            case UpgradeType.critDamage: break;
            case UpgradeType.hp:
                if (remove)
                    Globals.player.RemoveUpgrade(new Upgrade { type = UpgradeType.hp, rarity = 1 });
                else
                    Globals.player.AddUpgrade(new Upgrade { type = UpgradeType.hp, rarity = 1 });
                break;
            case UpgradeType.pierce: break;
            case UpgradeType.bounce: break;
            case UpgradeType.cooldown:
                percent += amountAdd / 100.0;
                if (remove)
                    Globals.player.cooldownBase += percent;
                else
                    Globals.player.cooldownBase -= percent;
                break;
            case UpgradeType.area:
                percent += amountAdd / 100.0;
                if (remove)
                    Globals.player.areaBase -= percent;
                else
                    Globals.player.areaBase += percent;
                break;
            case UpgradeType.resurrect: break;
            case UpgradeType.dash:
                if (remove)
                    Globals.player.RemoveUpgrade(new Upgrade { type = UpgradeType.dash, rarity = 1 });
                else
                    Globals.player.AddUpgrade(new Upgrade { type = UpgradeType.dash, rarity = 1 });
                break;
            case UpgradeType.moveSpeed:
                percent += amountAdd / 100.0;
                if (remove)
                    Globals.player.moveSpeedBase -= percent;
                else
                    Globals.player.moveSpeedBase += percent;
                break;
            case UpgradeType.pickupRange:
                percent += amountAdd / 100.0;
                if (remove)
                    Globals.player.pickupRangeBase -= percent;
                else
                    Globals.player.pickupRangeBase += percent;
                break;
        }
    }
}