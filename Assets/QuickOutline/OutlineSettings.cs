using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(menuName = "Alkemik/Outline Settings", fileName = "Outline")]
public class OutlineSettings : SerializedScriptableObject
{
    [SerializeField] private Outline.Mode _outlineMode = Outline.Mode.OutlineAll;
    [SerializeField] private Color _outlineColor = Color.white;
    [SerializeField, Range(0, 10)] private float _outlineWidth = 2;

    [Header("Optional"), SerializeField] private bool _precomputeOutline = false;

    public void Apply(Outline outline)
    {
        outline.OutlineMode = _outlineMode;
        outline.OutlineColor = _outlineColor;
        outline.OutlineWidth = _outlineWidth;
        outline.Precompute = _precomputeOutline;

        outline.OnValidate();
    }
}
