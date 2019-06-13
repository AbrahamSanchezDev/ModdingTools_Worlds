namespace Worlds.Monsters
{
    public enum Stat
    {
        Hp = 0,
        Energy = 1,
        MaxHp = 2,
        MaxEnergy = 3,
        Attack = 4,
        Defence = 5,
        SpecialAttack = 6,
        SpecialDefence = 7
    }

    public enum BuffStat
    {
        Hp = Stat.Hp,
        Energy = Stat.Energy,
        MaxHp = Stat.MaxHp,
        MaxEnergy = Stat.MaxEnergy,
        Attack = Stat.Attack,
        Defence = Stat.Defence,
        SpecialAttack = Stat.SpecialAttack,
        SpecialDefence = Stat.SpecialDefence,
        Inmunity = 8
    }
}