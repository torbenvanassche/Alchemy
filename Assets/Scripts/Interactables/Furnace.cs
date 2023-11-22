using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Furnace : Crafter
{
    protected override void OnMouseUpAsButton()
    {
        if (GameManager.Instance.IngredientReference &&GameManager.Instance.IngredientReference.interactable)
        {
            AddToCurrentRecipe(GameManager.Instance.IngredientReference.interactable);
            Create();
        }
    }
}
