using Microsoft.VisualBasic;
using System;

namespace BoO_UV;

public sealed class Player
{
    public event OnPlayerGetUpgrade onGetUpgrade;
    public event OnPlayerGetObject onGetObject;
    public int attackBase { get { return _attack; } set { _attack = value; } }
    public int attack { get { if (character != null) return character.attack + _attack; else return _attack; } set { _attack = value; } }
    private int _attack;
    public double attackCalc
    {
        get
        {
            double attackTemp = attack;
            foreach (Object currObject in objects.Where(x => x.attackMultiplicator != 0))
            {
                attackTemp *= currObject.attackMultiplicatorCalc;
            }
            return attackTemp;
        }
    }
    public int projectileCountBase { get { return _projectileCount; } set { _projectileCount = value; } }
    public int projectileCount { get { if (character != null) return character.projectileCount + _projectileCount; else return _projectileCount; } set { _projectileCount = value; } }
    private int _projectileCount;
    public double attackSpeedbase { get { return _attackSpeed; } set { _attackSpeed = value; } }
    public double attackSpeed { get { if (character != null) return character.attackSpeed + _attackSpeed; else return _attackSpeed; } set { _attackSpeed = value; } }
    private double _attackSpeed;
    public double attackSpeedCalc
    {
        get
        {
            double attackSpeedTemp = attackSpeed;
            foreach (Object currObject in objects.Where(x => x.attackSpeedMultiplicator != 0))
            {
                attackSpeedTemp *= currObject.attackSpeedMultiplicator;
            }
            return attackSpeedTemp;
        }
    }
    public double critChanceBase { get { return _critChance; } set { _critChance = value; } }
    public double critChance { get { if (character != null) return character.critChance + _critChance; else return _critChance; } set { _critChance = value; } }
    private double _critChance;
    public double critDamageBase { get { return _critDamage; } set { _critDamage = value; } }
    public double critDamage { get { if (character != null) return character.critDamage + _critDamage; else return _critDamage; } set { _critDamage = value; } }
    private double _critDamage;
    public int hpBase { get { return _hp; } set { _hp = value; } }
    public int hp { get { if (character != null) return character.hp + _hp; else return _hp; } set { _hp = value; } }
    private int _hp;
    public int pierceBase { get { return _pierce; }set { _pierce = value; } }
    public int pierce { get { if (character != null) return character.pierce + _pierce; else return _pierce; } set { _pierce = value; } }
    private int _pierce;
    public int bounceBase { get { return _bounce; } set { _bounce = value; } }
    public int bounce { get { if (character != null) return character.bounce + _bounce; else return _bounce; } set { _bounce = value; } }
    private int _bounce;
    public double cooldownBase { get { return _cooldown; } set { _cooldown = value; } }
    public double cooldown { get { if (character != null) return character.cooldown + _cooldown; else return _cooldown; } set { _cooldown = value; } }
    private double _cooldown;
    public double areaBase { get { return _area; } set { _area = value; } }
    public double area { get { if (character != null) return character.area + _area; else return _area; } set { _area = value; } }
    private double _area;
    public double areaCalc
    {
        get
        {
            double areaTemp = area;
            foreach (Object currObject in objects.Where(x => x.areaMultiplicator != 0))
            {
                areaTemp *= currObject.areaMultiplicator;
            }
            return areaTemp;
        }
    }
    public int resurrectBase { get { return _resurrect; } set { _resurrect = value; } }
    public int resurrect { get { if (character != null) return character.resurrect + _resurrect; else return _resurrect; } set { _resurrect = value; } }
    private int _resurrect;
    public double moveSpeedBase { get { return _moveSpeed; } set { _moveSpeed = value; } }
    public double moveSpeed { get { if (character != null) return character.moveSpeed + _moveSpeed; else return _moveSpeed; } set { _moveSpeed = value; } }
    private double _moveSpeed;
    public double moveSpeedCalc
    {
        get
        {
            double moveSpeedTemp = moveSpeed;
            foreach (Object currObject in objects.Where(x => x.moveSpeedMultiplicator != 0))
            {
                moveSpeedTemp *= currObject.moveSpeedMultiplicatorCalc;
            }
            return moveSpeedTemp;
        }
    }
    public double pickupRangeBase { get { return _pickupRange; } set { _pickupRange = value; } }
    public double pickupRange { get { if (character != null) return character.pickupRange + _pickupRange; else return _pickupRange; } set { _pickupRange = value; } }
    private double _pickupRange;
    public int dashBase { get { return _dash; } set { _dash = value; } }
    public int dash { get { if (character != null) return character.dash + _dash; else return _dash; } set { _dash = value; } }
    private int _dash;
    public List<Object> objects { get; set; } = new List<Object>();
    public Character character 
    {
        get {  return _character; }
        set
        {
            if (_character != null)
                if (_character.startObject != null)
                    RemoveObject(_character.startObject);
            _character = value;
            if (_character.startObject != null && _character.startObject.hasEffect)
                AddObject(_character.startObject);
        }
    }
    private Character _character;

