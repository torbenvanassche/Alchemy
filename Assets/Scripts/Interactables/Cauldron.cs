using UnityEngine;

public class Cauldron : RecipeHandler
{
    [SerializeField] private Renderer _bubbleParticleRenderer = null;
    [SerializeField] private Renderer _liquidMaterial = null;

    [SerializeField] private Color defaultColor = Color.gray;
    [SerializeField] private Color invalidColor = Color.black;

    protected override void Initialize()
    {
        _liquidMaterial.material.color = defaultColor;
        _bubbleParticleRenderer.material.color = defaultColor;
    }

    protected override void OnMouseUpAsButton()
    {
        if (GameManager.Instance.IngredientReference &&GameManager.Instance.IngredientReference.interactable)
        {
            AddItem(GameManager.Instance.IngredientReference.interactable);
        }
    }

    protected override void OnStateChange()
    {
        switch (State)
        {
            case Interactable.RecipeState.Valid:
                var color = validCreations[0].onValidColor;
                _liquidMaterial.material.color = color;
                _bubbleParticleRenderer.material.color = color;
                break;
            case Interactable.RecipeState.Invalid:
                _liquidMaterial.material.color = invalidColor;
                _bubbleParticleRenderer.material.color = invalidColor;
                break;
            case Interactable.RecipeState.Incomplete:
                _liquidMaterial.material.color = defaultColor;
                _bubbleParticleRenderer.material.color = defaultColor;
                break;
        }
    }
}
