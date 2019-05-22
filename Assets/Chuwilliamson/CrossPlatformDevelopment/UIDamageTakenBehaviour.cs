using UnityEngine;
using UnityEngine.UI;

namespace Chuwilliamson.CrossPlatformDevelopment
{
    public class UIDamageTakenBehaviour : MonoBehaviour
    {
        public int startamount = 10;

        //uiscreens how to do it
        // Use this for initialization
        private void Start()
        {
            GetComponent<Text>().text = startamount.ToString();
            GameSingleton.DamageTakenEvent.AddListener(ChangeText);
        }

        public void ChangeText(int value)
        {
            startamount -= value;
            GetComponent<Text>().text = startamount.ToString();
        }
    }
}