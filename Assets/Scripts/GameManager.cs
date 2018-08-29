using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager gm;
    public static AudioManager am;
    [SerializeField]
    AudioManager audioManager;
    LevelChanger levelChanger;
    public GameObject levelChangerPrefab;


	// Use this for initialization
	void Awake () {
        
        if(gm == null)
        {
            gm = this;
            am = audioManager;
            levelChanger = Instantiate(levelChangerPrefab).GetComponent<LevelChanger>();
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void StartGame()
    {
        levelChanger.FadeToLevel(1);
    }

    public  void LevelComplete()
    {
        levelChanger.FadeToNextLevel();
    }

    public void Damage(Character damager, Character damaged)
    {
        Debug.Log("Game Manager " + damager + " Hit " + damaged);
    }
}
