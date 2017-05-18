using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieBehaviour : CharacterBehaviour
{
    
    public Modifier ZombieModConfig;
    public float DistanceToTravel;
    public float WalkSpeed;

    private Modifier RUNTIME_MOD;
    private Vector2 InitialPosition;    
    private Vector2 GoalPosition;

    void Awake()
    {
        if (CharacterStats == null)
        {
            Debug.LogWarning("You have not assigned a stats reference object... creating.");
            CharacterStats = Resources.Load<Stats>(@"Stats\PlayerStats");
        }

        GameState.Instance._player = new GameState.PlayerInfo(CharacterStats) { Name = name };
    }    

    void Start()
    {
        InitialPosition = transform.position;
        GoalPosition.x = InitialPosition.x + DistanceToTravel;
        RUNTIME_MOD = Instantiate(ZombieModConfig);
        RUNTIME_MOD.Initialize(null);
    }
    
    void Update()
    {        
        if (Vector2.Distance(GoalPosition, transform.position) <= 1)
        {
            DistanceToTravel = -DistanceToTravel;
            GoalPosition.x = InitialPosition.x + DistanceToTravel;
            transform.right = -transform.right;
            WalkSpeed = -WalkSpeed;
        }        
        GetComponent<Rigidbody2D>().AddForce(Vector2.right * WalkSpeed);
    }

    void Attack(GameObject target)
    {
        target.GetComponent<CharacterBehaviour>().ModifyStat(RUNTIME_MOD.EffectedStat, RUNTIME_MOD.mod);
    }
}
