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
        public static List<UpgradeView> upgradeViews { get; set; } = new List<UpgradeView>();
        public static List<ObjectView> objectViews { get; set; } = new List<ObjectView>();
        public static List<UpgradeType> wantedUpgrades { get; set; } = new List<UpgradeType>();
        public static List<Object> possibleObjects { get; set; } = new List<Object>();
        public static EmbedView EmbedView { get; set; }
        public static StackGrid UpgradeGrid { get; set; }
        public static Player player { get; set; } = new Player();

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
