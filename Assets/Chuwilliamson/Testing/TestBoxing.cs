using System.IO;
using Chuwilliamson.Variables;
using UnityEngine;

namespace Chuwilliamson.Testing
{
    public class TestBoxing : MonoBehaviour
    {
        [SerializeField] private Vector3 v;

        [SerializeField] private Vector3Variable variable;

        // Use this for initialization
        private void OnEnable()
        {
            Load();
        }

        private void Load()
        {
            var filename = "test";
            var path = Path.Combine(Application.streamingAssetsPath, filename + ".json");
            var tmp = ScriptableObject.CreateInstance<Vector3Variable>();
            if (File.Exists(path))
            {
                var json = File.ReadAllText(path);
                JsonUtility.FromJsonOverwrite(json, tmp);
            }

            variable = tmp.Value == Vector3.zero ? variable : tmp;
        }

        private void Save()
        {
            var filename = "test";
            var path = Path.Combine(Application.streamingAssetsPath, filename + ".json");
            if (!File.Exists(path)) File.WriteAllText(path, JsonUtility.ToJson(variable));
        }

        private void OnDisable()
        {
            Save();
        }

        private void Start()
        {
            object o = new Vector3(1, 1, 1);
            v = (Vector3) o;
        }

        // Update is called once per frame
        private void Update()
        {
        }
    }
}