using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Level Data")]
public class LevelData : ScriptableObject
{
    public Vector3 playerPos;
    public Vector3 finishPos;
    public Vector3[] jointsPos;
    public TileData[] tilesData;

    public static class Database
    {
        public static LevelData GetCurrentLevelDataByIndex(int idx)
            => Resources.Load<LevelData>($"LevelData/Level {idx}");
    }

}

[Serializable]
public class TileData
{
    public Vector3 position;
    public Quaternion rotation;
}
