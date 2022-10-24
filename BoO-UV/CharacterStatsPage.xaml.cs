namespace BoO_UV;

public partial class CharacterStatsPage : ContentPage
{
    public CharacterStatsPage()
    {
        InitializeComponent();
        Appearing += CharacterStatsPage_Appearing;
        Disappearing += CharacterStatsPage_Disappearing;

        AttackEntry.TextChanged += AttackEntry_TextChanged;
        AttackSpeedEntry.TextChanged += AttackSpeedEntry_TextChanged;
        CritChanceEntry.TextChanged += CritChanceEntry_TextChanged;
        CritDamageEntry.TextChanged += CritDamageEntry_TextChanged;
        AreaEntry.TextChanged += AreaEntry_TextChanged;
        CooldownEntry.TextChanged += CooldownEntry_TextChanged;
        PierceEntry.TextChanged += PierceEntry_TextChanged;
        BounceEntry.TextChanged += BounceEntry_TextChanged;
        DashEntry.TextChanged += DashEntry_TextChanged;
        HPEntry.TextChanged += HPEntry_TextChanged;

        //ApplyButton.Clicked += ApplyButton_Clicked;
    }

    private void CharacterStatsPage_Disappearing(object sender, EventArgs e)
    {
        MauiProgram.player.attack = int.Parse(AttackEntry.Text);
        MauiProgram.player.attackSpeed = double.Parse(AttackSpeedEntry.Text);
        MauiProgram.player.critChance = int.Parse(CritChanceEntry.Text) / 100.0;
        MauiProgram.player.critDamage = int.Parse(CritDamageEntry.Text) / 100.0;
        //MauiProgram.player.area = int.Parse(AreaEntry.Text) / 100.0;
        //MauiProgram.player.cooldown = int.Parse(CooldownEntry.Text) / 100.0;
        //MauiProgram.player.pierce = int.Parse(PierceEntry.Text);
        //MauiProgram.player.bounce = int.Parse(BounceEntry.Text);
        MauiProgram.player.hp = int.Parse(HPEntry.Text);
    }

    private void CharacterStatsPage_Appearing(object sender, EventArgs e)
    {
        AttackEntry.Text = MauiProgram.player.attack.ToString();
        AttackSpeedEntry.Text = MauiProgram.player.attackSpeed.ToString();
        CritChanceEntry.Text = (MauiProgram.player.critChance * 100).ToString();
        CritDamageEntry.Text = (MauiProgram.player.critDamage * 100).ToString();
        AreaEntry.Text = (MauiProgram.player.area * 100).ToString();
        CooldownEntry.Text = (MauiProgram.player.cooldown * 100).ToString();
        PierceEntry.Text = MauiProgram.player.pierce.ToString();
        BounceEntry.Text = MauiProgram.player.bounce.ToString();
        HPEntry.Text = MauiProgram.player.hp.ToString();
    }

    private void ApplyButton_Clicked(object sender, EventArgs e)
    {
        MauiProgram.player.attack = int.Parse(AttackEntry.Text);
        MauiProgram.player.attackSpeed = double.Parse(AttackSpeedEntry.Text);
        MauiProgram.player.critChance = int.Parse(CritChanceEntry.Text) / 100.0;
        MauiProgram.player.critDamage = int.Parse(CritDamageEntry.Text) / 100.0;
        //MauiProgram.player.area = int.Parse(AreaEntry.Text) / 100.0;
        //MauiProgram.player.cooldown = int.Parse(CooldownEntry.Text) / 100.0;
        //MauiProgram.player.pierce = int.Parse(PierceEntry.Text);
        //MauiProgram.player.bounce = int.Parse(BounceEntry.Text);
        MauiProgram.player.hp = int.Parse(HPEntry.Text);
    }

    private void AttackEntry_TextChanged(object sender, TextChangedEventArgs e)
    {
        int attack = 0;
        try
        {
            if (string.IsNullOrEmpty(e.NewTextValue))
                attack = 0;
            else
                attack = int.Parse(e.NewTextValue);
        }
        catch { attack = -1; }
        if (attack == -1)
            AttackEntry.Text = e.OldTextValue;
    }
    private void AttackSpeedEntry_TextChanged(object sender, TextChangedEventArgs e)
    {
        double attackSpeed = 0;
        try
        {
            if (string.IsNullOrEmpty(e.NewTextValue))
                attackSpeed = 0;
            else
                attackSpeed = double.Parse(e.NewTextValue);
        }
        catch { attackSpeed = -1; }
        if (attackSpeed == -1)
            AttackSpeedEntry.Text = e.OldTextValue;
    }
    private void CritChanceEntry_TextChanged(object sender, TextChangedEventArgs e)
    {
        int critchance = 0;
        try
        {
            if (string.IsNullOrEmpty(e.NewTextValue))
                critchance = 0;
            else
                critchance = int.Parse(e.NewTextValue);
        }
        catch { critchance = -1; }
        if (critchance == -1)
            CritChanceEntry.Text = e.OldTextValue;
        if (critchance > 100)
            CritChanceEntry.Text = 100.ToString();
    }
    private void CritDamageEntry_TextChanged(object sender, TextChangedEventArgs e)
    {
        int critdamage = 0;
        try
        {
            if (string.IsNullOrEmpty(e.NewTextValue))
                critdamage = 0;
            else
                critdamage = int.Parse(e.NewTextValue);
        }
        catch { critdamage = -1; }
        if (critdamage == -1)
            CritDamageEntry.Text = e.OldTextValue;
    }
    private void AreaEntry_TextChanged(object sender, TextChangedEventArgs e)
    {

    }
    private void CooldownEntry_TextChanged(object sender, TextChangedEventArgs e)
    {

    }
    private void PierceEntry_TextChanged(object sender, TextChangedEventArgs e)
    {

    }
    private void BounceEntry_TextChanged(object sender, TextChangedEventArgs e)
    {

    }
    private void DashEntry_TextChanged(object sender, TextChangedEventArgs e)
    {

    }
    private void HPEntry_TextChanged(object sender, TextChangedEventArgs e)
    {
        int hp = 0;
        try
        {
            if (string.IsNullOrEmpty(e.NewTextValue))
                hp = 0;
            else
                hp = int.Parse(e.NewTextValue);
        }
        catch { hp = -1; }
        if (hp == -1)
            HPEntry.Text = e.OldTextValue;
    }
}