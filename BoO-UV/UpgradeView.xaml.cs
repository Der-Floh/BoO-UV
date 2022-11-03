namespace BoO_UV;

public partial class UpgradeView : ContentView
{
    public Upgrade upgrade
    {
        get { return _upgrade; }
        set
        {
            _upgrade = value;
            UpdateUpgrade(_upgrade.rarity, true);
            typeImg.Source = upgrade.image;
        }
    }
    private Upgrade _upgrade;
    public UpgradeView(Upgrade upgrade)
    {
        InitializeComponent();
        Globals.player.onGetUpgrade += OnPlayerGetUpgrade;
        this.upgrade = upgrade;
    }

    private void OnSliderValueChanged(object sender, ValueChangedEventArgs args)
    {
        byte oldValue = (byte)Math.Round(args.OldValue);
        byte newValue = (byte)Math.Round(args.NewValue);

        if (newValue != oldValue)
        {
            UpdateUpgrade(newValue, true);
        }
    }

    private void OnSliderDragCompleted(object sender, EventArgs args)
    {
        slider.Value = Math.Round(slider.Value);
    }

    void ChangeImage()
    {
        holderImg.Source = upgrade.holderImage;
        rarityImg.Source = upgrade.rarityImage;
    }

    private void UpdateUpgrade(byte rarity, bool updateRarity = false)
    {
        if (updateRarity)
        {
            upgrade.rarity = rarity;
            slider.Minimum = upgrade.rarityMin;
            slider.Maximum = upgrade.rarityMax;
            slider.Value = upgrade.rarity;
            ChangeImage();
        }
        
        currentStats.Text = GetStatsText(Globals.currText);

        Globals.player.AddUpgrade(upgrade);
        upgradeStats.Text = GetStatsText(Globals.newText);
        Globals.player.RemoveUpgrade(upgrade);

        dps.Text = Globals.dpsText + Globals.player.GetDps(upgrade).ToString();
    }

    private string GetStatsText(string prefix)
    {
        switch (upgrade.type)
        {
            case UpgradeType.damage: return prefix + upgrade.name + ": " + Math.Round(Globals.player.attackCalc, Globals.roundingPrecision);
            case UpgradeType.attackSpeed: return prefix + upgrade.name + ": " + Math.Round(Globals.player.attackSpeedCalc, Globals.roundingPrecision);
            case UpgradeType.critChance: return prefix + upgrade.name + ": " + Math.Round(Globals.player.critChance * 100, Globals.roundingPrecision) + "%";
            case UpgradeType.critDamage: return prefix + upgrade.name + ": " + Math.Round(Globals.player.critDamage * 100, Globals.roundingPrecision) + "%";
            case UpgradeType.hp: return prefix + upgrade.name + ": " + Globals.player.hp;
            case UpgradeType.pierce: return prefix + upgrade.name + ": " + Globals.player.pierce;
            case UpgradeType.bounce: return prefix + upgrade.name + ": " + Globals.player.bounce;
            case UpgradeType.cooldown: return prefix + upgrade.name + ": " + Math.Round(Globals.player.cooldown * 100, Globals.roundingPrecision) + "%";
            case UpgradeType.area: return prefix + upgrade.name + ": " + Math.Round(Globals.player.areaCalc, Globals.roundingPrecision);
            case UpgradeType.resurrect: return prefix + upgrade.name + ": " + Globals.player.resurrect;
            case UpgradeType.moveSpeed: return prefix + upgrade.name + ": " + Globals.player.moveSpeed;
            case UpgradeType.pickupRange: return prefix + upgrade.name + ": " + Globals.player.pickupRange;
        }
        return null;
    }

    public void RecalculateRarity()
    {
        UpdateUpgrade(upgrade.rarity, true);
    }
    public void UpdateValues()
    {
        UpdateUpgrade(upgrade.rarity);
    }

    private void OnButtonClicked(object sender, EventArgs args)
    {
        Globals.player.AddUpgrade(upgrade, true);
        Button button = (Button)sender;
        Dispatcher.DispatchDelayed(TimeSpan.FromMilliseconds(50), () =>
        {
            VisualStateManager.GoToState(button, "Normal");
        });
    }

    private void OnPlayerGetUpgrade(object sender, UpgradeEventArgs args)
    {
        UpdateUpgrade((byte)Math.Round(slider.Value));
    }
}