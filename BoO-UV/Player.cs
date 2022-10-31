using Microsoft.VisualBasic;
using System;

namespace BoO_UV
{
    public sealed class Player
    {
        public event OnPlayerGetUpgrade onGetUpgrade;
        public event OnPlayerGetObject onGetObject;
        public int attack { get; set; }
        public double attackCalc
        {
            get
            {
                double attackTemp = attack;
                foreach (Object currObject in objects.Where(x => x.attackMultiplicator != 0))
                {
                    attackTemp *= currObject.attackMultiplicator;
                }
                return attackTemp;
            }
        }
        public int projectileCount { get; set; }
        public double attackSpeed { get; set; }
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
        public double critChance { get; set; }
        public double critDamage { get; set; }
        public int hp { get; set; }
        public int pierce { get; set; }
        public int bounce { get; set; }
        public double cooldown { get; set; }
        public double area { get; set; }
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
        public int resurrect { get; set; }
        public List<Object> objects { get; set; } = new List<Object>();

        public Player(int attack = 40, double attackSpeed = 1.5, double critChance = 0.05, double critDamage = 2, int hp = 3, int pierce = 0, int bounce = 0, double cooldown = 1, double area = 1, int resurrect = 0, int projectileCount = 1)
        {
            this.attack = attack;
            this.attackSpeed = attackSpeed;
            this.critChance = critChance;
            this.critDamage = critDamage;
            this.hp = hp;
            this.pierce = pierce;
            this.bounce = bounce;
            this.cooldown = cooldown;
            this.area = area;
            this.resurrect = resurrect;
            this.projectileCount = projectileCount;
        }

        public void AddUpgrade(Upgrade upgrade, bool throwGetEvent = false)
        {
            switch (upgrade.type)
            {
                case UpgradeType.damage: AddDamageUpgrade(upgrade.rarity, true); break;
                case UpgradeType.attackSpeed: AddAttackSpeedUpgrade(upgrade.rarity, true); break;
                case UpgradeType.critChance: AddCritChanceUpgrade(upgrade.rarity, true); break;
                case UpgradeType.critDamage: AddCritDamageUpgrade(upgrade.rarity, true); break;
                case UpgradeType.hp: AddHPUpgrade(upgrade.rarity, true); break;
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
            }
            return 0;
        }

        private void AddDamageUpgrade(byte rarity, bool add)
        {
            if (add)
                attack += GetDamageUpgrade(rarity);
            else
                attack -= GetDamageUpgrade(rarity);
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
                attackSpeed = Math.Round(attackSpeed + GetAttackSpeedUpgrade(rarity), Globals.roundingPrecision);
            else
                attackSpeed = Math.Round(attackSpeed - GetAttackSpeedUpgrade(rarity), Globals.roundingPrecision);
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
                critChance = Math.Round(critChance + GetCritChanceUpgrade(rarity), Globals.roundingPrecision);
            else
                critChance = Math.Round(critChance - GetCritChanceUpgrade(rarity), Globals.roundingPrecision);
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
                critDamage = Math.Round(critDamage + GetCritDamageUpgrade(rarity), Globals.roundingPrecision);
            else
                critDamage = Math.Round(critDamage - GetCritDamageUpgrade(rarity), Globals.roundingPrecision);
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
                hp += GetHPUpgrade(rarity);
            else
                hp -= GetHPUpgrade(rarity);
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

        public void AddObject(Object currobject, bool throwGetEvent = false)
        {
            attack += currobject.attackBaseAdd;
            projectileCount += currobject.projectileAdd;
            attackSpeed += currobject.attackSpeedBaseAdd;
            critChance += currobject.critChanceBaseAdd;
            critDamage += currobject.critDamageBaseAdd;
            hp += currobject.hpAdd;
            pierce += currobject.pierceAdd;
            bounce += currobject.bounceAdd;
            area += currobject.areaBaseAdd;
            objects.Add(currobject);
            if (throwGetEvent)
                onGetObject(this, new ObjectEventArgs(currobject));
        }
        public void RemoveObject(Object currobject)
        {
            attack -= currobject.attackBaseAdd;
            projectileCount -= currobject.projectileAdd;
            attackSpeed -= currobject.attackSpeedBaseAdd;
            critChance -= currobject.critChanceBaseAdd;
            critDamage -= currobject.critDamageBaseAdd;
            hp -= currobject.hpAdd;
            pierce -= currobject.pierceAdd;
            bounce -= currobject.bounceAdd;
            area -= currobject.areaBaseAdd;
            objects.Remove(currobject);
        }

        public double GetDps(Upgrade currupgrade = null, Object currobject = null)
        {
            if (currupgrade != null) AddUpgrade(currupgrade);
            if (currobject != null) AddObject(currobject);

            double dps = attackCalc * attackSpeedCalc;

            double dpsCrit = dps * critDamage;

            dps = dps * (1 - critChance) + dpsCrit * critChance;

            dps *= projectileCount;

            if (currupgrade != null) RemoveUpgrade(currupgrade);
            if (currobject != null) RemoveObject(currobject);
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
}
