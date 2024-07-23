using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    public int levelIndex;
    public int maxLevel;
    public JointAnchor jointPrefab;
    public TileBounce tilePrefab;
    public Stickman stickmanPrefab;

    public LevelData currentLevel;
    public List<JointAnchor> joints;
    public List<TileBounce> tiles;

    [SerializeField] private Vector3 initWinEffectLocalPos;

    private void Awake()
    {

        if (instance == null)
            instance = this;


        joints = new List<JointAnchor>();
        tiles = new List<TileBounce>();

        DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {
        currentLevel = LevelData.Database.GetCurrentLevelDataByIndex(0);

        SetUpPool();
        // Tránh việc GameManager chưa kịp khởi tạo instance
        SetUpCurrentLevelPosition();
    }
    private void SetUpPool()
    {
        ObjectPooler.SetUpPool<JointAnchor>(jointPrefab, 20, KeySave.joint);
        ObjectPooler.SetUpPool<TileBounce>(tilePrefab, 20, KeySave.tileBounce);
    }

    public void SetUpCurrentLevelPosition()
    {
        GameManager.instance.player.transform.position = currentLevel.playerPos;
        
        GameManager.instance.finishLine.transform.position = currentLevel.finishPos;

        for (int i = 0; i < currentLevel.jointsPos.Length; i++)
        {
            JointAnchor joint = ObjectPooler.DequeueObject<JointAnchor>(KeySave.joint);
            joints.Add(joint);
            joint.transform.position = currentLevel.jointsPos[i];
        }

        if (currentLevel.tilesData.Length > 0)
        {
            for (int i = 0; i < currentLevel.tilesData.Length; i++)
            {
                TileBounce tileBounce = ObjectPooler.DequeueObject<TileBounce>(KeySave.tileBounce);
                tiles.Add(tileBounce);
                tileBounce.transform.position = currentLevel.tilesData[i].position;
            }
        }
    }

    public void PlayerRestart()
    {
        GameManager.instance.player.SetActive(false);
        StartCoroutine(WaitTrail());
        GameManager.instance.player.SetActive(true);
    }

    public void NextLevel()
    {
        // Win Setup
        GameManager.instance.stickman.Win();
        GameManager.instance.cameraFollow.Win();

        GameManager.instance.winEffect.SetActive(true);
        GameManager.instance.winEffect.transform.SetParent(null);

        StartCoroutine(FinishLevel());

    }

    IEnumerator FinishLevel()
    {
        yield return new WaitForSeconds(3);

        CollectPool();
        GameManager.instance.winEffect.SetActive(false);
        GameManager.instance.winEffect.transform.SetParent(GameManager.instance.player.transform);
        GameManager.instance.winEffect.transform.localPosition = initWinEffectLocalPos;
        GameManager.instance.winEffect.transform.localRotation = Quaternion.Euler(-60, 270, -180);

        // Load new level data
        levelIndex = (levelIndex + 1) % maxLevel;
        currentLevel = LevelData.Database.GetCurrentLevelDataByIndex(levelIndex);
        SetUpCurrentLevelPosition();

        // Reset stickman, camera
        PlayerRestart();
        GameManager.instance.won = false;
        GameManager.instance.cameraFollow.Reset();
        
        
    }

    IEnumerator WaitTrail()
    {
        yield return new WaitForSeconds(0.3f);
        GameManager.instance.stickman.trailRenderer.enabled = true;
    }

    public void CollectPool()
    {
        while(joints.Count > 0)
        {
            ObjectPooler.EnqueueObject<JointAnchor>(KeySave.joint, joints[0]);
            joints.RemoveAt(0);
        }
        while(tiles.Count > 0)
        {
            ObjectPooler.EnqueueObject<TileBounce>(KeySave.tileBounce, tiles[0]);
            tiles.RemoveAt(0);
        }
    }
}