    public Player() { }

    public void AddUpgrade(Upgrade upgrade, bool throwGetEvent = false)
    {
        switch (upgrade.type)
        {
            case UpgradeType.damage: AddDamageUpgrade(upgrade.rarity, true); break;
            case UpgradeType.attackSpeed: AddAttackSpeedUpgrade(upgrade.rarity, true); break;
            case UpgradeType.critChance: AddCritChanceUpgrade(upgrade.rarity, true); break;
            case UpgradeType.critDamage: AddCritDamageUpgrade(upgrade.rarity, true); break;
            case UpgradeType.hp: AddHPUpgrade(upgrade.rarity, true); break;
            case UpgradeType.cooldown: AddCooldownUpgrade(upgrade.rarity, true); break;
            case UpgradeType.area: AddAreaUpgrade(upgrade.rarity, true); break;
            case UpgradeType.pierce: AddPierceUpgrade(upgrade.rarity, true); break;
            case UpgradeType.bounce: AddBounceUpgrade(upgrade.rarity, true); break;
            case UpgradeType.resurrect: AddResurrectUpgrade(upgrade.rarity, true); break;
            case UpgradeType.moveSpeed: AddMoveSpeedUpgrade(upgrade.rarity, true); break;
        }
        if (throwGetEvent)
            onGetUpgrade(this, new UpgradeEventArgs(upgrade));
    }
    public void RemoveUpgrade(Upgrade upgrade)
    {
        switch (upgrade.type)
        {
            case UpgradeType.damage: AddDamageUpgrade(upgrade.rarity, false); break;
            case UpgradeType.attackSpeed: AddAttackSpeedUpgrade(upgrade.rarity, false); break;
            case UpgradeType.critChance: AddCritChanceUpgrade(upgrade.rarity, false); break;
            case UpgradeType.critDamage: AddCritDamageUpgrade(upgrade.rarity, false); break;
            case UpgradeType.hp: AddHPUpgrade(upgrade.rarity, false); break;
            case UpgradeType.cooldown: AddCooldownUpgrade(upgrade.rarity, false); break;
            case UpgradeType.area: AddAreaUpgrade(upgrade.rarity, false); break;
            case UpgradeType.pierce: AddPierceUpgrade(upgrade.rarity, false); break;
            case UpgradeType.bounce: AddBounceUpgrade(upgrade.rarity, false); break;
            case UpgradeType.resurrect: AddResurrectUpgrade(upgrade.rarity, false); break;
            case UpgradeType.moveSpeed: AddMoveSpeedUpgrade(upgrade.rarity, false); break;
        }
    }
    public double GetUpgrade(Upgrade upgrade)
    {
        switch (upgrade.type)
        {
            case UpgradeType.damage: return GetDamageUpgrade(upgrade.rarity);
            case UpgradeType.attackSpeed: return GetAttackSpeedUpgrade(upgrade.rarity);
            case UpgradeType.critChance: return GetCritChanceUpgrade(upgrade.rarity);
            case UpgradeType.critDamage: return GetCritDamageUpgrade(upgrade.rarity);
            case UpgradeType.hp: return GetHPUpgrade(upgrade.rarity);
            case UpgradeType.cooldown: return GetCooldownUpgrade(upgrade.rarity);
            case UpgradeType.area: return GetAreaUpgrade(upgrade.rarity);
            case UpgradeType.pierce: return GetPierceUpgrade(upgrade.rarity);
            case UpgradeType.bounce: return GetBounceUpgrade(upgrade.rarity);
            case UpgradeType.resurrect: return GetResurrectUpgrade(upgrade.rarity);
            case UpgradeType.moveSpeed: return GetMoveSpeedUpgrade(upgrade.rarity);
        }
        return 0;
    }

