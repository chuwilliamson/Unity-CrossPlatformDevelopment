using System.Runtime.Serialization;

namespace RPGStats
{
    [DataContract]
    [System.Serializable]
    public class Modifier
    {
        [DataMember] public string stat;
        [DataMember] public string type;

        [DataMember] public int value;

        /// <summary>
        ///     Modifier Object
        /// </summary>
        /// <param name="t">type of modifier mult or add</param>
        /// <param name="s">stat to modify</param>
        /// <param name="v">value to apply t to</param>
        public Modifier(string t, string s, int v)
        {
            type = t;
            stat = s;
            value = v;
        }
    }
}