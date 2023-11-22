using Sirenix.OdinInspector;
using UnityEngine;

[RequireComponent(typeof(Outline))]
public abstract class Hoverable : SerializedMonoBehaviour
{
    protected Outline _outline;

    public string displayName = string.Empty;
    [SerializeField] protected bool _isUnlocked = false;

    private void Reset()
    {
        if (displayName == string.Empty)
        {
            displayName = gameObject.name;
        }
    }

    protected virtual void Awake()
    {
        //Get the outline components
        if (!TryGetComponent(out _outline))
        {
            _outline = gameObject.AddComponent<Outline>();
        }
        _outline.enabled = false;
        
        //Make sure layer masking is set up correctly
        gameObject.layer = LayerMask.NameToLayer("Interactable");
    }
    
    public void OnMouseEnter()
    {
        GameManager.Instance.Tooltip.Draw(this);

        if (GameManager.Instance.IngredientReference != this)
        {
            GameManager.Instance.HoveredOutline.Apply(_outline);
        }
        
        if(_outline) _outline.enabled = true;
    }

    public void OnMouseExit()
    {
        if(_outline) _outline.enabled = false;
        
        GameManager.Instance.Tooltip.Remove();
    }

    protected virtual void OnAwake()
    {
        
    }
}
