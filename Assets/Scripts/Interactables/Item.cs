using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(menuName = "Alkemik/Interactable", fileName = "interactable")]
public class Item : SerializedScriptableObject
{
    public Color onValidColor = Color.white;
    public string description;
    
    [SerializeField] private Dictionary<Item, int> ingredients = new Dictionary<Item, int>();

    private bool CanAddItem(Dictionary<Item, int> recipeState, Item item)
    {
        return ingredients.ContainsKey(item) && recipeState[item] < ingredients[item];
    }

    public bool IsValid(Dictionary<Item, int> recipeState)
    {
        return !recipeState.Keys.Any(x => !ingredients.ContainsKey(x) || !CanAddItem(recipeState, x));
    }
}
