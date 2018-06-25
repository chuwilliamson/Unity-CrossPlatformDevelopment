using System.Runtime.Serialization;
using RPGStats;

namespace Combat
{
    [DataContract]
    //chance to hit attack roll
    public class Unit
    {
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public int ArmorCount { get; set; }

        [DataMember]
        public int Health { get; set; }

        [DataMember]
        public int Level { get; set; }

        /// <summary>
        ///     is a modifier value based on Dexterity
        /// </summary>
        [DataMember]
        public int Initiative { get; set; }

        [DataMember]
        public RPG_Stats RpgStats { get; set; }

        [DataMember]
        public Weapon Weapon { get; set; }

        /// <summary>
        ///     is a modifier value based on Level starting at 1 and increasing by 1 every 5 levels after the first 5
        /// </summary>
        [DataMember]
        public int Proficiency
        {
            get { return Level / 5 + 2; }
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            return GetHashCode() == obj.GetHashCode();
        }

        public override int GetHashCode()
        {
            return Weapon.GetHashCode() + Name.GetHashCode();
        }


        public Tuple<bool, int> Attack(Unit defender)
        {
            return Combat.Instance.RollForAttack(this, defender);
        }

        public void TakeDamage(int amount)
        {
            Health -= amount;
        }
    }
}