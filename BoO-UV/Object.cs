namespace BoO_UV;

public sealed class Object
{
    public string name { get; set; }
    public string description { get; set; }
    public byte rarity { get; set; }
    public string imagePath { get; set; }
    public string holderPath { get; set; }
    public string rarityPath { get; set; }
    public bool multiplicatorsFromHP { get; set; }
    public int cooldown { get; set; }
    public int attackBaseAdd { get; set; }
    public double attackMultiplicator { get; set; }
    public double attackMultiplicatorCalc
    {
        get
        {
            if (multiplicatorsFromHP)
                return attackMultiplicator * Globals.player.hp + 1;
            else
                return attackMultiplicator;
        }
        set { }
    }
    public int projectileAdd { get; set; }
    public double attackSpeedBaseAdd { get; set; }
    public double attackSpeedMultiplicator { get; set; }
    public double critChanceBaseAdd { get; set; }
    public double critDamageBaseAdd { get; set; }
    public double cooldownBaseAdd { get; set; }
    public double cooldownMultiplicator { get; set; }
    public double areaBaseAdd { get; set; }
    public double areaMultiplicator { get; set; }
    public int pierceAdd { get; set; }
    public int bounceAdd { get; set; }
    public int hpAdd { get; set; }
    public int resurrectAdd { get; set; }
    public int dashAdd { get; set; }
    public double moveSpeedMultiplicator { get; set; }
    public double moveSpeedMultiplicatorCalc
    {
        get
        {
            if (multiplicatorsFromHP)
                return moveSpeedMultiplicator * Globals.player.hp + 1;
            else
                return moveSpeedMultiplicator;
        }
        set { }
    }
    public bool hasEffect
    {
        get
        {
            return attackBaseAdd != 0 || attackMultiplicator != 0 || attackSpeedBaseAdd != 0 || attackSpeedMultiplicator != 0 || critChanceBaseAdd != 0 || critDamageBaseAdd != 0 || areaBaseAdd != 0 || areaMultiplicator != 0 || hpAdd != 0 || pierceAdd != 0 || bounceAdd != 0 || projectileAdd != 0;
        }
        set { }
    }
    public ObjectView objectView;

    public Object(string name, int rarity)
    {
        this.name = name;

        if (rarity >= 0)
        {
            this.rarity = (byte)rarity;
            SetDefaultImagePaths();
        }
        AddToObjectList();
    }
    public Object() { }

    public void AddToObjectList()
    {
        if (Globals.objectList.Find(x => x.name == name) != null) return;
        Globals.objectList.Add(this);
    }

    private void SetDefaultImagePaths()
    {
        imagePath = name + ".png";
        switch (rarity)
        {
            case 0: 
                holderPath = "object_holder_common.png";
                rarityPath = "object_rarity_common.png";
                break;
            case 1:
                holderPath = "object_holder_rare.png";
                rarityPath = "object_rarity_rare.png";
                break;
            case 2:
                holderPath = "object_holder_epic.png";
                rarityPath = "object_rarity_epic.png";
                break;
            case 3:
                holderPath = "object_holder_legendary.png";
                rarityPath = "object_rarity_legendary.png";
                break;
        }
    }
}
