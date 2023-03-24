using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Furnace : RecipeHandler
{
    protected override void Initialize()
    {
        
    }

    protected override void OnMouseUpAsButton()
    {
        if (GameManager.Instance.IngredientReference &&GameManager.Instance.IngredientReference.interactable)
        {
            AddItem(GameManager.Instance.IngredientReference.interactable);
            Create();
        }
    }
}
