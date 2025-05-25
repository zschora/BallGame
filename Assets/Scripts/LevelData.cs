using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "Game/Level Data")]
public class LevelData : ScriptableObject
{
    public Vector2 ballStartPosition;
    public Vector2 goalPosition;
    public List<PlaceableObjectData> initialObjects;
    public List<InventoryItemData> availableObjects;
}

[System.Serializable]
public class PlaceableObjectData
{
    public SpawnedObjectType type;
    public Vector2 position;
    public float rotation;
    public bool movable;
}

[System.Serializable]
public class InventoryItemData
{
    public SpawnedObjectType type;
    public int quantity;
}

public enum SpawnedObjectType
{
    Ball,
    Beam,
    Goal,
    Ground,
    Star
}
