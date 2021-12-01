using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Hypnos.Audio;
using Zenject;

[ExecuteInEditMode]
public class LevelSwitcher : MonoBehaviour
{
    [SerializeField] private Hypnos.Entities.Entity player;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private CameraFollowObject cameraFollowObject;

    [SerializeField] private int currentLevel = 1;

    [SerializeField] private Level[] levels;

    [SerializeField] private float LEVEL_X_OFFSET = 50.0f;

    private Clock _clock;
    private AudioSystem _audioSystem;

    [Inject]
    public void Construct(Clock clock, AudioSystem audioSystem)
    {
        _clock = clock;
        _audioSystem = audioSystem;
    }

    private void Start()
    {
        SwitchToLevel(currentLevel);
    }

    void OnValidate()
    {
        if (!isActiveAndEnabled)
            return;

        Debug.Assert(levels.Length > 0, "Levels array is empty");
        Debug.Assert(player != null, "Player is null");
        Debug.Assert(mainCamera != null, "Main camera is null");
        Debug.Assert(cameraFollowObject != null, "Camera follow object is null");

        SwitchToLevel(currentLevel);
    }

    void Update()
    {
        if (Application.isEditor)
        {
            UpdateCameraPrefs(currentLevel);
        }
    }

    private float CalculateLevelXOffset(int level) => (level - 1) * LEVEL_X_OFFSET;


    public void SwitchToLevel(int level)
    {
        if (level < 1 || level > levels.Length)
        {
            Debug.LogError("Level " + level + " does not exist");
            return;
        }

        currentLevel = level;
        InitializeLevelPlayMode(level);
        UpdateCameraPrefs(level);
        MovePlayerToLevel(level);
        MoveCameraToLevel(level);
    }

    private void InitializeLevelPlayMode(int level)
    {
        if (!Application.isPlaying) return; //TODO: inject on edit mode

        if (level == 1) InitializeLevel1();
        if (level == 2) InitializeLevel2();
        else 
        {
            Debug.LogWarning("Level " + level + " is not initializable");
            return;
        }   
    }

    private void InitializeLevel1()
    {
        _clock.Paused = true;
        _clock.Reset();
        _audioSystem.PlaySoundTrack(AudioType.ST_House);
    }

    private void InitializeLevel2()
    {
        _clock.Paused = false;
        _audioSystem.PlaySoundTrack(AudioType.ST_Street);
    }

    private void MovePlayerToLevel(int level)
    {
        var levelObject = levels[level - 1];
        player.transform.position = levelObject.playerPosition + Vector2.right * CalculateLevelXOffset(currentLevel);
        player.transform.localScale = new Vector3(levelObject.playerXScale, player.transform.localScale.y, player.transform.localScale.z);
    }

    private void MoveCameraToLevel(int level)
    {
        var levelObject = levels[level - 1];
        mainCamera.transform.position = new Vector3(
            levelObject.cameraPosition.x + CalculateLevelXOffset(level),
            levelObject.cameraPosition.y,
            mainCamera.transform.position.z
        );
    }

    private void UpdateCameraPrefs(int level)
    {
        float xOffset = CalculateLevelXOffset(level);
        var levelObject = levels[level - 1];
        CameraPrefs offsettedPrefs = ScriptableObject.CreateInstance<CameraPrefs>();
        offsettedPrefs.minX = levelObject.cameraPrefs.minX + xOffset;
        offsettedPrefs.maxX = levelObject.cameraPrefs.maxX + xOffset;
        offsettedPrefs.minY = levelObject.cameraPrefs.minY;
        offsettedPrefs.maxY = levelObject.cameraPrefs.maxY;
        offsettedPrefs.followX = levelObject.cameraPrefs.followX;
        offsettedPrefs.followY = levelObject.cameraPrefs.followY;
        cameraFollowObject.prefs = offsettedPrefs;
    }
}
