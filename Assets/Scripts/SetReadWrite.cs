using Sirenix.OdinInspector;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

public class SetReadWrite : SerializedMonoBehaviour
{
    [Button]
    private void MakeReadable()
    {
        #if UNITY_EDITOR
        foreach (var filter in FindObjectsOfType<MeshFilter>())
        {
            //Exception for probuilder assets
            if (filter.sharedMesh.name.Contains("pb_Mesh"))
            {
                continue;
            }
            
            var path = AssetDatabase.GetAssetPath(filter.sharedMesh);
            ModelImporter ti = (ModelImporter)AssetImporter.GetAtPath(path);

            if (!ti.isReadable)
            {
                ti.isReadable = true;
                EditorUtility.SetDirty(ti);
                ti.SaveAndReimport();
            }
        }
        AssetDatabase.SaveAssets();
        #endif
    }
}
