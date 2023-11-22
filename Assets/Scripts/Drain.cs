using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class CustomInteractions : MonoBehaviour
{
    private Outline _outline;

    [SerializeField] private UnityEvent _onClick;

    private void Awake()
    {
        _outline = this.AddComponent<Outline>();
        if(_outline) _outline.enabled = false;
    }

    public void OnMouseEnter()
    {
        if(_outline) _outline.enabled = true;
    }

    public void OnMouseExit()
    {
        if(_outline) _outline.enabled = false;
    }

    public void OnMouseUpAsButton()
    {
        if(_outline) _outline.enabled = true;
        
        _onClick?.Invoke();
    }
}
