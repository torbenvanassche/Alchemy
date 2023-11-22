using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Crafter : Hoverable
{
    public enum RecipeState
    {
        Valid, 
        Invalid, 
        Incomplete
    }
    
    [SerializeField] protected RecipeBook _book = null;
    [ReadOnly, SerializeField] protected List<Item> validRecipes = new();
        
    protected Dictionary<Item, int> _currentRecipe = new();

    [ShowInInspector] private RecipeState state = RecipeState.Incomplete;

    protected RecipeState State
    {
        get => state;
        private set
        {
            state = value;
            OnStateChange();
        }
    }
    
    protected override void Awake()
    {
        base.Awake();
        
        validRecipes = _book.GetRecipes();
        _currentRecipe.Clear();

        State = RecipeState.Incomplete;
        
        //Child awake to make sure everything calls in the right order
        OnAwake();
    }

    protected virtual void OnStateChange()
    {
        
    }

    protected void AddToCurrentRecipe(Item ingredient)
    {
        if (_currentRecipe.ContainsKey(ingredient))
        {
            _currentRecipe[ingredient]++;
        }
        else
        {
            _currentRecipe.Add(ingredient, 1);
        }
        
        validRecipes = _book.GetRecipes(_currentRecipe);
        GameManager.Instance.IngredientReference = null;
    }
    
    protected void Create()
    {
        //TODO
        var recipes = new List<Item>();
        foreach (var interactable in recipes)
        {
            _book.FinishRecipe(interactable);
        }
        OnAwake();
    }

    protected abstract void OnMouseUpAsButton();
}
