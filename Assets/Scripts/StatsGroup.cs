using Worlds.Monsters;

namespace Worlds
{
    [System.Serializable]
    public class StatsSet
    {
        public int Hp;
        public int Atk;
        public int Def;
        public int SpeAtk;
        public int SpeDef;
        public int Speed;
        public StatsSet() { }

    }
    [System.Serializable]
    public class StatsGroup : StatsSet
    {
        public AmountDel LevelDel;

        public delegate void AddExp(Monster amount, float expRate);

        public AddExp OnAddExp;
        public Element Element1 { get; set; }
        public Element Element2 { get; set; }
    }
    public class Monster { }
}