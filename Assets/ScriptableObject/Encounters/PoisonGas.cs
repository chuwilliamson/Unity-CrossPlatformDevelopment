using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Encounter/PoisonGas")]
public class PoisonGas : Encounter {
    
    public Material encounterMaterial;
    public Modifier modifier;

    private GameObject _player;
    private Modifier RUNTIME_MOD;
    private float _timer = 0;

    public override void Initialize(GameObject obj)
    {
        base.Initialize(obj);

        //Adds a particle system to the Encounter GameObject
        //Edits the rate of emission and the particle lifetimes
        obj.AddComponent<ParticleSystem>();
        var em = obj.GetComponent<ParticleSystem>().emission;
        em.rateOverTime = 200;
        var main = obj.GetComponent<ParticleSystem>().main;
        main.startLifetime = 1;

        obj.GetComponent<ParticleSystemRenderer>().material = encounterMaterial;

        //Finds player for Encounter functions
        _player = FindObjectOfType<PlayerInput>().gameObject;

        RUNTIME_MOD = Instantiate(modifier);
        RUNTIME_MOD.Initialize(null);
    }

    public bool EncounterStartedWithPlayer(GameObject obj)
    {
        if(obj.GetComponent<PlayerBehaviour>())
            obj.GetComponent<PlayerBehaviour>().ModifyStat(RUNTIME_MOD.EffectedStat, RUNTIME_MOD.mod);
        return true;
    }

    public override bool EncounterStart()
    {
        
        _player.GetComponent<PlayerBehaviour>().ModifyStat(RUNTIME_MOD.EffectedStat, RUNTIME_MOD.mod);

        return true;
    }

    public override bool EncounterUnderway()
    {
        if (_timer >= 1.5f)
        {
            _player.GetComponent<PlayerBehaviour>().ModifyStat(RUNTIME_MOD.EffectedStat, RUNTIME_MOD.mod);
            _timer = 0;
        }
        else
        {
            _timer += Time.deltaTime;
        }
        return true;
    }

    public override bool EncounterEnd()
    {
        _timer = 0;

        return true;
    }
}
