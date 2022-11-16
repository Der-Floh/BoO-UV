namespace BoO_UV;

public sealed partial class ObjectView : ContentView
{
    public Object currobject
    {
        get { return _currobject; }
        set
        {
            _currobject = value;
            UpdateObject();
            typeImg.Source = currobject.imagePath;
            rarityImg.Source = currobject.rarityPath;
            holderImg.Source = currobject.holderPath;
        }
    }
    private Object _currobject;
    private List<Label> labels = new List<Label>();
    private EmbedView embedView;
    public ObjectView(Object currobject, in EmbedView embedView)
    {
        InitializeComponent();
        Globals.player.onGetObject += OnGetObject;
        this.currobject = currobject;
        this.embedView = embedView;
    }

    private void UpdateObject()
    {
        labels.Clear();
        AddLabels(Globals.currText);

        Globals.player.AddObject(currobject);

        AddLabels(Globals.newText);

        Globals.player.RemoveObject(currobject);

        //labels.Add(new Label { Text = "Curr dps: " + Globals.player.GetDps().ToString() });
        labels.Add(new Label { Text = Globals.dpsText + Globals.player.GetDps(null, currobject).ToString() });

        int i = 0;
        foreach (Label label in labels)
        {
            ObjectsGrid.AddRowDefinition(new RowDefinition(new GridLength(0.75, GridUnitType.Star)));
            ObjectsGrid.Add(label, 0, i + 1);
            i++;
        }
        Button TakeObjectButton = new Button();
        TakeObjectButton.Text = "Take";
        TakeObjectButton.Clicked += OnButtonClicked;
        ObjectsGrid.AddRowDefinition(new RowDefinition(new GridLength(1.5, GridUnitType.Star)));
        ObjectsGrid.Add(TakeObjectButton, 0, i + 1);
    }

    public void UpdateValues()
    {
        ObjectsGrid.Clear();
        ObjectsGrid.RowDefinitions.Clear();
        ObjectsGrid.AddRowDefinition(new RowDefinition(new GridLength(10, GridUnitType.Star)));
        ObjectsGrid.Add(holderImg, 0, 0);
        ObjectsGrid.Add(rarityImg, 0, 0);
        ObjectsGrid.Add(typeImg, 0, 0);
        UpdateObject();
    }

    private void AddLabels(string prefix)
    {
        if (currobject.attackBaseAdd != 0 || currobject.attackMultiplicator != 0)
            labels.Add(new Label { Text = prefix + "Damage: " + Math.Round(Globals.player.attackCalc, Globals.roundingPrecision) });

        if (currobject.attackSpeedBaseAdd != 0 || currobject.attackSpeedMultiplicator != 0)
            labels.Add(new Label { Text = prefix + "Attackspeed: " + Math.Round(Globals.player.attackSpeedCalc, Globals.roundingPrecision) });

        if (currobject.critChanceBaseAdd != 0)
            labels.Add(new Label { Text = prefix + "Crit-Chance: " + Math.Round(Globals.player.critChance * 100, Globals.roundingPrecision) + "%" });

        if (currobject.critDamageBaseAdd != 0)
            labels.Add(new Label { Text = prefix + "Crit-Damage: " + Math.Round(Globals.player.critDamage * 100, Globals.roundingPrecision) + "%" });

        if (currobject.cooldownBaseAdd != 0 | currobject.cooldownMultiplicator != 0)
            labels.Add(new Label { Text = prefix + "Cooldown: " + Math.Round(Globals.player.cooldown * 100, Globals.roundingPrecision) + "%" });

        if (currobject.areaBaseAdd != 0 || currobject.areaMultiplicator != 0)
            labels.Add(new Label { Text = prefix + "Area: " + Math.Round(Globals.player.areaCalc * 100, Globals.roundingPrecision) + "%" });

        if (currobject.hpAdd != 0)
            labels.Add(new Label { Text = prefix + "HP: " + Globals.player.hp });

        if (currobject.pierceAdd != 0)
            labels.Add(new Label { Text = prefix + "Pierce: " + Globals.player.pierce });

        if (currobject.bounceAdd != 0)
            labels.Add(new Label { Text = prefix + "Bounce: " + Globals.player.bounce });

        if (currobject.resurrectAdd != 0)
            labels.Add(new Label { Text = prefix + "Resurrect: " + Globals.player.resurrect });
    }

    private void OnButtonClicked(object sender, EventArgs args)
    {
        Globals.player.AddObject(currobject, true);
        Globals.objectViews.Remove(this);
        foreach (UpgradeView view in Globals.upgradeViews)
        {
            view.UpdateValues();
        }
        embedView.IsVisible = false;
    }
    private void OnGetObject(object source, ObjectEventArgs args)
    {
        
    }
}