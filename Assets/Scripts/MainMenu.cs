
using UnityEngine;

public class MainMenu : MonoBehaviour {
    public LevelChanger levelChanger;
	// Use this for initialization
	public void PlayGame()
    {
        levelChanger.FadeToNextLevel();
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
