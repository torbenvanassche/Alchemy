using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(menuName = "Alkemik/Interactable", fileName = "interactable")]
public class Interactable : SerializedScriptableObject
{
    public enum RecipeState
    {
        Valid, 
        Invalid, 
        Incomplete
    }
    
    public Color onValidColor = Color.white;
    public Interactable productStorage = null;
    
    public string description;
    public List<Interactable> ingredients = new List<Interactable>();

    public RecipeState GetRecipeState(List<Interactable> ingredients)
    {
        //Are all ingredients in cauldron valid for this recipe
        if (ingredients.All(x => this.ingredients.Contains(x)))
        {
            //Do both containers have the same amount of items
            if (ingredients.Count == this.ingredients.Count)
            {
                bool containsAll = false;
                foreach (var ingredient in ingredients)
                {
                    containsAll = this.ingredients.Contains(ingredient);
                    
                    //Potentially needs a fix
                    //if (containsAll == false) break;
                }

                if (containsAll)
                {
                    return RecipeState.Valid;
                }
            }
            else if (ingredients.Count > this.ingredients.Count)
            {
                return RecipeState.Invalid;
            }
            else
            {
                return RecipeState.Incomplete;
            }
        }

        return RecipeState.Invalid;
    }
}
