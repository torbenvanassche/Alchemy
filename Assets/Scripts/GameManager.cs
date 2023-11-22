using UnityEngine;
using UnityEngine.UIElements;
using Utilities;

public class GameManager : Singleton<GameManager>
{
    public Cauldron _Cauldron = null;

    private ItemReference _currentSelection = null;
    
    [Header("Outlines")]
    public OutlineSettings SelectedOutline = null;
    public OutlineSettings HoveredOutline = null;

    [SerializeField] private UIDocument runtimeUI = null;
    [HideInInspector] public Tooltip Tooltip = null;

    public ItemReference IngredientReference
    {
        get => _currentSelection;
        set
        {
            if (_currentSelection != value)
            {
                if (_currentSelection)
                {
                    _currentSelection.OnDeselect();
                }

                _currentSelection = value;
            }
        }
    }

    private void Awake()
    {
        Tooltip = new Tooltip(Camera.main, Color.white, 20, 5);
        runtimeUI.rootVisualElement.Add(Tooltip);
    }
}
