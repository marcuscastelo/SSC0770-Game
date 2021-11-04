using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialog : MonoBehaviour
{
    public DialogInfo dialogInfo;

    public Text titleRef;
    public Text contentRef;
    public Button confirmRef;
    public Button cancelRef;

    // Start is called before the first frame update
    void Start()
    {
        UpdateUI();
    }

    void OnValidate()
    {
        if (dialogInfo == null)
            dialogInfo = ScriptableObject.CreateInstance<DialogInfo>();

        UpdateUI();
    }

    void UpdateUI()
    {   
        titleRef.text = dialogInfo.title;
        contentRef.text = dialogInfo.content;

        if (dialogInfo.hasCancel) {
            cancelRef.gameObject.SetActive(true);
        }
        else
            cancelRef.gameObject.SetActive(false);

    }
}
