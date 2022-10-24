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
        MauiProgram.player.onGetUpgrade += OnPlayerGetUpgrade;
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

    //void UpdateRarityColor()
    //{
    //    switch (upgrade.rarity)
    //    {
    //        case 0:
    //            rarityTint.BackgroundColor = Colors.Transparent;
    //            break;
    //        case 1:
    //            rarityTint.BackgroundColor = Colors.Blue;
    //            break;
    //    }
    //}

    private void UpdateUpgrade(byte rarity, bool updateRarity = false)
    {
        if (updateRarity)
        {
            upgrade.rarity = rarity;
            slider.Minimum = upgrade.rarityMin;
            slider.Maximum = upgrade.rarityMax;
            slider.Value = upgrade.rarity;
            //UpdateRarityColor();
            ChangeImage();
        }
        
        currentStats.Text = GetStatsText("Curr ");

        MauiProgram.player.AddUpgrade(upgrade);
        upgradeStats.Text = GetStatsText("New ");
        MauiProgram.player.RemoveUpgrade(upgrade);

        dps.Text = "DpS when Taken: " + MauiProgram.player.GetDps(upgrade).ToString();
    }

    private string GetStatsText(string prefix)
    {
        switch (upgrade.type)
        {
            case UpgradeType.damage: return prefix + "Damage: " + Math.Round(MauiProgram.player.attackCalc, MauiProgram.roundingPrecision);
            case UpgradeType.attackSpeed: return prefix + "Attackspeed: " + Math.Round(MauiProgram.player.attackSpeedCalc, MauiProgram.roundingPrecision);
            case UpgradeType.critChance: return prefix + "Crit-Chance: " + Math.Round(MauiProgram.player.critChance * 100, MauiProgram.roundingPrecision) + "%";
            case UpgradeType.critDamage: return prefix + "Crit-Damage: " + Math.Round(MauiProgram.player.critDamage * 100, MauiProgram.roundingPrecision) + "%";
            case UpgradeType.hp: return prefix + "HP: " + MauiProgram.player.hp;
        }
        return null;
    }

    public void RecalculateRarity()
    {
        UpdateUpgrade(upgrade.rarity, true);
        //slider.Value = upgrade.rarityMax;
        //slider.Value = upgrade.rarityMin;
    }
    public void UpdateValues()
    {
        UpdateUpgrade(upgrade.rarity);
    }

    private void OnButtonClicked(object sender, EventArgs args)
    {
        MauiProgram.player.AddUpgrade(upgrade, true);
    }

    private void OnPlayerGetUpgrade(object sender, UpgradeEventArgs args)
    {
        UpdateUpgrade((byte)Math.Round(slider.Value));
    }
}