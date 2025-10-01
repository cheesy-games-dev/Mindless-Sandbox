#if PHOTON_UNITY_NETWORKING
using System.Linq;
using System.Threading;
using ExitGames.Client.Photon.StructWrapping;
using Photon.Pun;
using UnityEditor;
using UnityEngine;

public class PhotonViewEditorWindow : EditorWindow
{
    [MenuItem("MindlessSDK/PhotonConverter")]
    public static void OpenWindow() {
        PhotonViewEditorWindow window = CreateWindow<PhotonViewEditorWindow>();
        window.titleContent = new("Photon Converter");
    }
    Thread ConversionThread;
    public void OnGUI()
    {
        if (GUILayout.Button("Photon Converting"))
        {
            ConversionThread = new(new ThreadStart(RemoveViewThread));
            ConversionThread.Start();
            RemoveViewThread();
        }
    }

    private void RemoveViewThread()
    {
        var prefabs = Resources.FindObjectsOfTypeAll<GameObject>();
        foreach (var prefab in prefabs)
        {
            foreach (var view in prefab.GetComponentsInChildren<PhotonTransformView>().ToList())
            {
                DestroyImmediate(view);
            }
            foreach (var view in prefab.GetComponentsInChildren<PhotonRigidbodyView>().ToList())
            {
                DestroyImmediate(view);
            }
            DestroyImmediate(prefab.GetPhotonView());
            EditorUtility.SetDirty(prefab);
            AssetDatabase.SaveAssetIfDirty(prefab);
        }
        ConversionThread.Abort();
    }
}
#endif