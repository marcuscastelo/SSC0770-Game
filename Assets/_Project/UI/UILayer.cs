using UnityEngine;

public class UILayer : MonoBehaviour
{
    public bool Active { get { return gameObject.activeSelf; } }
    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void Toggle()
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }

    public void SetActive(bool active)
    {
        gameObject.SetActive(active);
    }
}