    private void AddDamageUpgrade(byte rarity, bool add)
    {
        if (add)
            _attack += GetDamageUpgrade(rarity);
        else
            _attack -= GetDamageUpgrade(rarity);
    }
    private int GetDamageUpgrade(byte rarity)
    {
        switch (rarity)
        {
            case 0: return 10;
            case 1: return 15;
            case 2: return 20;
            case 3: return 30;
        }
        return 0;
    }
    private void AddAttackSpeedUpgrade(byte rarity, bool add)
    {
        if (add)
            _attackSpeed = Math.Round(_attackSpeed + GetAttackSpeedUpgrade(rarity), Globals.roundingPrecision);
        else
            _attackSpeed = Math.Round(_attackSpeed - GetAttackSpeedUpgrade(rarity), Globals.roundingPrecision);
    }
    private double GetAttackSpeedUpgrade(byte rarity)
    {
        switch (rarity)
        {
            case 0: return 0.3;
            case 1: return 0.45;
            case 2: return 0.6;
            case 3: return 1;
        }
        return 0;
    }
    private void AddCritChanceUpgrade(byte rarity, bool add)
    {
        if (add)
            _critChance = Math.Round(_critChance + GetCritChanceUpgrade(rarity), Globals.roundingPrecision);
        else
            _critChance = Math.Round(_critChance - GetCritChanceUpgrade(rarity), Globals.roundingPrecision);
    }
    private double GetCritChanceUpgrade(byte rarity)
    {
        switch (rarity)
        {
            case 0: return 0.05;
            case 1: return 0.1;
        }
        return 0;
    }
    private void AddCritDamageUpgrade(byte rarity, bool add)
    {
        if (add)
            _critDamage = Math.Round(_critDamage + GetCritDamageUpgrade(rarity), Globals.roundingPrecision);
        else
            _critDamage = Math.Round(_critDamage - GetCritDamageUpgrade(rarity), Globals.roundingPrecision);
    }
    private double GetCritDamageUpgrade(byte rarity)
    {
        switch (rarity)
        {
            case 1: return 1;
            case 2: return 1.5;
        }
        return 0;
    }
    private void AddHPUpgrade(byte rarity, bool add)
    {
        if (add)
            _hp += GetHPUpgrade(rarity);
        else
            _hp -= GetHPUpgrade(rarity);
    }
    private int GetHPUpgrade(byte rarity)
    {
        switch (rarity)
        {
            case 1: return 1;
            case 2: return 2;
        }
        return 0;
    }
    private void AddPierceUpgrade(byte rarity, bool add)
    {
        if (add)
            _pierce += GetPierceUpgrade(rarity);
        else
            _pierce -= GetPierceUpgrade(rarity);
    }
    private int GetPierceUpgrade(byte rarity)
    {
        switch (rarity)
        {
            case 3: return 1;
        }
        return 0;
    }
    private void AddBounceUpgrade(byte rarity, bool add)
    {
        if (add)
            _bounce += GetBounceUpgrade(rarity);
        else
            _bounce -= GetBounceUpgrade(rarity);
    }
    private int GetBounceUpgrade(byte rarity)
    {
        switch (rarity)
        {
            case 3: return 1;
        }
        return 0;
    }
    private void AddCooldownUpgrade(byte rarity, bool add)
    {
        if (add)
            _cooldown *= 1.0 - GetCooldownUpgrade(rarity);
        else
            _cooldown /= 1.0 - GetCooldownUpgrade(rarity);
    }
    private double GetCooldownUpgrade(byte rarity)
    {
        switch (rarity)
        {
            case 1: return 0.1;
            case 2: return 0.15;
        }
        return 0;
    }
    private void AddAreaUpgrade(byte rarity, bool add)
    {
        if (add)
            _area += GetAreaUpgrade(rarity);
        else
            _area -= GetAreaUpgrade(rarity);
    }
    private double GetAreaUpgrade(byte rarity)
    {
        switch (rarity)
        {
            case 1: return 0.2;
            case 2: return 0.3;
            case 3: return 0.4;
        }
        return 0;
    }
    private void AddResurrectUpgrade(byte rarity, bool add)
    {
        if (add)
            _resurrect += GetResurrectUpgrade(rarity);
        else
            _resurrect -= GetResurrectUpgrade(rarity);
    }
    private int GetResurrectUpgrade(byte rarity)
    {
        switch (rarity)
        {
            case 3: return 1;
        }
        return 0;
    }
    private void AddMoveSpeedUpgrade(byte rarity, bool add)
    {
        if (add)
            _moveSpeed += GetMoveSpeedUpgrade(rarity);
        else
            _moveSpeed -= GetMoveSpeedUpgrade(rarity);
    }
    private double GetMoveSpeedUpgrade(byte rarity)
    {
        switch (rarity)
        {
            case 1: return 0.5;
        }
        return 0;
    }

