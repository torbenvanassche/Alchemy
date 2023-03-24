using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(menuName = "Alkemik/RecipeBook", fileName = "recipes")]
public class RecipeBook : SerializedScriptableObject
{ 
    [SerializeField] private IEnumerable<Interactable> _interactables = new List<Interactable>();
    private IEnumerable<InteractableReference> interactableReferences = new List<InteractableReference>();

    public List<Interactable> GetRecipes(List<Interactable> ingredients = null)
    {
        if (!interactableReferences.Any())
        {
            interactableReferences = FindObjectsOfType<InteractableReference>(true);
        }
        
        if (ingredients == null)
        {
            return _interactables.ToList();
        }
        
        List<Interactable> canCreate = new();
        foreach (var interactable in _interactables)
        {
            if (interactable.GetRecipeState(ingredients) != Interactable.RecipeState.Invalid)
            {
                canCreate.Add(interactable);
            }
        }

        return canCreate;
    }

    public void FinishRecipe(Interactable interactable)
    {
        var validRecipes = interactableReferences.Where(x => x.interactable == interactable);
        foreach (var interactableReference in validRecipes)
        {
            interactableReference.OnCreate();
        }
    }
}
