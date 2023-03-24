using System;
using System.Collections.Generic;
using UnityEngine;
using Utilities;

public class GameManager : Singleton<GameManager>
{
    public Cauldron _Cauldron = null;

    private InteractableReference _ingredientReference = null;
    public Color SelectedOutlineColor = Color.white;
    public Color HoveredOutlineColor = Color.white;

    public GameObject _shelf = null;

    public InteractableReference IngredientReference
    {
        get => _ingredientReference;
        set
        {
            if (_ingredientReference != value)
            {
                if (_ingredientReference)
                {
                    _ingredientReference.OnDeselect();   
                }
                _ingredientReference = value;   
            }
        }
    }

    public void Awake()
    {
        _shelf.GetComponent<Collider>().enabled = false;
    }
}
