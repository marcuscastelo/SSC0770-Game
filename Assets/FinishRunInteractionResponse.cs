using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using Hypnos.Core;
using Zenject;

public class FinishRunInteractionResponse : MonoBehaviour, IInteractionResponse
{
    [SerializeField] private DialogInfo winDialog;
    [SerializeField] private DialogInfo loseDialog;
    [SerializeField] private string nextSceneIfWin; 
    [SerializeField] private string nextSceneIfLose;

    Clock _clock;
    LevelSwitcher _levelSwitcher;

    void Awake()
    {
        if (winDialog == null)
        {
            Debug.LogWarning("No win dialog set for FinishRunInteractionResponse, using default");
            winDialog = ScriptableObject.CreateInstance<DialogInfo>();
            winDialog.title = "You win!";
            winDialog.content = "You finished the run with $time seconds remaining!";
            winDialog.buttons = DialogButtonCombination.OK;
        }

        if (loseDialog == null)
        {
            Debug.LogWarning("No lose dialog set for FinishRunInteractionResponse, using default");
            loseDialog = ScriptableObject.CreateInstance<DialogInfo>();
            loseDialog.title = "You lose!";
            loseDialog.content = $"You did not finish in time!";
            loseDialog.buttons = DialogButtonCombination.OK;
        }

        if (SceneManager.GetSceneByName(nextSceneIfWin) == null)
            Debug.LogError("FinishRunInteractionResponse: Scene " + nextSceneIfWin + " not found");

        if (SceneManager.GetSceneByName(nextSceneIfLose) == null)
            Debug.LogError("FinishRunInteractionResponse: Scene " + nextSceneIfLose + " not found");
    }

    [Inject]
    public void Construct(Clock clock, LevelSwitcher levelSwitcher)
    {
        _clock = clock;
        _levelSwitcher = levelSwitcher;
    }

    public void OnInteracted(Interaction interaction)
    {
        bool won = _clock.CurrentTime > 0;
        DialogInfo dialogInfo = ScriptableObject.CreateInstance<DialogInfo>();

        //Copy the dialog info from the win or lose dialog (why do we need to do this?)
        dialogInfo.title = won ? winDialog.title : loseDialog.title; 
        dialogInfo.content = won ? winDialog.content : loseDialog.content;
        dialogInfo.buttons = won ? winDialog.buttons : loseDialog.buttons;

        dialogInfo.content = dialogInfo.content.Replace("$time", Mathf.Ceil(_clock.CurrentTime).ToString());

        Time.timeScale = 0;
        Dialog dialog = new Dialog(dialogInfo, _ => {
            Time.timeScale = 1;
            SceneManager.LoadScene(won ? nextSceneIfWin : nextSceneIfLose);
            interaction.EndInteraction(true);
        });

        DialogSystem.ShowDialog(dialog);
    }
}
