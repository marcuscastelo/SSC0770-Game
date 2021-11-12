using UnityEngine;

[CreateAssetMenu(fileName = "Level", menuName = "ScriptableObjects/Level")]
public class Level : ScriptableObject {
    public int level;
    public Vector2 playerPosition;
    public int playerXScale = 1;
    public Vector2 cameraPosition;
    public CameraPrefs cameraPrefs;
}