using UnityEditor;

using UnityEngine;

namespace MorningExercises
{



    public class RandomFacialBehaviour : MonoBehaviour
    {
        [ContextMenu("doit")]
        public void RandomFacialExpression()
        {

            var mesh = GetComponent<Mesh>();
            var smr = GetComponent<SkinnedMeshRenderer>();
            for (var i = 0; i < smr.sharedMesh.blendShapeCount; i++)
            {
                var name = smr.sharedMesh.GetBlendShapeName(i);
                int max = 50;
                if (name.Contains("Mouth"))
                    max = 25;
                smr.SetBlendShapeWeight(i, Random.Range(0, 50));
            }
        }
    }

    [CustomEditor(typeof(RandomFacialBehaviour))]
    public class RandomFacialBehaviourEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            var mt = target as RandomFacialBehaviour;

            if (GUILayout.Button("doit"))
                mt.RandomFacialExpression();
        }
    }
}