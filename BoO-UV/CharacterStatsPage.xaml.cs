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

        ResetButton.Clicked += ResetButton_Clicked;

        PrevCharacterButton.Clicked += PrevCharacterButton_Clicked;
        NextCharacterButton.Clicked += NextCharacterButton_Clicked;
    }

    private void PrevCharacterButton_Clicked(object sender, EventArgs e)
    {
        throw new NotImplementedException();
    }

    private void NextCharacterButton_Clicked(object sender, EventArgs e)
    {
        
    }

    private void CharacterStatsPage_Disappearing(object sender, EventArgs e)
    {
        Globals.player.attack = int.Parse(AttackEntry.Text) - Globals.player.character.attack;
        Globals.player.attackSpeed = double.Parse(AttackSpeedEntry.Text) - Globals.player.character.attackSpeed;
        Globals.player.critChance = int.Parse(CritChanceEntry.Text) / 100.0 - Globals.player.character.critChance;
        Globals.player.critDamage = int.Parse(CritDamageEntry.Text) / 100.0 - Globals.player.character.critDamage;
        Globals.player.area = int.Parse(AreaEntry.Text) / 100.0 - Globals.player.character.area;
        Globals.player.cooldown = int.Parse(CooldownEntry.Text) / 100.0 - Globals.player.character.cooldown;
        Globals.player.pierce = int.Parse(PierceEntry.Text) - Globals.player.character.pierce;
        Globals.player.bounce = int.Parse(BounceEntry.Text) - Globals.player.character.bounce;
        Globals.player.hp = int.Parse(HPEntry.Text) - Globals.player.character.hp;
    }

    private void CharacterStatsPage_Appearing(object sender, EventArgs e)
    {
        AttackEntry.Text = Globals.player.attack.ToString();
        AttackSpeedEntry.Text = Math.Round(Globals.player.attackSpeed, Globals.roundingPrecision).ToString();
        CritChanceEntry.Text = (Math.Round(Globals.player.critChance * 100.0, Globals.roundingPrecision)).ToString();
        CritDamageEntry.Text = (Math.Round(Globals.player.critDamage * 100.0, Globals.roundingPrecision)).ToString();
        AreaEntry.Text = (Math.Round(Globals.player.area * 100.0, Globals.roundingPrecision)).ToString();
        CooldownEntry.Text = (Math.Round(Globals.player.cooldown * 100.0, Globals.roundingPrecision)).ToString();
        PierceEntry.Text = Globals.player.pierce.ToString();
        BounceEntry.Text = Globals.player.bounce.ToString();
        HPEntry.Text = Globals.player.hp.ToString();
    }

    private void ResetButton_Clicked(object sender, EventArgs e)
    {
        //todo reset outdated
        Player newPlayer = new Player(Globals.characterList.Find(x => x.name == "ranger"));
        Globals.player.attack = newPlayer.attack;
        Globals.player.projectileCount = 1;
        Globals.player.attackSpeed = newPlayer.attackSpeed;
        Globals.player.critChance = newPlayer.critChance;
        Globals.player.critDamage = newPlayer.critDamage;
        Globals.player.hp = newPlayer.hp;
        Globals.player.pierce = newPlayer.pierce;
        Globals.player.bounce = newPlayer.bounce;
        Globals.player.cooldown = newPlayer.cooldown;
        Globals.player.area = newPlayer.area;
        Globals.player.resurrect = newPlayer.resurrect;
        Globals.player.objects.Clear();
        Globals.objectViews.Clear();
        Globals.RecalculatePossibleObjects();
        Globals.CreateDefaultObjects();

        CharacterStatsPage_Appearing(this, new EventArgs());

        Button button = (Button)sender;
        Dispatcher.DispatchDelayed(TimeSpan.FromMilliseconds(50), () =>
        {
            VisualStateManager.GoToState(button, "Normal");
        });
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
        double critchance = 0;
        try
        {
            if (string.IsNullOrEmpty(e.NewTextValue))
                critchance = 0;
            else
                critchance = double.Parse(e.NewTextValue);
        }
        catch { critchance = -1; }
        if (critchance == -1)
            CritChanceEntry.Text = e.OldTextValue;
        if (critchance > 100)
            CritChanceEntry.Text = 100.ToString();
    }
    private void CritDamageEntry_TextChanged(object sender, TextChangedEventArgs e)
    {
        double critdamage = 0;
        try
        {
            if (string.IsNullOrEmpty(e.NewTextValue))
                critdamage = 0;
            else
                critdamage = double.Parse(e.NewTextValue);
        }
        catch { critdamage = -1; }
        if (critdamage == -1)
            CritDamageEntry.Text = e.OldTextValue;
    }
    private void AreaEntry_TextChanged(object sender, TextChangedEventArgs e)
    {
        double area = 0;
        try
        {
            if (string.IsNullOrEmpty(e.NewTextValue))
                area = 0;
            else
                area = double.Parse(e.NewTextValue);
        }
        catch { area = -1; }
        if (area == -1)
            AreaEntry.Text = e.OldTextValue;
    }
    private void CooldownEntry_TextChanged(object sender, TextChangedEventArgs e)
    {
        double cooldown = 0;
        try
        {
            if (string.IsNullOrEmpty(e.NewTextValue))
                cooldown = 0;
            else
                cooldown = double.Parse(e.NewTextValue);
        }
        catch { cooldown = -1; }
        if (cooldown == -1)
            CooldownEntry.Text = e.OldTextValue;
    }
    private void PierceEntry_TextChanged(object sender, TextChangedEventArgs e)
    {
        int pierce = 0;
        try
        {
            if (string.IsNullOrEmpty(e.NewTextValue))
                pierce = 0;
            else
                pierce = int.Parse(e.NewTextValue);
        }
        catch { pierce = -1; }
        if (pierce == -1)
            PierceEntry.Text = e.OldTextValue;
    }
    private void BounceEntry_TextChanged(object sender, TextChangedEventArgs e)
    {
        int bounce = 0;
        try
        {
            if (string.IsNullOrEmpty(e.NewTextValue))
                bounce = 0;
            else
                bounce = int.Parse(e.NewTextValue);
        }
        catch { bounce = -1; }
        if (bounce == -1)
            BounceEntry.Text = e.OldTextValue;
    }
    private void DashEntry_TextChanged(object sender, TextChangedEventArgs e)
    {
        int dash = 0;
        try
        {
            if (string.IsNullOrEmpty(e.NewTextValue))
                dash = 0;
            else
                dash = int.Parse(e.NewTextValue);
        }
        catch { dash = -1; }
        if (dash == -1)
            DashEntry.Text = e.OldTextValue;
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