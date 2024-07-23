using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] public CameraFollow cameraFollow;
    [SerializeField] public GameObject finishLine;
    [SerializeField] public Stickman stickman;
    [SerializeField] public GameObject winEffect;
    [SerializeField] public GameObject anchor;

    [SerializeField] public GameObject player;
    [SerializeField] public Rigidbody2D playerRb;

    public bool won;
    
    private void Awake()
    {
        if(instance == null)
            instance = this;
        won = false;
        DontDestroyOnLoad(gameObject);
        Application.targetFrameRate = 60;
    }

    private void Start()
    {
        stickman.gameObject.SetActive(true);
    }

    private void Update()
    {
        if (!stickman.Sticked && (player.transform.position.x < -3 || player.transform.position.y < -5))
            LevelManager.instance.PlayerRestart();
        if (player.transform.position.x > finishLine.transform.position.x && !won)
        {
            won = true;
            LevelManager.instance.NextLevel();
        }
    }
}
