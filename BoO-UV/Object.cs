namespace BoO_UV
{
    public sealed class Object
    {
        public string name { get; set; }
        public byte rarity { get; set; }
        public string imagePath { get; set; }
        public string holderPath { get; set; }
        public string rarityPath { get; set; }
        public int attackBaseAdd { get; set; }
        public bool attackMultiplicatorFromHP { get; set; }
        public double attackMultiplicator
        {
            get
            {
                if (attackMultiplicatorFromHP)
                    return _attackMultiplicator * MauiProgram.player.hp + 1;
                else
                    return _attackMultiplicator;
            }
            set
            {
                _attackMultiplicator = value;
            }
        }
        private double _attackMultiplicator;
        public double attackSpeedBaseAdd { get; set; }
        public double attackSpeedMultiplicator { get; set; }
        public double critChanceBaseAdd { get; set; }
        public double critDamageBaseAdd { get; set; }
        public double areaBaseAdd { get; set; }
        public double areaMultiplicator { get; set; }
        public int pierceAdd { get; set; }
        public int bounceAdd { get; set; }
        public int hpAdd { get; set; }
        public bool hasEffect
        {
            get
            {
                return attackBaseAdd != 0 || attackMultiplicator != 0 || attackSpeedBaseAdd != 0 || attackSpeedMultiplicator != 0 || critChanceBaseAdd != 0 || critDamageBaseAdd != 0 || areaBaseAdd != 0 || areaMultiplicator != 0 || hpAdd != 0 || pierceAdd != 0 || bounceAdd != 0;
            }
            set
            {
                _hasEffect = value;
            }
        }
        private bool _hasEffect;

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
        public Object()
        {
            
        }

        public void AddToObjectList()
        {
            if (MauiProgram.objectList.Find(x => x.name == name) != null) return;
            MauiProgram.objectList.Add(this);
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
                    holderPath = "object_holder_legandary.png";
                    rarityPath = "object_rarity_legandary.png";
                    break;
            }
        }
    }
}
