namespace BoO_UV;

public enum UpgradeType
{
    damage,
    attackSpeed,
    critChance,
    critDamage,
    hp,
    area,
    cooldown,
    pierce,
    bounce,
    dash,
    choice,
    resurrect,
    moveSpeed,
    pickupRange,
}
public sealed class Upgrade
{
    public UpgradeType type
    {
        get { return _type; }
        set
        {
            _type = value;
            rarity = rarityMin;
        }
    }
    private UpgradeType _type;
    public byte rarity
    {
        get { return _rarity; }
        set
        {
            if (value > rarityMax)
                _rarity = rarityMax;
            else if (value < rarityMin)
                _rarity = rarityMin;
            else
                _rarity = value;
        }
    }
    private byte _rarity;
    public byte rarityMin
    {
        get
        {
            switch(type)
            {
                default: return 0;
                case UpgradeType.critDamage: return 1;
                case UpgradeType.hp: return 1;
                case UpgradeType.area: return 1;
                case UpgradeType.cooldown: return 1;
                case UpgradeType.pierce: return 3;
                case UpgradeType.bounce: return 3;
                case UpgradeType.dash: return 1;
                case UpgradeType.choice: return 3;
                case UpgradeType.resurrect: return 3;
                case UpgradeType.moveSpeed: return 1;
            }
        }
    }
    public byte rarityMax
    {
        get
        {
            switch (type)
            {
                default : return 3;
                case UpgradeType.critChance: return 1;
                case UpgradeType.critDamage: return 2;
                case UpgradeType.hp: return 2;
                case UpgradeType.dash: return 2;
                case UpgradeType.cooldown: return 2;
                case UpgradeType.moveSpeed: return 1;
            }
        }
    }
    public string image
    {
        get
        {
            switch (type)
            {
                case UpgradeType.damage: return "atk.png";
                case UpgradeType.attackSpeed: return "atk_spe.png";
                case UpgradeType.critChance: return "crit_chance.png";
                case UpgradeType.critDamage: return "crit_dmg.png";
                case UpgradeType.hp: return "hp.png";
                case UpgradeType.area: return "aoe_spread.png";
                case UpgradeType.cooldown: return "overall_cd.png";
                case UpgradeType.pierce: return "pierce.png";
                case UpgradeType.bounce: return "bounce.png";
                case UpgradeType.dash: return "dash_number.png";
                case UpgradeType.choice: return "choice.png";
                case UpgradeType.resurrect: return "resurrect.png";
                case UpgradeType.moveSpeed: return "speed.png";
            }
            return null;
        }
    }
    public string holderImage
    {
        get
        {
            switch(rarity)
            {
                case 0: return "holder_common.png";
                case 1: return "holder_rare.png";
                case 2: return "holder_epic.png";
                case 3: return "holder_legendary.png";
            }
            return null;
        }
    }
    public string rarityImage
    {
        get
        {
            switch (rarity)
            {
                case 0: return "rarity_common.png";
                case 1: return "rarity_rare.png";
                case 2: return "rarity_epic.png";
                case 3: return "rarity_legendary.png";
            }
            return null;
        }
    }

    public string name
    {
        get
        {
            switch (type)
            {
                case UpgradeType.damage: return "Damage";
                case UpgradeType.attackSpeed: return "Attackspeed";
                case UpgradeType.critChance: return "Crit-Chance";
                case UpgradeType.critDamage: return "Crit-Damage";
                case UpgradeType.hp: return "HP";
                case UpgradeType.area: return "Area";
                case UpgradeType.cooldown: return "Cooldown";
                case UpgradeType.pierce: return "Pierce";
                case UpgradeType.bounce: return "Bounce";
                case UpgradeType.dash: return "Dash";
                case UpgradeType.choice: return "Choice";
                case UpgradeType.resurrect: return "Resurrect";
                case UpgradeType.moveSpeed: return "Movespeed";
                case UpgradeType.pickupRange: return "Pickup-Range";
            }
            return null;
        }
    }
}
