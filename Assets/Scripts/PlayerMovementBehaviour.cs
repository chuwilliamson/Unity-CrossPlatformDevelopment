using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Variables;

public class PlayerMovementBehaviour : MonoBehaviour
{
    [Header("Variables")]
    public FloatVariable SpeedVar;
    public Vector3Variable MouseToWorld;

    [Header("Transforms")]
    public Transform HelperBall_Transform;
    public Transform Base_Transform;
    public Transform Turret_Transform;
    public Transform Barrel_Transform;
    public Transform Barrel_Tip;

    [Header("Camera")]
    public Camera cameraRef;

    [Header("Prefabs")]
    public GameObject bulletPrefab;

    public float timer = 5;
    public float cooldownTime = 1;
    public bool shotFired = false;
    [Header("Gameplay")]
    public float GUNPOWER = 15;

    private void Update()
    {
        transform.position += PlayerInput.Move * SpeedVar.Value;
        MouseToWorld.Value = cameraRef.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -cameraRef.transform.position.z));
        Turret_Transform.LookAt(MouseToWorld.Value);
        HelperBall_Transform.position = MouseToWorld.Value;
        if (Input.GetButtonDown("Fire1"))
        {
            var bullet = Shoot(Barrel_Transform.forward);
        }

        if (shotFired)
        {
            Barrel_Transform.GetComponentInChildren<Material>().color = Color.red;
            timer -= Time.deltaTime;
            if (timer < 0)
            {
                timer = cooldownTime;
                shotFired = false;
                Barrel_Transform.GetComponentInChildren<Material>().color = Color.white;
            }
        }

        
    }
    
    public GameObject Shoot(Vector3 dir)
    {
        if (shotFired)
            return null;
        shotFired = true;
        var go = Instantiate(bulletPrefab, Barrel_Tip.position, Quaternion.identity);
        go.GetComponent<BulletBehaviour>().SetVelocity(dir * GUNPOWER);
        return go;
    }
}
