using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class UIGridBehaviour : MonoBehaviour
{
    public int Capacity = 16;
    public List<Transform> children = new List<Transform>();
    public List<GameObject> ui_items = new List<GameObject>();


    private GameObject firstAvailable;
    private int itemIndex = 1;
    private int numItems = 0;


    public GameObject FirstAvailable
    {
        get
        {
            firstAvailable = children.First(c => c.childCount <= 0 && c.parent == transform).gameObject;
            return firstAvailable;
        }
    }

    private void Awake()
    {
        GetComponentsInChildren(true, children);
    }

    public void SetItems(BackPack backPack)
    {
        ui_items.ForEach(Destroy);
        numItems = 0;
        itemIndex = 1;
        backPack.Items.ForEach(SetItem);
    }

    public void SetItem(Item item)
    {
        var newtotal = numItems + 1;
        if (newtotal > Capacity)
            return;
        var itemgo = new GameObject("Item", typeof(Image), typeof(Selectable));
        ui_items.Add(itemgo);
        itemgo.transform.SetParent(children[itemIndex]);
        itemgo.GetComponent<RectTransform>().Stretch();
        itemgo.GetComponent<Image>().sprite = item.ItemSprite;
        numItems++;
        itemIndex++;
    }
}