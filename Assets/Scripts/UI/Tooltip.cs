using UnityEngine;
using UnityEngine.UIElements;

public class Tooltip : VisualElement
{
    private Label runtimeLabel;

    private Camera _camera = null;
    private Color _color = Color.white;

    public Tooltip(Camera camera, Color backgroundColor, int fontSize, int padding)
    {
        runtimeLabel = new Label();
        Add(runtimeLabel);

        style.alignItems = Align.FlexStart;

        _camera = camera;
        _color = backgroundColor;

        runtimeLabel.style.paddingBottom = padding;
        runtimeLabel.style.paddingLeft = padding;
        runtimeLabel.style.paddingRight = padding;
        runtimeLabel.style.paddingTop = padding;

        runtimeLabel.style.fontSize = fontSize;
    }

    public void Draw(Hoverable hoverable)
    {
        var screenPosition = RuntimePanelUtils.CameraTransformWorldToPanel(panel, hoverable.transform.position, _camera);
        transform.position = screenPosition;

        runtimeLabel.text = hoverable.displayName;
        runtimeLabel.style.backgroundColor = _color;
        visible = true;
    }

    public void Remove()
    {
        visible = false;
        runtimeLabel.text = string.Empty;
    }
}
