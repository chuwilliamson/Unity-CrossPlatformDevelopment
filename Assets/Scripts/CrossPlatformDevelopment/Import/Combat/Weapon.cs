using System;
using System.Runtime.Serialization;

namespace Combat
{
    [DataContract]
    [KnownType(typeof(Dagger))]
    [KnownType(typeof(Club))]
    public abstract class Weapon
    {
        protected Random seed;

        [DataMember]
        public string StatName { get; set; }

        public abstract int Roll();
    }

    [DataContract]
    public class Dagger : Weapon
    {
        public Dagger()
        {
            seed = new Random();
        }

        [DataMember]
        public string Name { get; set; }

        //1d4 damages
        //then add the stat modifier
        //dmg is rolling the weapons dice and adding the attackers stat
        public override int Roll()
        {
            return seed.Next(1, 5);
        }
    }

    [DataContract]
    public class Club : Weapon
    {
        public Club()
        {
            seed = new Random();
        }

        //1d6 damages
        public override int Roll()
        {
            return seed.Next(1, 6);
        }
    }
}