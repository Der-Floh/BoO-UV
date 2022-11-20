using Microsoft.VisualBasic;

namespace BoO_UV;

public sealed class Character
{
    public int attack { get; set; }
    public int projectileCount { get; set; }
    public double attackSpeed { get; set; }
    public double critChance { get; set; }
    public double critDamage { get; set; }
    public int hp { get; set; }
    public int pierce { get; set; }
    public int bounce { get; set; }
    public double cooldown { get; set; }
    public double area { get; set; }
    public int resurrect { get; set; }
    public double moveSpeed { get; set; }
    public double pickupRange { get; set; }
    public int dash {  get; set; }
    public string imagePath { get; set; }
    public string name { get; set; }
    public int id { get; set; }
    public string description { get; set; }
    public byte difficulty { get; set; }
    public Object startObject {  get; set; }

    public Character(string name)
    {
        this.name = name;
        imagePath = "score_" + name + "_portrait.png";

        AddToCharacterList();
    }
    public Character() { }

    public void AddToCharacterList()
    {
        if (Globals.characterList.Find(x => x.name == name) != null) return;
            Globals.characterList.Add(this);
    }
}