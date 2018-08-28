using UnityEngine;

public class GameManager : MonoBehaviour {
    public GameObject levelChangerPrefab;
    LevelChanger levelChanger;

	// Use this for initialization
	void Start () {
        levelChanger = Instantiate(levelChangerPrefab).GetComponent<LevelChanger>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public  void LevelComplete()
    {
        levelChanger.FadeToNextLevel();
    }
}
