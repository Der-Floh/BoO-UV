namespace BoO_UV;

public partial class ObjectView : ContentView
{
    public Object currobject
    {
        get { return _currobject; }
        set
        {
            _currobject = value;
            UpdateObject(_currobject.rarity);
            typeImg.Source = currobject.imagePath;
            rarityImg.Source = currobject.rarityPath;
            holderImg.Source = currobject.holderPath;
        }
    }
    private Object _currobject;
    private List<Label> labels = new List<Label>();
    private EmbedView embedView;
    private MainPage mainPage;
    public ObjectView(Object currobject, in EmbedView embedView, in MainPage mainPage)
    {
        InitializeComponent();
        MauiProgram.player.onGetObject += OnGetObject;
        this.currobject = currobject;
        this.embedView = embedView;
        this.mainPage = mainPage;
    }

    private void UpdateObject(byte rarity)
    {
        labels.Clear();
        AddLabels("Curr");

        MauiProgram.player.AddObject(currobject);

        AddLabels("New");

        MauiProgram.player.RemoveObject(currobject);

        labels.Add(new Label { Text = "DpS when Taken: " + MauiProgram.player.GetDps(null, currobject).ToString() });

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

    private void AddLabels(string prefix)
    {
        if (currobject.attackBaseAdd != 0 || currobject.attackMultiplicator != 0)
            labels.Add(new Label { Text = prefix + " Damage: " + Math.Round(MauiProgram.player.attackCalc, MauiProgram.roundingPrecision) });

        if (currobject.attackSpeedBaseAdd != 0 || currobject.attackSpeedMultiplicator != 0)
            labels.Add(new Label { Text = prefix + " Attackspeed: " + Math.Round(MauiProgram.player.attackSpeedCalc, MauiProgram.roundingPrecision) });

        if (currobject.critChanceBaseAdd != 0)
            labels.Add(new Label { Text = prefix + " Crit-Chance: " + Math.Round(MauiProgram.player.critChance * 100, MauiProgram.roundingPrecision) + "%" });

        if (currobject.critDamageBaseAdd != 0)
            labels.Add(new Label { Text = prefix + " Crit-Damage: " + Math.Round(MauiProgram.player.critDamage * 100, MauiProgram.roundingPrecision) + "%" });

        if (currobject.areaBaseAdd != 0 || currobject.areaMultiplicator != 0)
            labels.Add(new Label { Text = prefix + " Area: " + Math.Round(MauiProgram.player.areaCalc * 100, MauiProgram.roundingPrecision) + "%" });

        if (currobject.hpAdd != 0)
            labels.Add(new Label { Text = prefix + " HP: " + MauiProgram.player.hp });

        if (currobject.pierceAdd != 0)
            labels.Add(new Label { Text = prefix + " Pierce: " + MauiProgram.player.pierce });

        if (currobject.bounceAdd != 0)
            labels.Add(new Label { Text = prefix + " Bounce: " + MauiProgram.player.bounce });
    }

    private void OnButtonClicked(object sender, EventArgs args)
    {
        MauiProgram.player.AddObject(currobject, true);
        mainPage.objectViews.Remove(this);
        foreach (UpgradeView view in mainPage.upgradeViews)
        {
            view.UpdateValues();
        }
        embedView.IsVisible = false;
    }
    private void OnGetObject(object source, ObjectEventArgs args)
    {
        
    }
}