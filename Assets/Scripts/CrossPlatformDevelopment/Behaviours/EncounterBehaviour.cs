using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class EncounterBehaviour : MonoBehaviour
{
    public Encounter Encounter;

    //use this for initialization
    private void Start()
    {
        Encounter.Initialize(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag != "Player") return;
        Encounter.EncounterStart();
        Encounter.EncounterStart(other.gameObject);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag != "Player") return;
        Encounter.EncounterUnderway();
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag != "Player") return;
        if (!Encounter.Contained) return;
        Encounter.EncounterEnd();
    }
}