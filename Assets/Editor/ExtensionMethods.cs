using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Debug = System.Diagnostics.Debug;

namespace Chuwilliamson.ScriptableObjects
{
    public static class ExtensionMethods
    {

        public static bool DrawDictionary(SerializedObject so, string propertyName)
        {
            if (propertyName == null)
                return false;
            var field = so.targetObject.GetType().GetField(propertyName);

            if (field != null)
            {
                var fieldName = field.Name;
                var fieldValue = field.GetValue(so.targetObject) as Dictionary<string, string>;
                var gcName = new GUIContent(fieldName);
                EditorGUILayout.LabelField(gcName, GUILayout.Width(100));
                Debug.Assert(fieldValue != null, nameof(fieldValue) + " != null");
                foreach (var kvp in fieldValue)
                {
                    EditorGUI.indentLevel++;
                    var gcKey = new GUIContent("Key: " + kvp.Key);
                    var gcValue = new GUIContent("Value: " + kvp.Value);
                    EditorGUILayout.BeginHorizontal();

                    EditorGUILayout.LabelField(gcKey, GUILayout.Width(100));
                    EditorGUILayout.LabelField(gcValue);
                    EditorGUILayout.EndHorizontal();
                    EditorGUI.indentLevel--;
                }
            }
            else
            {
                EditorGUILayout.LabelField("Field is null");
            }

            return true;
        }
        public static Rect MoveDown(this Rect rect, int yoffset)
        {
            rect.y += yoffset;
            return rect;
        }

        public static class uGUITools
        {
            [MenuItem("GameObject/Utilities/RemoveAllComponents ", false, -1)]
            private static void RemoveAllComponents()
            {
                foreach (var t in Selection.transforms)
                {
                    var components = t.GetComponentsInChildren<MonoBehaviour>();
                    foreach(var c in components)
                        Object.DestroyImmediate(c);

                }
            }


            [MenuItem("GameObject/uGUI/Anchors to Corners ", false, -1)]
            private static void AnchorsToCorners()
            {
                foreach (var transform in Selection.transforms)
                {
                    var t = transform as RectTransform;
                    var pt = Selection.activeTransform.parent as RectTransform;

                    if (t == null || pt == null) return;

                    var rect = pt.rect;
                    var newAnchorsMin = new Vector2(t.anchorMin.x + t.offsetMin.x / rect.width,
                        t.anchorMin.y + t.offsetMin.y / rect.height);
                    var newAnchorsMax = new Vector2(t.anchorMax.x + t.offsetMax.x / rect.width,
                        t.anchorMax.y + t.offsetMax.y / rect.height);

                    t.anchorMin = newAnchorsMin;
                    t.anchorMax = newAnchorsMax;
                    t.offsetMin = t.offsetMax = new Vector2(0, 0);
                }
            }

            [MenuItem("GameObject/uGUI/Corners to Anchors", false, -1)]
            private static void CornersToAnchors()
            {
                foreach (var transform in Selection.transforms)
                {
                    var t = transform as RectTransform;

                    if (t == null) return;

                    t.offsetMin = t.offsetMax = new Vector2(0, 0);
                }
            }

            [MenuItem("GameObject/uGUI/Mirror Horizontally Around Anchors", false, -1)]
            private static void MirrorHorizontallyAnchors()
            {
                MirrorHorizontally(false);
            }

            [MenuItem("GameObject/uGUI/Mirror Horizontally Around Parent Center ", false, -1)]
            private static void MirrorHorizontallyParent()
            {
                MirrorHorizontally(true);
            }

            private static void MirrorHorizontally(bool mirrorAnchors)
            {
                foreach (var transform in Selection.transforms)
                {
                    var t = transform as RectTransform;
                    var pt = Selection.activeTransform.parent as RectTransform;

                    if (t == null || pt == null) return;

                    if (mirrorAnchors)
                    {
                        var oldAnchorMin = t.anchorMin;
                        t.anchorMin = new Vector2(1 - t.anchorMax.x, t.anchorMin.y);
                        t.anchorMax = new Vector2(1 - oldAnchorMin.x, t.anchorMax.y);
                    }

                    var oldOffsetMin = t.offsetMin;
                    t.offsetMin = new Vector2(-t.offsetMax.x, t.offsetMin.y);
                    t.offsetMax = new Vector2(-oldOffsetMin.x, t.offsetMax.y);

                    t.localScale = new Vector3(-t.localScale.x, t.localScale.y, t.localScale.z);
                }
            }

            [MenuItem("GameObject/uGUI/Mirror Vertically Around Anchors ", false, -1)]
            private static void MirrorVerticallyAnchors()
            {
                MirrorVertically(false);
            }

            [MenuItem("GameObject/uGUI/Mirror Vertically Around Parent Center ", false, -1)]
            private static void MirrorVerticallyParent()
            {
                MirrorVertically(true);
            }

            private static void MirrorVertically(bool mirrorAnchors)
            {
                foreach (var transform in Selection.transforms)
                {
                    var t = transform as RectTransform;
                    var pt = Selection.activeTransform.parent as RectTransform;

                    if (t == null || pt == null) return;

                    if (mirrorAnchors)
                    {
                        var anchorMin = t.anchorMin;
                        var oldAnchorMin = anchorMin;
                        t.anchorMin = new Vector2(anchorMin.x, 1 - t.anchorMax.y);
                        t.anchorMax = new Vector2(t.anchorMax.x, 1 - oldAnchorMin.y);
                    }

                    var offsetMin = t.offsetMin;
                    var oldOffsetMin = offsetMin;
                    t.offsetMin = new Vector2(offsetMin.x, -t.offsetMax.y);
                    t.offsetMax = new Vector2(t.offsetMax.x, -oldOffsetMin.y);

                    var localScale = t.localScale;
                    localScale = new Vector3(localScale.x, -localScale.y, localScale.z);
                    t.localScale = localScale;
                }
            }
        }
    }
}