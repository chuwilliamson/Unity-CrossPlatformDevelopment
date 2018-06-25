using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIView : MonoBehaviour
{
    public GameObject Dimmer;

    public GameObject HealthBar;

    public GameObject Info;
    public GameObject Inventory;

    public GameObject InventoryGrid;

    public GameObject NPC;

    public GameObject PlayerChoices;

    public GameObject Root;

    private List<GameObject> ui_items;
    public Slider HealthSlider;
    private void Awake()
    {
        if (ui_items == null) return;
        if (ui_items.Count > 0) ui_items.ForEach(go => go.SetActive(false));
    }
 
}