    public void AddObject(Object currobject, bool throwGetEvent = false, bool updatePossibleObjects = true)
    {
        _attack += currobject.attackBaseAdd;
        _projectileCount += currobject.projectileAdd;
        _attackSpeed += currobject.attackSpeedBaseAdd;
        _critChance += currobject.critChanceBaseAdd;
        _critDamage += currobject.critDamageBaseAdd;
        _hp += currobject.hpAdd;
        _pierce += currobject.pierceAdd;
        _bounce += currobject.bounceAdd;
        _area += currobject.areaBaseAdd;
        objects.Add(currobject);
        if (updatePossibleObjects)
            Globals.possibleObjects.RemoveAll(x => x.name == currobject.name);
        if (throwGetEvent)
            onGetObject(this, new ObjectEventArgs(currobject));
    }
    public void RemoveObject(Object currobject, bool updatePossibleObjects = true)
    {
        _attack -= currobject.attackBaseAdd;
        _projectileCount -= currobject.projectileAdd;
        _attackSpeed -= currobject.attackSpeedBaseAdd;
        _critChance -= currobject.critChanceBaseAdd;
        _critDamage -= currobject.critDamageBaseAdd;
        _hp -= currobject.hpAdd;
        _pierce -= currobject.pierceAdd;
        _bounce -= currobject.bounceAdd;
        _area -= currobject.areaBaseAdd;
        objects.Remove(currobject);
        if (updatePossibleObjects)
            Globals.possibleObjects.Add(currobject);
    }

    public double GetDps(Upgrade currupgrade = null, Object currobject = null)
    {
        if (currupgrade != null) AddUpgrade(currupgrade);
        if (currobject != null) AddObject(currobject, false, false);

        double dps = attackCalc * attackSpeedCalc;

        double dpsCrit = dps * critDamage;

        dps = dps * (1 - critChance) + dpsCrit * critChance;

        dps *= projectileCount;

        if (currupgrade != null) RemoveUpgrade(currupgrade);
        if (currobject != null) RemoveObject(currobject, false);
        return Math.Round(dps, Globals.roundingPrecision);
    }
}

public delegate void OnPlayerGetUpgrade(object source, UpgradeEventArgs args);
public class UpgradeEventArgs : EventArgs
{
    private Upgrade currupgrade { get; set; }
    public UpgradeEventArgs(Upgrade currupgrade)
    {
        this.currupgrade = currupgrade;
    }
}
public delegate void OnPlayerGetObject(object source, ObjectEventArgs args);
public class ObjectEventArgs : EventArgs
{
    public Object currobject { get; set; }
    public ObjectEventArgs(Object currobject)
    {
        this.currobject = currobject;
    }
}
