using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class FirstController : MonoBehaviour, IUserAction, sceneController
{
    public PatrolFactory patrolFactory;
    public TreasureFactory treasureFactory;
    public ScoreController scoreController;
    public CCActionManager actionManager;
    public CameraFlow cameraFlow;
    public GameObject player;
    private float player_speed;
    private float rotate_speed;
    private bool game_over;

    void Awake()
    {
        LoadResources();
        Director director = Director.getInstance();
        director.currentSceneController = this;
        this.gameObject.AddComponent<CCActionManager>();
        actionManager = Singleton<CCActionManager>.Instance;
        this.gameObject.AddComponent<PatrolFactory>();
        patrolFactory = Singleton<PatrolFactory>.Instance;
        this.gameObject.AddComponent<TreasureFactory>();
        treasureFactory = Singleton<TreasureFactory>.Instance;
        this.gameObject.AddComponent<ScoreController>();
        scoreController = Singleton<ScoreController>.Instance;
        this.gameObject.AddComponent<CameraFlow>();
        cameraFlow = Singleton<CameraFlow>.Instance;
    }
    // Start is called before the first frame update
    void Start()
    {
        player_speed = 10f;
        rotate_speed = 150f;
        patrolFactory.init();
        treasureFactory.init();
        foreach(PatrolInfo patrolInfo in patrolFactory.GetPatrols()) {
            actionManager.GoPatrol(patrolInfo);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(treasureFactory.getCnt() == 0) {
            Gameover();
        }
    }

    public void LoadResources()
    {
        GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/map" ), Vector3.zero , Quaternion.identity);
        player = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/Player" ), Vector3.zero , Quaternion.identity);
        player.name = "Player";
    }

    public void MovePlayer(float translationX, float translationZ)
    {
        if(!game_over)
        {
            if (translationX != 0 || translationZ != 0)
            {
                player.GetComponent<Animator>().SetBool("run", true);
            }
            else
            {
                player.GetComponent<Animator>().SetBool("run", false);
            }
            player.transform.Translate(0, 0, translationZ * player_speed * Time.deltaTime);
            player.transform.Rotate(0, translationX * rotate_speed * Time.deltaTime, 0);
        }
        if (player.transform.localEulerAngles.x != 0 || player.transform.localEulerAngles.z != 0)
        {
            player.transform.localEulerAngles = new Vector3(0, player.transform.localEulerAngles.y, 0);
        }
        if (player.transform.position.y != 0)
        {
            player.transform.position = new Vector3(player.transform.position.x, 0, player.transform.position.z);
        }     
    }
    public int GetScore()
    {
        return scoreController.getScore();
    }
    public bool GetGameover()
    {
        return game_over;
    }
    public void Reset()
    {
        SceneManager.LoadScene("Scenes/test");
        
    }
    public void Gameover()
    {
        game_over = true;
        player.GetComponent<Animator>().SetBool("run", false);
        patrolFactory.Stop();
        actionManager.Stop();
    }
    void OnEnable()
    {
        GameEventManager.gameover += Gameover;
    }
    void OnDisable()
    {
        GameEventManager.gameover -= Gameover;
    }
}
