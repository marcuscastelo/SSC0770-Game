using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Hypnos.Entities;
using Zenject;

public class HealthUIMock : MonoBehaviour
{
    [SerializeField] private float xOffset = 15f;
    [SerializeField] private Sprite _heartSprite;
    // [SerializeField] private Sprite _emptyHeartSprite; //TODO: Implement this

    [SerializeField] Entity player;

    void Start() {
        player.Health.OnHealthChanged += UpdateHealthDisplay;
        UpdateHealthDisplay(player.Health.CurrentHealth);
    }

    private void UpdateHealthDisplay(int currentHealth)
    {
        // Debug.Log("HealthUIMock: " + currentHealth);
        //Delete all children
        GameObject[] children = new GameObject[transform.childCount];

        {
            int i = 0;
            foreach (Transform child in transform)
                children[i++] = child.gameObject;

            foreach (GameObject child in children)
            {
                if (Application.isEditor && !Application.isPlaying)
                    DestroyImmediate(child);
                else
                    Destroy(child);
            }
        }

        for (int i = 0; i < currentHealth; i++)
        {
            //Creates a new gameobject for each heart and sets its sprite to the heart sprite and sets its parent to the health ui
            GameObject heart = new GameObject("Heart", typeof(RectTransform));
            heart.transform.SetParent(transform);
            heart.transform.localScale = Vector3.one;
            heart.transform.localPosition = new Vector3(i * xOffset, 0, 0);
            Image heartImage = heart.AddComponent<Image>();
            heartImage.sprite = _heartSprite;
            heartImage.SetNativeSize();
        }
    }   

    [ContextMenu("Test Health")]
    void Test()
    {
        UpdateHealthDisplay(4);
    }

}
