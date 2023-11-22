using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(menuName = "Alkemik/RecipeBook", fileName = "recipes")]
public class RecipeBook : SerializedScriptableObject
{ 
    [SerializeField] private IEnumerable<Item> _interactables = new List<Item>();
    private IEnumerable<ItemReference> interactableReferences = new List<ItemReference>();

    public List<Item> GetRecipes(Dictionary<Item, int> ingredients = null)
    {
        if (ingredients.Count == 0)
        {
            //Create a copy so we don't remove data from our source container
            return _interactables.ToList();
        }

        var returnValue = new List<Item>();
        foreach (var interactable in _interactables)
        {
            if (interactable.IsValid(ingredients))
            {
                returnValue.Add(interactable);
            }
        }

        return returnValue;
    }

    public void FinishRecipe(Item interactable)
    {
        var validRecipes = interactableReferences.Where(x => x.interactable == interactable);
        foreach (var interactableReference in validRecipes)
        {
            interactableReference.OnCreate();
        }
    }
}
