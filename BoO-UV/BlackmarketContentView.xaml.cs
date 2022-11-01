namespace BoO_UV;

public partial class BlackmarketContentView : ContentView
{
    public int value
    {
        get { return _value; }
        set
        {
            _value = value;
            if (isPercent)
                ContentAmountLabel.Text = "+" + value.ToString() + "%";
            else
                ContentAmountLabel.Text = value.ToString();
        }
    }
    private int _value;
    public int amount
    {
        get { return _amount; }
        set
        {
            int add = value - _amount;
            if (_amount == maxAmount && add > 0) return;
            if (_amount == 0 && add < 0) return;
            _amount = value;
            if (add < 0)
                this.value -= amountAdd;
            else
                this.value += amountAdd;
        }
    }
    private int _amount;
    public int maxAmount { get; set; }
    public int cost
    {
        get { return _cost; }
        set
        {
            _cost = value;
            /*ContentCostLabel.Text = value.ToString();*/
        }
    }
    private int _cost;
    public string typeText { get { return _typeText; } set { _typeText = value; ContentTypeLabel.Text = value; } }
    private string _typeText;
    public int amountAdd { get; set; }
    public bool isPercent
    {
        get { return _ispercent; }
        set
        {
            _ispercent = value;
            if (isPercent)
                ContentAmountLabel.Text = "+" + this.value.ToString() + "%";
            else
                ContentAmountLabel.Text = this.value.ToString();
        }
    }
    private bool _ispercent;
    public BlackmarketContentView()
	{
		InitializeComponent();
        ContentAmountLabel.Text = value.ToString();
	}

	private void ContentRemoveButton_Clicked(object sender, EventArgs e)
	{
        amount--;
        SetVisualStateNormal(sender);
    }
    private void ContentAddButton_Clicked(object sender, EventArgs e)
    {
        amount++;
        SetVisualStateNormal(sender);
    }

    private void SetVisualStateNormal(object sender)
    {
        ImageButton button = (ImageButton)sender;
        Dispatcher.DispatchDelayed(TimeSpan.FromMilliseconds(50), () =>
        {
            VisualStateManager.GoToState(button, "Normal");
        });
    }
}