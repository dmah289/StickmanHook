#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
[Serializable]
public enum TypeObject
{
    Player,
    Finish,
    Joint,
    Tile,
    Remove
}
public class CreateLevelData : MonoBehaviour
{
    public static CreateLevelData instance;

    [SerializeField] GameObject tile;
    [SerializeField] GameObject joint;
    [SerializeField] GameObject player;
    [SerializeField] GameObject finishLine;

    [SerializeField] public GameObject tiles;
    [SerializeField] public GameObject joints;

    [SerializeField] int levelIdx;
    [SerializeField] LevelData newLevel;

    [SerializeField] public TypeObject currentObject;
    

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }

        newLevel = new LevelData();
        currentObject = TypeObject.Player;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.J))
        {
            currentObject = TypeObject.Joint;
        }
        else if (Input.GetKey(KeyCode.T))
        {
            currentObject = TypeObject.Tile;
        }
        else if (Input.GetKey(KeyCode.P))
        {
            currentObject = TypeObject.Player;
        }
        else if(Input.GetKey(KeyCode.F))
        {
            currentObject = TypeObject.Finish;
        }
        else if(Input.GetKey(KeyCode.R))
        {
            currentObject = TypeObject.Remove;
        }
        else if (Input.GetKey(KeyCode.Return))
        {
            SaveData();
            SaveAssets();
        }
        else if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition).With(z: 0);

            switch (currentObject)
            {
                case TypeObject.Joint:
                    GameObject newJoint = Instantiate(joint, mousePos, Quaternion.identity);
                    newJoint.transform.SetParent(joints.transform);
                    break;

                case TypeObject.Tile:
                    GameObject newTile = Instantiate(tile, mousePos, Quaternion.identity);
                    newTile.transform.SetParent(tiles.transform);
                    break;

                case TypeObject.Player:
                    GameObject newPlayer = Instantiate(player, mousePos, Quaternion.identity);
                    newLevel.playerPos = newPlayer.transform.position;
                    break;

                case TypeObject.Finish:
                    GameObject newFinishLine = Instantiate(finishLine, mousePos, Quaternion.identity);
                    newLevel.finishPos = newFinishLine.transform.position;
                    break;
            }
        }

    }

    private void SaveData()
    {
        newLevel.jointsPos = new Vector3[joints.transform.childCount];
        newLevel.tilesData = new TileData[tiles.transform.childCount];
        for(int i = 0; i < joints.transform.childCount; i++)
        {
            newLevel.jointsPos[i] = joints.transform.GetChild(i).position;
        }

        for(int i = 0; i < tiles.transform.childCount; i++)
        {
            TileData newTileData = new TileData();
            newTileData.position = tiles.transform.GetChild(i).transform.position;
            newTileData.rotation = tiles.transform.GetChild(i).transform.rotation;
            newLevel.tilesData[i] = newTileData;
        }
    }

    private void SaveAssets()
    {
        string assetPath = $"{KeySave.levelDataPath}/Level {levelIdx}.asset";
        EditorUtility.SetDirty(newLevel);
        AssetDatabase.CreateAsset(newLevel, assetPath);
        AssetDatabase.SaveAssets();
    }

}
#endif
