using UnityEngine;

using UnityEngine.SceneManagement;

using Hypnos.Entities;
using Zenject;

public class PlayerDeathResponse : MonoBehaviour
{
    [SerializeField] private DialogInfo deathDialogInfo;

    private Entity _entity;

    [Inject]
    private void Construct(Entity entity)
    {
        _entity = entity;
    }

    private void Awake()
    {
        if (deathDialogInfo == null)
        {
            Debug.LogWarning("PlayerDeathResponse: deathDialogInfo is null, using default");
            deathDialogInfo = CreateDefaultDialogInfo();
        }
    }

    private void Start()
    {
        _entity.Health.OnDeath += OnDeath;
    }

    private DialogInfo CreateDefaultDialogInfo()
    {
        DialogInfo dialogInfo = ScriptableObject.CreateInstance<DialogInfo>();
        dialogInfo.title = "You died!";
        dialogInfo.content = "You died!\nRestart?";
        dialogInfo.buttons = DialogButtonCombination.OK;
        return dialogInfo;
    }


    private void OnDeath()
    {
        Debug.Log("Player died");
        
        Time.timeScale = 0; //TODO: ?
        
        DialogSystem.ShowDialog(new Dialog(deathDialogInfo, (result) =>
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            Time.timeScale = 1;
        }));
    }
}