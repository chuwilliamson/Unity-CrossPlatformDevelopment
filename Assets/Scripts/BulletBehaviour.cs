using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour {
    public Vector3 Velocity { get; set; }

    public void SetVelocity(Vector3 v)
    {
        Velocity = v;
    }

    private float TTL = 2;
    private void Update()
    {
        if(TTL < 0)
            Destroy(gameObject);
        TTL -= Time.deltaTime;
        transform.position += Velocity * Time.deltaTime;
    }
}
