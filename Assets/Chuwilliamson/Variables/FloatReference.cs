// ----------------------------------------------------------------------------
// Unite 2017 - Game Architecture with Scriptable Objects
// 
// Author: Ryan Hipple
// Date:   10/04/17
// ----------------------------------------------------------------------------

using System;

namespace Chuwilliamson.Variables
{
    [Serializable]
    public class FloatReference
    {
        public float constantValue;
        public bool useConstant = true;
        public FloatVariable variable;

        public FloatReference()
        {
        }

        public FloatReference(float value)
        {
            useConstant = true;
            constantValue = value;
        }

        public float Value
        {
            get { return useConstant ? constantValue : variable.value; }
        }

        public static implicit operator float(FloatReference reference)
        {
            return reference.Value;
        }
    }
}