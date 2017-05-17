using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
#if UNITY_EDITOR
using UnityEditor;
#endif

[CreateAssetMenu]
public class GameState : ScriptableSingleton<GameState>
{
    [System.Serializable]
    public class PlayerInfo
    {
        public PlayerInfo(Stats pstats)
        {
            statList = new List<Stat>();
            statList.AddRange(pstats.Items.Values);
#if UNITY_EDITOR
            Stats = AssetDatabase.FindAssets("t:Stats")
                .Select(AssetDatabase.GUIDToAssetPath)
                .Select(AssetDatabase.LoadAssetAtPath<Stats>)
                .FirstOrDefault(stat => stat == pstats);
#endif
        }

        public Stats Stats = null;
        public string Name = string.Empty;
        public List<Stat> statList = null;
    }

    public PlayerInfo _player = null;

    public PlayerInfo Player
    {
        get { return _player; }
        set { _player = value; }
    }
}

