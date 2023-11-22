using Sirenix.OdinInspector;

public class ItemReference : Hoverable
{
    [OnValueChanged("@displayName = interactable.name")] public Item interactable;

    public void OnMouseUpAsButton()
    {
        GameManager.Instance.IngredientReference = this;
        
        GameManager.Instance.SelectedOutline.Apply(_outline);
        if(_outline) _outline.enabled = true;
    }

    public void OnDeselect()
    {
        if(_outline) _outline.enabled = false;
        
        GameManager.Instance.HoveredOutline.Apply(_outline);
    }

    public void OnCreate()
    {        
        _isUnlocked = true;
        gameObject.SetActive(_isUnlocked);

        GameManager.Instance.IngredientReference = null;
    }
}
