using UnityEngine;

public class Cauldron : Crafter
{
    [SerializeField] private Renderer _bubbleParticleRenderer = null;
    [SerializeField] private Renderer _liquidMaterial = null;

    [SerializeField] private Color defaultColor = Color.gray;
    [SerializeField] private Color invalidColor = Color.black;

    protected override void OnMouseUpAsButton()
    {
        if (GameManager.Instance.IngredientReference &&GameManager.Instance.IngredientReference.interactable)
        {
            AddToCurrentRecipe(GameManager.Instance.IngredientReference.interactable);
        }
    }

    protected override void OnAwake()
    {
        _liquidMaterial.material.color = defaultColor;
        _bubbleParticleRenderer.material.color = defaultColor;
    }

    protected override void OnStateChange()
    {
        switch (State)
        {
            case RecipeState.Valid:
                var color = validRecipes[0].onValidColor;
                _liquidMaterial.material.color = color;
                _bubbleParticleRenderer.material.color = color;
                break;
            case RecipeState.Invalid:
                _liquidMaterial.material.color = invalidColor;
                _bubbleParticleRenderer.material.color = invalidColor;
                break;
            case RecipeState.Incomplete:
                _liquidMaterial.material.color = defaultColor;
                _bubbleParticleRenderer.material.color = defaultColor;
                break;
        }
    }
}
