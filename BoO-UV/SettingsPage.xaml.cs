using System.Diagnostics;

namespace BoO_UV;

public partial class SettingsPage : ContentPage
{
	public SettingsPage()
	{
		InitializeComponent();
        Appearing += SettingsPage_Appearing;
        Disappearing += SettingsPage_Disappearing;
	}

    private void SettingsPage_Appearing(object sender, EventArgs e)
    {
        UpgradeAmountEntry.Text = Globals.columnAmount.ToString();
    }

    private void SettingsPage_Disappearing(object sender, EventArgs e)
    {
        Globals.columnAmount = int.Parse(UpgradeAmountEntry.Text);
    }

    private void SaveLocationButton_Clicked(object sender, EventArgs e)
    {
        if (DeviceInfo.Current.Platform == DevicePlatform.WinUI)
        {
            JsonHandler jsonHandler = new JsonHandler();
            string path = Path.Combine(jsonHandler.directorypath, jsonHandler.directoryname);
            ProcessStartInfo startInfo = new ProcessStartInfo("explorer.exe", path);
            Process.Start(startInfo);
        }
        else
        {
            DisplayAlert("Not on Windows", "This only works for Windows", "OK");
        }
    }
    private void ResetObjectsButton_Clicked(object sender, EventArgs e)
    {
        Globals.ResetObjects();
    }

    private void ResetCharactersButton_Clicked(object sender, EventArgs e)
    {
        Globals.ResetCharacters();
    }

    private void UpgradeAmountEntry_TextChanged(object sender, TextChangedEventArgs e)
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
            UpgradeAmountEntry.Text = e.OldTextValue;
    }
}