using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using Unity.VisualScripting;
using UnityEngine;

public abstract class RecipeHandler : SerializedMonoBehaviour
{
    [SerializeField] protected RecipeBook _book = null;
    [ReadOnly, SerializeField] protected List<Interactable> validCreations = new();
    [ReadOnly, SerializeField] protected List<Interactable> currentCreation = new List<Interactable>();

    [ShowInInspector] private Interactable.RecipeState state = Interactable.RecipeState.Incomplete;
    private Outline _outline;
    
    protected Interactable.RecipeState State
    {
        get => state;
        private set
        {
            state = value;
            OnStateChange();
        }
    }
    
    public void ResetHandler()
    {
        validCreations = _book.GetRecipes();
        currentCreation.Clear();

        State = Interactable.RecipeState.Incomplete;
    }

    protected virtual void OnStateChange()
    {
        
    }

    private void Awake()
    {
        state = Interactable.RecipeState.Incomplete;
        
        _outline = this.AddComponent<Outline>();
        if(_outline) _outline.enabled = false;
        
        ResetHandler();
        
        Initialize();
    }

    protected abstract void Initialize();

    public bool HasRecipes(Interactable interactable)
    {
        return _book.GetRecipes(new List<Interactable> { interactable }).Count > 0;
    }
    
    protected void AddItem(Interactable ingredient)
    {
        if (currentCreation.Contains(ingredient))
        {
            return;
        }

        currentCreation.Add(ingredient);
        validCreations = _book.GetRecipes(currentCreation);
        GameManager.Instance.IngredientReference = null;

        State = validCreations.Count == 0 ? Interactable.RecipeState.Invalid : Interactable.RecipeState.Valid;
        
        if (validCreations.Count > 0 && validCreations[0].productStorage == ingredient)
        {
            Create();
        }
    }
    
    protected void Create()
    {
        var recipes = validCreations.Where(x => x.GetRecipeState(currentCreation) == Interactable.RecipeState.Valid);
        foreach (var interactable in recipes)
        {
            _book.FinishRecipe(interactable);
        }
        ResetHandler();
    }
    
    public void OnMouseEnter()
    {
        HoverController.Instance.Draw(name);
        HoverController.Instance.UpdatePosition(transform.position);

        _outline.OutlineColor = GameManager.Instance.HoveredOutlineColor;
        
        if(_outline) _outline.enabled = true;
    }

    public void OnMouseExit()
    {
        if(_outline) _outline.enabled = false;
        HoverController.Instance.Remove();
    }

    protected abstract void OnMouseUpAsButton();
}
