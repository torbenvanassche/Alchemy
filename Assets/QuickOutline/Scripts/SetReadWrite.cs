using UnityEditor;
using UnityEngine;

public static class SetReadWrite
{
    [MenuItem("Tools/Alkemika/SetReadWriteIfOutline")]
    private static void SetReadableIfOutlined()
    {
        foreach (var o in Selection.gameObjects)
        {
            if (o.TryGetComponent<MeshFilter>(out var filter) && o.TryGetComponent<Outline>(out var outline))
            {
                var path = AssetDatabase.GetAssetPath(filter.sharedMesh);
                ModelImporter ti = (ModelImporter)AssetImporter.GetAtPath(path);

                if (ti && !ti.isReadable)
                {
                    ti.isReadable = true;
                    EditorUtility.SetDirty(ti);
                    ti.SaveAndReimport();
                }
            }
        }

        AssetDatabase.SaveAssets();
    }
}
