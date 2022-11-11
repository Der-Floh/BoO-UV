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
        public static List<ObjectView> objectViews { get; set; } = new List<ObjectView>();
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

            CreateCurrentExistingCharacters();

            player = new Player(characterList.Find(x => x.name == "ranger"));

            CreateCurrentExistingBlackmarketChoices();
            CreateCurrentExistingObjects();

            CreateDefaultUpgrades();
            CreateDefaultObjects();
        }

        public static void CreateDefaultUpgrades()
        {
            /*
            for (int i = 0; i < 5; i++)
            {
                Upgrade upgrade = new Upgrade { type = (UpgradeType)i };
                Globals.upgradeViews.Add(new UpgradeView(upgrade));
                UpgradeGrid.Rebuild(Globals.upgradeViews[i]);
            }*/
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
                objectViews.Add(new ObjectView(currobject, EmbedView));
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

            new Object("adrenaline", 1);
            //new Object("bottled_lightning", 0);
            new Object("bow_gun", 3) { attackSpeedMultiplicator = 2, };
            new Object("bronze_loaded_die", 1);
            new Object("dark_revenge", 3);
            new Object("dirty_nigel", 0) { attackMultiplicator = 1.2, };
            new Object("eye_of_the_storm", 2);
            new Object("faith_full", 0);
            new Object("far_sight", 0) { attackMultiplicator = 1.3, attackSpeedMultiplicator = 0.8, };
            new Object("gluttony", 1) { attackMultiplicator = 0.1, attackMultiplicatorFromHP = true, };
            new Object("golden_loaded_die", 3);
            new Object("hawk_eye", 3) { critChanceBaseAdd = 0.6, attackMultiplicator = 0.5, };
            new Object("heroes_landing", 1);
            new Object("horseshoe_magnet", 0);
            new Object("intimidating_bounty", 1);
            new Object("lucky_shot", 2);
            new Object("machine_gun", 3) { attackSpeedMultiplicator = 4, attackMultiplicator = 1.0 / 3.0, };
            //new Object("mega_net", 0);
            new Object("mezcal_defiance", 1);
            new Object("mezcal_mantle", 0);
            new Object("mezcals_mighty_staff", 3);
            new Object("ninja_headband", 0);
            new Object("nitro_crit", 3);
            new Object("nitro_suit", 0);
            new Object("no_scope", 2) { projectileAdd = 1, };
            new Object("onion", 2);
            new Object("parting_gift", 1);
            new Object("pong", 3);
            new Object("proud_magnet", 0);
            new Object("reckless_rush", 2) { attackBaseAdd = 100, };
            new Object("sherifken", 3);
            new Object("shield", 2);
            new Object("shockwave", 0);
            new Object("shoot_it_thrice", 3) { projectileAdd = 2, attackSpeedMultiplicator = 0.8, };
            new Object("silver_loaded_die", 2);
            new Object("soothing_dash", 1);
            new Object("spread_shot", 3);
            new Object("spring_bullet", 1) { bounceAdd = 2, };
            new Object("steam_boots", 2);
            new Object("steamtech_turret", 2);
            new Object("surprise_attack", 1);
            new Object("target_acquired", 0);
            new Object("taunt", 2);
            new Object("tequila", 1) { attackSpeedMultiplicator = 1.3, };
            new Object("tesla_field", 1);
            new Object("thunder_dance", 1);
            new Object("true_survivor", 1) { attackSpeedMultiplicator = 1.3, };
            new Object("valiant_heart", 0);
            new Object("warning_shot", 0);

            foreach (Object currObject in objectList)
            {
                jsonHandler.WriteObject(currObject);
                if (currObject.hasEffect)
                    possibleObjects.Add(currObject);
            }
            possibleObjects = possibleObjects.OrderBy(x => x.rarity).ThenBy(x => x.name).ToList();
        }
        public static void CreateCurrentExistingCharacters()
        {
            new Character("ranger") { attack = 40, attackSpeed = 1.5, critChance = 0.05, critDamage = 2, hp = 3, pierce = 0, bounce = 0, cooldown = 1, area = 1, resurrect = 0, projectileCount = 1 };
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
            CreateCurrentExistingObjects();
        }

        public static void RecalculatePossibleObjects()
        {
            possibleObjects.Clear();
            foreach (Object currObject in objectList)
            {
                if (currObject.hasEffect && !player.objects.Contains(currObject))
                    possibleObjects.Add(currObject);
            }
            possibleObjects = possibleObjects.OrderBy(x => x.rarity).ThenBy(x => x.name).ToList();

            objectViews.Clear();
            CreateDefaultObjects();
        }
    }
}
