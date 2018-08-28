using UnityEngine;

public class LevelExit : MonoBehaviour {

    GameManager gameManager;
	// Use this for initialization
	void Start () {
        gameManager = GetComponentInParent<GameManager>();
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        gameManager.LevelComplete();
    }

}
