namespace BoO_UV;

public partial class EmbedView : ContentView
{

    public static readonly BindableProperty ContentToShowProperty = BindableProperty.Create(nameof(ContentToShow), typeof(IView), typeof(EmbedView));
    public IView ContentToShow
    {
        get => (IView)GetValue(ContentToShowProperty);
        set
        {
            SetValue(ContentToShowProperty, value);
            OnPropertyChanged(nameof(ContentToShow));
        }
    }

    public EmbedView()
    {
        InitializeComponent();
        PropertyChanged += ObjectView_PropertyChanged;
    }

    private void ObjectView_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(ContentToShow))
            UpdateContent();
    }

    public void UpdateContent()
    {
        if (ContentToShow == null)
            return;

        ObjectContent.Clear();
        ObjectContent.Add(ContentToShow);
    }

    private void OnCancelButtonClicked(object sender, EventArgs args)
    {
        IsVisible = false;
    }
}