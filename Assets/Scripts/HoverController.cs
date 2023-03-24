using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;
using Utilities;

public class HoverController : Singleton<HoverController>
{
    private UIDocument _document;
    private Label _label = null;
    private VisualElement _labelParent = null;

    private Label _errorMessage = null;

    // Start is called before the first frame update
    void Start()
    {
        _document = GetComponent<UIDocument>();
        _label = _document.rootVisualElement.Q<Label>("Hover");
        _errorMessage = _document.rootVisualElement.Q<Label>("Error");

        _labelParent = _label.parent;
        
        _labelParent.visible = false;
        _errorMessage.visible = false;
    }

    public void Draw(string text)
    {
        _label.text = text;
        _labelParent.visible = true;
    }

    public void UpdatePosition(Vector3 position)
    {
        if (_labelParent.visible)
        {
            var screenPosition = RuntimePanelUtils.CameraTransformWorldToPanel(_document.rootVisualElement.panel, position, Camera.main);
            _labelParent.transform.position = screenPosition;
        }
    }

    public void ShowError(string error)
    {
        _errorMessage.text = error;
        _errorMessage.visible = true;
        
        StartCoroutine(ResetText());
    }

    private IEnumerator ResetText()
    {
        yield return new WaitForSeconds(5);
        _errorMessage.visible = false;
    }

    public void Remove()
    {
        _labelParent.visible = false;
    }
}
