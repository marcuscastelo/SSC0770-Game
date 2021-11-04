using UnityEngine;

[CreateAssetMenu(fileName = "CameraPrefs", menuName = "ScriptableObjects/CameraPrefs")]
public class CameraPrefs : ScriptableObject
{
    public bool followX = true;
    public bool followY = true;

    public float minX = -10;
    public float maxX = 10;

    public float minY = -10;
    public float maxY = 10;
}