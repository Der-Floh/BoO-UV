using System.Collections.ObjectModel;

namespace BoO_UV;

public partial class StackGrid : ContentView
{
    //public int amount { get; set; }

    private double _newHeight;
    public double newHeight
    {
        get => _newHeight;
        set
        {
            _newHeight = value;
            Rebuild();
        }
    }

    public static readonly BindableProperty ParentHeightProperty = BindableProperty.Create(nameof(ParentHeight), typeof(double), typeof(StackGrid), 0.0);
    public double ParentHeight
    {
        get => (double)GetValue(ParentHeightProperty);
        set
        {
            SetValue(ParentHeightProperty, value);
            OnPropertyChanged(nameof(ParentHeight));
        }
    }

    private List<IView> views = new List<IView>();
    public StackGrid()
    {
        InitializeComponent();
        PropertyChanged += StackGrid_PropertyChanged;
    }

    public void Bind(ContentPage contentPage)
    {
        BindingContext = contentPage;
        this.SetBinding(ParentHeightProperty, nameof(contentPage.Height));
    }
    public void Bind(ContentView contentView)
    {
        BindingContext = contentView;
        this.SetBinding(ParentHeightProperty, nameof(contentView.Height));
    }

    private void StackGrid_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(ParentHeight) && ParentHeight > 0)
        {
            newHeight = ParentHeight / 2;
        }
    }

    public void Rebuild(IView view = null)
    {
        if (view != null)
            views.Add(view);
        stackGrid.Clear();
        stackGrid.ColumnDefinitions.Clear();
        stackGrid.RowDefinitions.Clear();

        int i = 0;
        int j = 0;
        stackGrid.AddRowDefinition(new RowDefinition(newHeight));
        for (i = 0; i < Globals.upgradeAmount; i++)
        {
            stackGrid.AddColumnDefinition(new ColumnDefinition(GridLength.Star));
        }
        i = 0;
        foreach (IView currview in views)
        {
            stackGrid.Add(currview, i, j);
            i++;
            if (i >= Globals.upgradeAmount)
            {
                j++;
                i = 0;
                stackGrid.AddRowDefinition(new RowDefinition(newHeight));
            }
        }
    }

    public void Clear()
    {
        views.Clear();
    }

    //public void Add(IView view)
    //{
    //	views.Add(view);
    //	stackGrid.Clear();
    //	stackGrid.ColumnDefinitions.Clear();
    //	stackGrid.RowDefinitions.Clear();

    //	int i = 0;
    //	int j = 0;
    //	stackGrid.AddRowDefinition(new RowDefinition(GridLength.Star));
    //	for (i = 0; i < amount; i++)
    //	{
    //		stackGrid.AddColumnDefinition(new ColumnDefinition(GridLength.Star));
    //	}
    //	i = 0;
    //	foreach (IView currview in views)
    //	{
    //		stackGrid.Add(currview, i, j);
    //		i++;
    //		if (i >= amount)
    //		{
    //			j++;
    //			i = 0;
    //               stackGrid.AddRowDefinition(new RowDefinition(GridLength.Star));
    //           }
    //	}
    //}
}