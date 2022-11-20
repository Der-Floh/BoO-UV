using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BoO_UV
{
    public static class Globals
    {
        public static List<Object> objectList { get; set; } = new List<Object>();
        public static List<Character> characterList { get; set; } = new List<Character>(); 
        public static List<UpgradeView> upgradeViews { get; set; } = new List<UpgradeView>();
        public static List<UpgradeType> wantedUpgrades { get; set; } = new List<UpgradeType>();
        public static List<Object> possibleObjects { get; set; } = new List<Object>();
        public static List<BlackmarketContentView> blackmarketContents { get; set; } = new List<BlackmarketContentView>();
        public static EmbedView EmbedView { get; set; }
        public static StackGrid UpgradeGrid { get; set; }
        public static Player player { get; set; }

        public static int roundingPrecision { get; set; } = 2;
        public static int columnAmount { get; set; } = 5;
        public static int rowAmount { get; set; } = 2;
        public static string currText { get; set; } = "Curr ";
        public static string newText { get; set; } = "New ";
        public static string dpsText { get; set; } = "DpS when taken: ";

        public static void Init(EmbedView _EmbedView, StackGrid _UpgradeGrid)
        {
            EmbedView = _EmbedView;
            UpgradeGrid = _UpgradeGrid;

            wantedUpgrades.Add(UpgradeType.damage);
            wantedUpgrades.Add(UpgradeType.attackSpeed);
            wantedUpgrades.Add(UpgradeType.critChance);
            wantedUpgrades.Add(UpgradeType.critDamage);
            wantedUpgrades.Add(UpgradeType.hp);

            player = new Player();

            CreateCurrentExistingBlackmarketChoices();
            CreateCurrentExistingObjects();
            CreateCurrentExistingCharacters();

            player.character = characterList.Find(x => x.name == "ranger");

            CreateDefaultUpgrades();
            CreateDefaultObjects();
        }

        public static void CreateDefaultUpgrades()
        {
            int i = 0;
            foreach (UpgradeType upgradeType in (UpgradeType[])Enum.GetValues(typeof(UpgradeType)))
            {
                if (wantedUpgrades.Contains(upgradeType))
                {
                    Upgrade upgrade = new Upgrade { type = upgradeType };
                    upgradeViews.Add(new UpgradeView(upgrade));
                    UpgradeGrid.Rebuild(upgradeViews[i]);
                    i++;
                }
            }
        }

        public static void CreateDefaultObjects()
        {
            foreach (Object currobject in possibleObjects)
            {
                currobject.objectView = new ObjectView(currobject, EmbedView);
            }
        }

        public static void CreateCurrentExistingObjects()
        {
            JsonHandler jsonHandler = new JsonHandler();
            string path = Path.Combine(jsonHandler.directorypath, jsonHandler.directoryname, jsonHandler.libdirectoryname, jsonHandler.objectdirectoryname);
            DirectoryInfo directoryInfo = new DirectoryInfo(path);
            try
            {
                foreach (FileInfo fileInfo in directoryInfo.GetFiles("*.json"))
                {
                    Object currObject = jsonHandler.ReadObject(fileInfo.FullName);
                    currObject.AddToObjectList();
                }
            }
            catch { }

            new Object("adrenaline", 1) { description = "You deal 50% more damage and your dash and abilities cooldown are reduced by 30% when you have 50% or less health left.", };
            new Object("corkscreww_bullet", 0) { pierceAdd = 2, description = "When you projectiles should be destroyed, reduces their damage by 50% and pierce instead (Max 2 times).", };
            new Object("bow_gun", 3) { attackSpeedMultiplicator = 2, description = "Standing still gradually increases your attack speed, until you move again. (Max bonus x2 after 10sec.)", };
            new Object("bronze_loaded_die", 1) { description = "Gain 3 reroll for upgrade choices", };
            new Object("dark_revenge", 3) { };
            new Object("dirty_nigel", 0) { attackMultiplicator = 1.2, description = "Permanently augments your damage by 20% but your projectiles are 50% slower.", };
            new Object("emergency_kit", 2) { cooldown = 60000, description = "Whenever you take 2 damage in a row, heal for 2 health. (Cooldown 60 sec)", };
            new Object("eye_of_the_storm", 2) { cooldown = 4000, description = "Shoot an orbiting projectile regularly.(Cooldown 4s) (Max 20 projectiles at the same time)", };
            new Object("faith_full", 0) { description = "2 shield rotate around you and blocks enemy projectiles.", };
            new Object("far_sight", 0) { attackMultiplicator = 1.3, attackSpeedMultiplicator = 0.8, description = "Permanently augments your damage by 30% but reduces your attack speed by 20%. Your projectiles are 100% faster.", };
            new Object("gluttony", 1) { attackMultiplicator = 0.1, moveSpeedMultiplicator = -0.05, multiplicatorsFromHP = true, description = "For each current health, get bigger, deal +10% more damage but move 5% slower.", };
            new Object("golden_loaded_die", 3) { description = "Gain an additional choice for upgrades and objects. This additional choice is always of rare or higher quality.", };
            new Object("hawk_eye", 3) { critChanceBaseAdd = 0.6, attackMultiplicator = 0.5, description = "Gain +60% crit chance but you deal 2 times less damage.", };
            new Object("heroes_landing", 1) { dashAdd = 1, description = "Deal area damage each time you dash. Also gain 1 dash.", };
            new Object("horseshoe_magnet", 0) { description = "Attract your collectible from further away.", };
            new Object("intimidating_bounty", 1) { description = "Emit an aura that slows enemies near you by 50%.", };
            new Object("lucky_shot", 2) { description = "When your projectiles deal critical damage they pierce.", };
            new Object("machine_gun", 3) { attackSpeedMultiplicator = 4, attackMultiplicator = 1.0 / 3.0, description = "Shoot 4 times faster but deal 3 times less damage.", };
            new Object("mezcal_defiance", 1) { cooldown = 6000, description = "When you stay still, periodically fire projectiles around you. (Cooldown 6s)", };
            new Object("mezcal_mantle", 0) { description = "Fires projectiles around you upon taking damage.", };
            new Object("mezcals_mighty_staff", 3) { attackSpeedMultiplicator = 0.5, cooldown = 10000, description = "Every 10s your next shot deal 2x more damage and explode. But divide your attack speed by 2.", };
            new Object("ninja_headband", 0) { description = "Dashing through enemies deal damage to them. Reduces the cooldown of your dashes by 30%.", };
            new Object("nitro_crit", 3) { attackMultiplicator = 1.1, description = "Deal 40% area damage each time your projectiles inflict a critical strike but reduce your damages by 30%. (Max 3 explosions per projectile)", };
            new Object("nitro_suit", 0) { description = "Creates an explosion dealing 4 times your damage upon taking damage.", };
            new Object("no_scope", 2) { projectileAdd = 1, description = "You also shoot behind you.", };
            new Object("onion", 2) { cooldown = 500, description = "Emit an aura that damages enemies near you periodically for 25% of your damage. (Cooldown 0.5s)", };
            new Object("parting_gift", 1) { dashAdd = 1, description = "Drops a dynamite before each dash, it explodes after 2 seconds. Gain 1 dash", };
            new Object("pong", 3) { bounceAdd = 2, attackSpeedMultiplicator = 0.7, description = "Your projectiles now bounce off the edges of the screen (Max 2 times). But reduce your attack speed by 30%.", };
            new Object("proud_magnet", 0) { description = "Attract all collectibles around you when you eliminate a deputy.", };
            new Object("reckless_rush", 2) { attackBaseAdd = 100, description = "Each time you dash, take 1 damage but permanently gain +10 default damage. (Max number of gains 10)", };
            new Object("sherifken", 3) { attackSpeedMultiplicator = 0.6, pierceAdd = 5, description = "Your shots come back to you and gain 5 piercing. Permanently reduces your attack speed by 40%", };
            new Object("shield", 2) { cooldown = 60000, description = "Gain an armor that protects you from 1 hit. (Cooldown 60s)", };
            new Object("shockwave", 0) { dashAdd = 1, description = "Deal area damage each time you dash. Also gain 1 dash", };
            new Object("shoot_it_thrice", 3) { projectileAdd = 2, attackSpeedMultiplicator = 0.6, };
            new Object("silver_loaded_die", 2) { description = "Gain 3 rerolls for object choices.", };
            new Object("soothing_dash", 1) { dashAdd = 1, description = "Heal for 1 before you dash.Gain 1 dash.+30 sec base dash cooldown.", };
            new Object("spread_shot", 3);
            new Object("spring_bullet", 1) { bounceAdd = 2, description = "Your projectiles bounce 2 more time. Projectiles that bounce now do so in the opposite direction.", };
            new Object("steam_boots", 2) { dashAdd = 1, description = "You reload all your dash at once. Dashing uses all your slots in succession. Gain 1 dash.", };
            new Object("steamtech_turret", 2);
            new Object("surprise_attack", 1) { description = "Whenever you dash gain +100% crit chance for 1.5 seconds.", };
            new Object("target_acquired", 0) { description = "Always target deputies, gradually increases your damage and attack speed until it dies or is out of screen. (Max bonus: x1.5 after 20 sec)", };
            new Object("taunt", 2);
            new Object("tequila", 1) { attackSpeedMultiplicator = 1.3, description = "Permanently raises your attack speed by 30% but half of your shot are less precise.", };
            new Object("tesla_field", 1) { description = "Slow ALL projectiles around you.", };
            new Object("thunder_dance", 1) { description = "Every time you move 2 meters, lightning strike a random enemy dealing small area damage.", };
            new Object("true_survivor", 1) { attackSpeedMultiplicator = 1.3, description = "Permanently gain 10% attack speed every 120s but reset the countdown each time you take damage (Max +30% attack speed)", };
            new Object("valiant_heart", 0) { description = "Heals for 1 each time you gain a level.", };
            new Object("warning_shot", 0) { description = "Your projectiles slow down any enemies they touch by 70% for 1 seconds.", };

            foreach (Object currObject in objectList)
            {
                jsonHandler.WriteObject(currObject);
                if (currObject.hasEffect && !possibleObjects.Contains(currObject))
                    possibleObjects.Add(currObject);
            }
            possibleObjects = possibleObjects.OrderBy(x => x.rarity).ThenBy(x => x.name).ToList();
        }
        public static void CreateCurrentExistingCharacters()
        {
            JsonHandler jsonHandler = new JsonHandler();
            string path = Path.Combine(jsonHandler.directorypath, jsonHandler.directoryname, jsonHandler.libdirectoryname, jsonHandler.characterdirectoryname);
            DirectoryInfo directoryInfo = new DirectoryInfo(path);
            try
            {
                foreach (FileInfo fileInfo in directoryInfo.GetFiles("*.json"))
                {
                    Character currchar = jsonHandler.ReadCharacter(fileInfo.FullName);
                    currchar.AddToCharacterList();
                }
            }
            catch { }

            new Character("ranger") { id = 0, attack = 40, attackSpeed = 1.5, critChance = 0.05, critDamage = 2, hp = 3, pierce = 0, bounce = 0, cooldown = 1, area = 1, resurrect = 0, projectileCount = 1, moveSpeed = 2.7, dash = 1, difficulty = 1, startObject = objectList.Find(x => x.name == "bow_gun") };
            new Character("nigel") { id = 1, description = "Start with -20 Damage.\nStart with -0.5 attack per second.", attack = 20, attackSpeed = 1, critChance = 0.05, critDamage = 2, hp = 3, pierce = 0, bounce = 0, cooldown = 1, area = 1.1, resurrect = 0, projectileCount = 2, moveSpeed = 2.7, dash = 1, difficulty = 2, startObject = objectList.Find(x => x.name == "sherifken") };
            new Character("ollin") { id = 2,description = "Start with -20% cooldowns reduction.", attack = 40, attackSpeed = 0.75, critChance = 0.05, critDamage = 2, hp = 3, pierce = 0, bounce = 0, cooldown = 0.8, area = 1, resurrect = 0, projectileCount = 1, moveSpeed = 2.7, dash = 1, difficulty = 3, startObject = objectList.Find(x => x.name == "mezcals_mighty_staff") };
            new Character("natoko") { id = 3,description = "Start with +0.3 move speed.", attack = 40, attackSpeed = 1.5, critChance = 0.05, critDamage = 2, hp = 3, pierce = 0, bounce = 0, cooldown = 1, area = 1, resurrect = 0, projectileCount = 1, moveSpeed = 3, dash = 1, difficulty = 3, startObject = objectList.Find(x => x.name == "dark_revenge") };
            new Character("roger") { id = 4,description = "Start with -2 max health.\nStart with -0.2 attack per second.", attack = 40, attackSpeed = 1.3, critChance = 0.05, critDamage = 2, hp = 1, pierce = 0, bounce = 0, cooldown = 1, area = 1, resurrect = 0, projectileCount = 1 , moveSpeed = 2.7, dash = 1, difficulty = 1, startObject = objectList.Find(x => x.name == "golden_loaded_die") };

            foreach (Character currchar in characterList)
            {
                jsonHandler.WriteCharacter(currchar);
            }
            characterList = characterList.OrderBy(x => x.id).ThenBy(x => x.name).ToList();
        }
        public static void CreateCurrentExistingBlackmarketChoices()
        {
            blackmarketContents.Add(new BlackmarketContentView { typeText = "RIDGED BARREL", description = "Start your game with 1 common\r\ndamage upgrade.", upgradeType = UpgradeType.damage, amountAdd = 1, maxAmount = 1, cost = 20 });
            blackmarketContents.Add(new BlackmarketContentView { typeText = "MODIFIED TRIGGER", description = "Start your game with 1 common\r\nattack speed upgrade.", upgradeType = UpgradeType.attackSpeed, amountAdd = 1, maxAmount = 1, cost = 20 });
            blackmarketContents.Add(new BlackmarketContentView { typeText = "SPRING LOADED SOLES", description = "Start your game with 1 more dash.", upgradeType = UpgradeType.dash, amountAdd = 1, maxAmount = 1, cost = 30 });
            blackmarketContents.Add(new BlackmarketContentView { typeText = "METAL PLATE", description = "Start your game with 1 more health.", upgradeType = UpgradeType.hp, amountAdd = 1, maxAmount = 1, cost = 30 });
            blackmarketContents.Add(new BlackmarketContentView { typeText = "AIR BOOTS", description = "You move 2% faster.", upgradeType = UpgradeType.moveSpeed, amountAdd = 2, maxAmount = 5, cost = 1, isPercent = true });
            blackmarketContents.Add(new BlackmarketContentView { typeText = "COUNTERFEIT WRISTWATCH", description = "Start with 2% cooldown reduction.", upgradeType = UpgradeType.cooldown, amountAdd = 2, maxAmount = 5, cost = 1, isPercent = true });
            blackmarketContents.Add(new BlackmarketContentView { typeText = "AMPLIFIER", description = "Start with 2% bigger area effect.", upgradeType = UpgradeType.area, amountAdd = 2, maxAmount = 5, cost = 1, isPercent = true });
            blackmarketContents.Add(new BlackmarketContentView { typeText = "BROKEN MAGNET", description = "Attract your collectibles from 2%\r\nfurther away.", upgradeType = UpgradeType.pickupRange, amountAdd = 2, maxAmount = 5, cost = 1, isPercent = true });
            blackmarketContents.Add(new BlackmarketContentView { typeText = "LUCKY STAR", description = "Raise the chance of generating an\r\nadditionl choice in chest by 10%.", amountAdd = 10, maxAmount = 5, cost = 5, isPercent = true });
            blackmarketContents.Add(new BlackmarketContentView { typeText = "FOUR LEAF CLOVER", description = "Raise the chance of generating an\r\nadditionl choice in for your upgrades\r\nby 10%.", amountAdd = 10, maxAmount = 5, cost = 5, isPercent = true });
            blackmarketContents.Add(new BlackmarketContentView { typeText = "LOADED DIE", description = "Gain 1 reroll for your upgrades per\r\ngame.", amountAdd = 1, maxAmount = 2, cost = 10 });
            blackmarketContents.Add(new BlackmarketContentView { typeText = "FATED DIE", description = "Gain 1 reroll in your chests per\r\ngame.", amountAdd = 1, maxAmount = 2, cost = 10 });
        }

        public static void ResetObjects()
        {
            objectList.Clear();
            JsonHandler jsonHandler = new JsonHandler();
            string path = Path.Combine(jsonHandler.directorypath, jsonHandler.directoryname, jsonHandler.libdirectoryname, jsonHandler.objectdirectoryname);
            DirectoryInfo directoryInfo = new DirectoryInfo(path);
            directoryInfo.Delete(true);
            possibleObjects.Clear();
            CreateCurrentExistingObjects();
            RecalculatePossibleObjects();
        }

        public static void ResetCharacters()
        {
            characterList.Clear();
            JsonHandler jsonHandler = new JsonHandler();
            string path = Path.Combine(jsonHandler.directorypath, jsonHandler.directoryname, jsonHandler.libdirectoryname, jsonHandler.characterdirectoryname);
            DirectoryInfo directoryInfo = new DirectoryInfo(path);
            directoryInfo.Delete(true);
            CreateCurrentExistingCharacters();
        }

        public static void RecalculatePossibleObjects()
        {
            possibleObjects.Clear();
            foreach (Object currObject in objectList)
            {
                if (currObject.hasEffect && !player.objects.Contains(currObject))
                    possibleObjects.Add(currObject);
            }
            if (player != null && player.character != null && player.character.startObject != null)
                possibleObjects.RemoveAll(x => x.name == player.character.startObject.name);
            possibleObjects = possibleObjects.OrderBy(x => x.rarity).ThenBy(x => x.name).ToList();

            CreateDefaultObjects();
        }
    }
}
