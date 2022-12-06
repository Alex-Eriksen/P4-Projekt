using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

namespace FunkyCode
	{
	[CanEditMultipleObjects]
	[CustomEditor(typeof(LightOcclusion2D))]
	public class LightOcclusion2DEditor : Editor {

		override public void OnInspectorGUI()
        {
            LightOcclusion2D script = target as LightOcclusion2D;

            DrawSortingLayerInspector(script);

            script.shape.shadowType = (LightOcclusion2D.ShadowType)EditorGUILayout.EnumPopup("Shadow Type", script.shape.shadowType);

            script.occlusionType = (LightOcclusion2D.OcclusionType)EditorGUILayout.EnumPopup("Occlusion Type", script.occlusionType);

            script.occlusionSize = EditorGUILayout.FloatField("Size", script.occlusionSize);

            if (GUILayout.Button("Update"))
            {
                script.Initialize();
            }

            if (GUI.changed)
            {
                script.Initialize();

                if (EditorApplication.isPlaying == false)
                {
                    EditorUtility.SetDirty(target);
                    EditorSceneManager.MarkSceneDirty(UnityEngine.SceneManagement.SceneManager.GetActiveScene());
                }
            }
        }

        private void DrawSortingLayerInspector(LightOcclusion2D script)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUI.BeginChangeCheck();
            int newId = DrawSortingLayersPopup(script.meshRenderer.sortingLayerID);
            if (EditorGUI.EndChangeCheck())
            {
                script.sortingLayerID = newId;
            }
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            EditorGUI.BeginChangeCheck();
            int order = EditorGUILayout.IntField("Sorting Order", script.meshRenderer.sortingOrder);
            if (EditorGUI.EndChangeCheck())
            {
                script.sortingOrder = order;
            }
            EditorGUILayout.EndHorizontal();
        }

        private int DrawSortingLayersPopup(int layerID)
        {
            var layers = SortingLayer.layers;
            string[] names = new string[layers.Length];
            for (int i = 0; i < layers.Length; i++)
            {
                names[i] = layers[i].name;
            }

            if (!SortingLayer.IsValid(layerID))
            {
                layerID = layers[0].id;
            }

            var layerValue = SortingLayer.GetLayerValueFromID(layerID);
            var newLayerValue = EditorGUILayout.Popup("Sorting Layer", layerValue, names);
            return layers[newLayerValue].id;
        }
    }
}