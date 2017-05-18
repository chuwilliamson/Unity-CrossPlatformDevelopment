using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZambinoBehaviour : CharacterBehaviour
{
    public GameObject PlayerGameObject;
    public Rigidbody2D rb;
    private void Start()
    {
        PlayerGameObject = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
    }
    [InspectorReadOnly]
    float distance;

    
    [Range(0, 10)]
    public float range;
    private void Update()
    {
        distance = Vector3.Distance(transform.position, PlayerGameObject.transform.position);
        if (Vector3.Distance(transform.position, PlayerGameObject.transform.position) < range)
        {
            bool behind = Vector3.Dot(transform.right, PlayerGameObject.transform.right) >= .9;
            if (behind)
                rb.AddForce(transform.right * 1f, ForceMode2D.Impulse);
            else
                rb.AddForce(transform.right * 1f, ForceMode2D.Impulse);
        }
            
    }
}
