using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSwitcher : MonoBehaviour
{
    [SerializeField] private Hypnos.Entities.Entity player;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private CameraFollowObject cameraFollowObject;

    [SerializeField] private int currentLevel = 1;

    [SerializeField] private Level[] levels;

    //TODO: Make mutable in editor, and have it update the level in the scene when changed
    public readonly float LEVEL_X_OFFSET = 10.0f;

    private void Start()
    {
        SwitchToLevel(currentLevel);
    }

    void OnValidate()
    {
        Debug.Assert(levels.Length > 0, "Levels array is empty");
        Debug.Assert(player != null, "Player is null");
        Debug.Assert(mainCamera != null, "Main camera is null");
        Debug.Assert(cameraFollowObject != null, "Camera follow object is null");

        SwitchToLevel(currentLevel);
    }

    void Update()
    {

    }

    public void SwitchToLevel(int level)
    {
        if (level < 1 || level > levels.Length)
        {
            Debug.LogError("Level " + level + " does not exist");
            return;
        }

        // >> TODO: Clean this up
        currentLevel = level;
        var levelObject = levels[level - 1];

        float xOffset = (level-1) * LEVEL_X_OFFSET;

        player.transform.position = levelObject.playerPosition + Vector2.right * xOffset;
        player.transform.localScale = new Vector3(levelObject.playerXScale, player.transform.localScale.y, player.transform.localScale.z);
        mainCamera.transform.position = new Vector3(
            levelObject.cameraPosition.x + xOffset,
            levelObject.cameraPosition.y,
            mainCamera.transform.position.z
        );

        CameraPrefs offsettedPrefs = ScriptableObject.CreateInstance<CameraPrefs>();
        offsettedPrefs.minX = levelObject.cameraPrefs.minX + xOffset;
        offsettedPrefs.maxX = levelObject.cameraPrefs.maxX + xOffset;
        offsettedPrefs.minY = levelObject.cameraPrefs.minY;
        offsettedPrefs.maxY = levelObject.cameraPrefs.maxY;
        offsettedPrefs.followX = levelObject.cameraPrefs.followX;
        offsettedPrefs.followY = levelObject.cameraPrefs.followY;
        cameraFollowObject.prefs = offsettedPrefs;
        // <<
    }

}
