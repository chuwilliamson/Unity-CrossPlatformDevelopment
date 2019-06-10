using Assessment;
using Chuwilliamson.GameEventSystem;
using UnityEngine;
using UnityEngine.UI;

namespace Chuwilliamson.CrossPlatformDevelopment
{
    public class UIInventoryBehaviour : MonoBehaviour, IListener
    {
        [SerializeField] private ItemList itemListRef;

        [SerializeField] public GameObject uiItemPrefab;

        [SerializeField] private Transform uiItemPrefabContainerTransform;

        [SerializeField] private GameEvent uiItemRemoved;

        public void Subscribe()
        {
            itemListRef.onInventoryChanged.RegisterListener(this);
        }

        public void UnSubscribe()
        {
            itemListRef.onInventoryChanged.UnRegisterListener(this);
        }

        public void OnEventRaised(params object[] args)
        {
            var sender = args[0] as ItemList;
            if (sender == null) return;
            OnInventoryChanged(sender);
        }

        // Use this for initialization
        private void Start()
        {
            OnInventoryChanged(itemListRef);
        }

        private void OnEnable()
        {
            Subscribe();
        }

        private void OnDisable()
        {
            UnSubscribe();
        }

        public void OnInventoryChanged(ItemList inventory)
        {
            for (var i = 0; i < uiItemPrefabContainerTransform.childCount; i++)
                Destroy(uiItemPrefabContainerTransform.GetChild(i).gameObject);
            foreach (var item in inventory)
            {
                var child = Instantiate(uiItemPrefab, uiItemPrefabContainerTransform);
                var img = child.GetComponent<Image>();
                var text = child.GetComponentInChildren<Text>();
                text.text = item.GetInstanceID().ToString();
                img.sprite = item.Value.itemImage;
                var button = child.GetComponent<Button>();
                button.onClick.AddListener(() => { uiItemRemoved.Raise(item); });
            }
        }
    }
}