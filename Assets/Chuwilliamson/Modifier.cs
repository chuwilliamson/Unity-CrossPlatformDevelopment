﻿namespace Chuwilliamson
{
    [System.Serializable]
    public class Modifier
    {
        public ModType ModType { get; set; }
        public float Value { get; set; }
        public string Target { get; set; }
    }
}