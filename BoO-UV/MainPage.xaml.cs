using System.Diagnostics;

namespace BoO_UV;

public sealed partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
        Globals.Init(EmbedView, UpgradeGrid);
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
    private void DexButton_Clicked(object sender, EventArgs args)
    {
        var ps = new ProcessStartInfo("https://www.youtube.com/c/DexCitingGaming")
        {
            UseShellExecute = true,
            Verb = "open"
        };
        Process.Start(ps);
    }
}

