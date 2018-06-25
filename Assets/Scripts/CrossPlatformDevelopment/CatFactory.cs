using System.Collections.Generic;
using UnityEngine;

public class CatFactory : PrefabFactory
{
    public float countdown = 2f;
    public float forceFactor = 1f;
    public int maxCats = 5;
    public float torqueFactor = 1f;
    public float ttl = 5f;

    private void Start()
    {
        objectPool = new List<GameObject>();
    }

    protected override void SpawnPrefab()
    {
        var go = Instantiate(prefab, transform);
        go.transform.localPosition = Vector3.zero;
        objectPool.Add(go);
        go.GetComponent<Rigidbody2D>().AddForce(new Vector2(.5f, .5f) * forceFactor, ForceMode2D.Force);
        go.GetComponent<Rigidbody2D>().AddTorque(10f * torqueFactor, ForceMode2D.Impulse);
        var timercomponent = go.AddComponent<DestroyOnTimer>();
        timercomponent.Time = 10f;
        timercomponent.onDestroyed.AddListener(RemoveFromPool);
    }

    private void Update()
    {
        if (objectPool.Count > maxCats)
            return;
        if (countdown <= 0)
        {
            SpawnPrefab();
            countdown = ttl;
        }
        else
        {
            countdown -= Time.deltaTime;
        }
    }


    protected override void AddToPool(GameObject go)
    {
        objectPool.Add(go);
    }

    protected override void RemoveFromPool(GameObject go)
    {
        objectPool.Remove(go);
    }
}