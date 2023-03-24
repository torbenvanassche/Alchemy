using Unity.VisualScripting;
using UnityEngine;

public class InteractableReference : MonoBehaviour
{
    public Interactable interactable;
    private Outline _outline;

    [SerializeField] private bool _isUnlocked = false;

    private void Awake()
    {
        _outline = this.AddComponent<Outline>();
        if(_outline) _outline.enabled = false;
        
        gameObject.SetActive(_isUnlocked);
    }

    public void OnMouseEnter()
    {
        HoverController.Instance.Draw(interactable.name);
        HoverController.Instance.UpdatePosition(transform.position);

        if (GameManager.Instance.IngredientReference != this)
        {
            _outline.OutlineColor = GameManager.Instance.HoveredOutlineColor;
        }
        
        if(_outline) _outline.enabled = true;
    }

    public void OnMouseExit()
    {
        if (GameManager.Instance.IngredientReference != this)
        {
            if(_outline) _outline.enabled = false;
        }
        
        HoverController.Instance.Remove();
    }

    public void OnMouseUpAsButton()
    {
        GameManager.Instance.IngredientReference = this;
        
        _outline.OutlineColor = GameManager.Instance.SelectedOutlineColor;
        if(_outline) _outline.enabled = true;
    }

    public void OnDeselect()
    {
        if(_outline) _outline.enabled = false;
        
        _outline.OutlineColor = GameManager.Instance.HoveredOutlineColor;
    }

    public void OnCreate()
    {
        gameObject.SetActive(true);
        _isUnlocked = true;

        GameManager.Instance.IngredientReference = null;
        gameObject.layer = LayerMask.NameToLayer("Interactable");
    }
}
