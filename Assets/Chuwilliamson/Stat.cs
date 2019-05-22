using System.Collections.Generic;

namespace Chuwilliamson
{
    public class Stat
    {
        public float BaseValue;
        private Dictionary<int, Modifier> modifiers;

        public float Value
        {
            get
            {
                var tmpvalue = BaseValue;
                foreach (var mod in modifiers.Values)
                    if (mod.ModType == ModType.Add)
                        tmpvalue += mod.Value;
                    else
                        tmpvalue = BaseValue + BaseValue * mod.Value;

                return tmpvalue;
            }
        }
    }
}