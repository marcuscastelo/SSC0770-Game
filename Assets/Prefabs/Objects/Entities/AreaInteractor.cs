using UnityEngine;

public class AreaInteractor: MonoBehaviour, IInteractor
{
    public Player player;

    private SelectableObject _selectedObject = null;

    void Awake()
    {
        if (player == null)
        {
            Debug.LogError("Interactor: Player is null on " + gameObject.name);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("PLAYER: Collision with " + collision.gameObject.name);
        if (collision.gameObject.tag == "SelectableObject")
        {
            GameObject parentGameObject = collision.gameObject.transform.parent.gameObject;
            Debug.Log("PLAYER: Object is tagged SelectableObject");
            SelectableObject selectableObj = parentGameObject.GetComponent<SelectableObject>();
            if (selectableObj != null) {
                Debug.Log("PLAYER: Object has SelectableObject component");
                Select(selectableObj);
            } else {
                Debug.LogError("PLAYER: Object has no SelectableObject component");
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("PLAYER: Exit collision with " + collision.gameObject.name);
        if (collision.gameObject.tag == "SelectableObject")
        {
            GameObject parentGameObject = collision.gameObject.transform.parent.gameObject;
            Debug.Log("PLAYER: Object is tagged SelectableObject");
            SelectableObject selectableObj = parentGameObject.GetComponent<SelectableObject>();
            if (selectableObj != null)
            {
                Debug.Log("PLAYER: Object has SelectableObject component");
                Deselect();
            }
        }
    }

    private void Select(SelectableObject selectableObject)
    {
        if (_selectedObject != null)
        {
            _selectedObject.SetSelected(false);
        }

        _selectedObject = selectableObject;
        _selectedObject.SetSelected(true);
    }

    private void Deselect()
    {
        if (_selectedObject != null)
        {
            _selectedObject.SetSelected(false);
        }
        _selectedObject = null;        
    }
    
    public void Interact()
    {
        if (_selectedObject != null)
        {
            _selectedObject.OnInteractedBy(player);
        }
    }
}