using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName ="GLOBALS/GlobalBlackBoard")]
public class GlobalBlackBoard : ScriptableObject
{
    
    private static GlobalBlackBoard _instance;

    public class GameStartedEvent : UnityEvent
    {
        
    } 

    public static GlobalBlackBoard Instance
    {
        get
        {
            if (!_instance)
                _instance = Resources.FindObjectsOfTypeAll<GlobalBlackBoard>().FirstOrDefault();
            if (!_instance)
                _instance = ScriptableObject.CreateInstance<GlobalBlackBoard>();
            return _instance;

        }    
    }

    [Serializable]
    public class PlayerInfo
    {
        public int Health;
        public int Mana;
    }

    public PlayerInfo ThePlayer;

}
