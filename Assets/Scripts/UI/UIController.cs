using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

/// <summary>
///     Controller class for UI
///     Interacts with the UIView and Rest of Game
/// </summary>
public class UIController : MonoBehaviour
{
    [SerializeField]
    private BackPackBehaviour backPackBehaviour;

    private bool InventoryUp;

    private Vector3 oldInventoryPos;

    [HideInInspector]
    public OnCancel onCancel;

    [HideInInspector]
    public OnStart onStart;

    public OnStartButton onStartButton;

    [HideInInspector]
    public OnSubmit onSubmit;
    [SerializeField]
    private Dropdown itemDropdownList;
    [SerializeField]
    private Dropdown savesDropdownList;

    [SerializeField]
    private UIView View;
    Button save_button, load_button, clear_button;
    List<Button> _buttons;
    List<Dropdown> _dropdowns;
    private void Awake()
    {
        View = FindObjectOfType<UIView>();
    }

    private void Start()
    {
        onStart.Invoke();
        var ui_gridbehaviour = View.InventoryGrid.GetComponent<UIGridBehaviour>();
        backPackBehaviour.onBackPackChange.AddListener(ui_gridbehaviour.SetItems);
        var allitems = Resources.LoadAll<Item>("Items");
        var names = allitems.Select(t => t._itemName).ToList();
        itemDropdownList.ClearOptions();
        itemDropdownList.AddOptions(names);
        itemDropdownList.onValueChanged.AddListener(delegate
        {
            var item_text = itemDropdownList.captionText.text;
            var v = ItemFactory.CreateItem(item_text);
            backPackBehaviour.AddToPack(v);
        });

        SetButtonCallbacks();
        UpdateSavesDropdown();


        _buttons.ForEach(b => b.gameObject.SetActive(false));
        _dropdowns = new List<Dropdown>() { itemDropdownList, savesDropdownList };
        _dropdowns.ForEach(d => d.gameObject.SetActive(false));
        onStartButton.Invoke();

    }

    private void Update()
    {
        if(GetCancel()) onCancel.Invoke();
        if(GetSubmit()) onSubmit.Invoke();
        if(GetStart()) onStartButton.Invoke();
    }


    private void SetButtonCallbacks()
    {
        save_button = GameObject.Find("Button-Save").GetComponent<Button>();
        load_button = GameObject.Find("Button-Load").GetComponent<Button>();
        clear_button = GameObject.Find("Button-Clear").GetComponent<Button>();

        clear_button.onClick.AddListener(delegate { backPackBehaviour.RemoveAll(); });

        save_button.onClick.AddListener(
            delegate
            {
                DataController.Save(backPackBehaviour.backPack_runtime, "BackPack");
                UpdateSavesDropdown();
            });

        load_button.onClick.AddListener(delegate
        {
            backPackBehaviour.Initialize(DataController.Load<BackPack>(savesDropdownList.captionText.text));
        });
        _buttons = new List<Button>() { save_button, load_button, clear_button };
    }

    public void UpdateSavesDropdown()
    {
        var path = Application.dataPath + "/StreamingAssets/";
        var files = System.IO.Directory.GetFiles(path, "*.json").ToList();

        var names = new List<string>();
        foreach(var f in files)
        {
            var fname = f.Substring(path.Length);
            var nfname = fname.Remove(fname.Length - ".json".Length);
            names.Add(nfname);
        }

        savesDropdownList.ClearOptions();
        savesDropdownList.AddOptions(names);
    }

    public void InventoryToggle()
    {
        if(!InventoryUp)
        {
            oldInventoryPos = View.Inventory.transform.localPosition;
            View.Inventory.transform.localPosition = new Vector3(0, 1000f, 0);
            InventoryUp = true;
        }

        else
        {
            View.Inventory.transform.localPosition = oldInventoryPos;
            InventoryUp = false;
        }
    }

    public void SetHealthSlider(int val)
    {
        View.HealthSlider.value = val;
    }

    public static bool GetCancel()
    {
        return Input.GetButtonDown("Cancel");
    }

    public static bool GetSubmit()
    {
        return Input.GetButtonDown("Submit");
    }

    public static bool GetStart()
    {
        return Input.GetButtonDown("Start");
    }

    [Serializable]
    public class OnStart : UnityEvent { }

    [Serializable]
    public class OnCancel : UnityEvent { }

    [Serializable]
    public class OnSubmit : UnityEvent { }

    [Serializable]
    public class OnStartButton : UnityEvent { }
}