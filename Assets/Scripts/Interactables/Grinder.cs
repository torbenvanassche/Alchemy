using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grinder : Crafter
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
