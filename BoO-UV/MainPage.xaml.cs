using Microsoft.VisualBasic;

namespace BoO_UV;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
        Globals.Init(EmbedView);
        Globals.wantedUpgrades.Add(UpgradeType.damage);
        Globals.wantedUpgrades.Add(UpgradeType.attackSpeed);
        Globals.wantedUpgrades.Add(UpgradeType.critChance);
        Globals.wantedUpgrades.Add(UpgradeType.critDamage);
        Globals.wantedUpgrades.Add(UpgradeType.hp);
        Globals.CreateCurrentExistingObjects();
        CreateDefaultUpgrades();
        Globals.CreateDefaultObjects();
        Appearing += MainPage_Appearing;
        UpgradeGrid.Bind(this);
        ObjectStackGrid.Bind(EmbedView);
    }

    private void MainPage_Appearing(object sender, EventArgs e)
    {
        foreach (UpgradeView view in Globals.upgradeViews)
        {
            view.UpdateValues();
        }
    }

    public void CreateDefaultUpgrades()
    {
        /*
        for (int i = 0; i < 5; i++)
        {
            Upgrade upgrade = new Upgrade { type = (UpgradeType)i };
            Globals.upgradeViews.Add(new UpgradeView(upgrade));
            UpgradeGrid.Rebuild(Globals.upgradeViews[i]);
        }*/
        int i = 0;
        foreach (UpgradeType upgradeType in (UpgradeType[]) Enum.GetValues(typeof(UpgradeType)))
        {
            if (Globals.wantedUpgrades.Contains(upgradeType))
            {
                Upgrade upgrade = new Upgrade { type = upgradeType };
                Globals.upgradeViews.Add(new UpgradeView(upgrade));
                UpgradeGrid.Rebuild(Globals.upgradeViews[i]);
                i++;
            }
        }
    }

    public void ResetObjects()
    {
        Globals.objectList.Clear();
        JsonHandler jsonHandler = new JsonHandler();
        string path = Path.Combine(jsonHandler.directorypath, jsonHandler.directoryname, jsonHandler.libdirectoryname, jsonHandler.objectdirectoryname);
        DirectoryInfo directoryInfo = new DirectoryInfo(path);
        directoryInfo.Delete(true);
        Globals.CreateCurrentExistingObjects();
    }
    private void AddObjectButtonClicked(object sender, EventArgs args)
    {
        ObjectStackGrid.Clear();
        foreach (ObjectView objectView in Globals.objectViews)
        {
            objectView.UpdateValues();
            ObjectStackGrid.Rebuild(objectView);
        }
        /*
        foreach (Object currobject in possibleObjects)
        {
            objectView = new ObjectView(currobject, EmbedView, this);
            ObjectStackGrid.Rebuild(objectView);
        }
        */
        EmbedView.UpdateContent();
        EmbedView.IsVisible = true;
    }
}

