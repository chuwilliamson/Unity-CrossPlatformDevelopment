using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ItemTableBehaviour : MonoBehaviour
{
    public LootTable lootTable;
    private GameObject itemPrefab;

    public List<string> items;
    // Use this for initialization
    public void Initialize()
    {
        if(lootTable == null)
        {
            Debug.LogError("LootTable not assigned");
            return;
        }
        itemPrefab = Resources.Load("ItemPrefabResource") as GameObject;
        lootTable = Instantiate(lootTable);
    }

    public void DropLoot()
    {
        var drops = lootTable.GetDrops();
        if(drops == null)
            return;
        foreach (var item in drops)
        {
            items.Add(item.name);
            var itemObject = Instantiate(itemPrefab, transform.position, transform.rotation);
            var itemBehaviour = itemObject.GetComponent<ItemBehaviour>();
            itemBehaviour.item_config = item;
            itemBehaviour.Initialize(item);
         
            itemObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 10f, ForceMode2D.Impulse);
            GetComponent<SpriteRenderer>().enabled = false;
            Destroy(gameObject);
        }

        
        
    }
}