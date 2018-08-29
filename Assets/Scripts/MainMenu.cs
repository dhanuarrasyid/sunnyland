
using UnityEngine;
using UnityEngine.EventSystems;

public class MainMenu : MonoBehaviour {
    EventSystem eventSystem;
    GameObject lastSelected;
    private void Start()
    {
        eventSystem = EventSystem.current.GetComponent<EventSystem>();
    }

    private void Update()
    {
        if(lastSelected != eventSystem.currentSelectedGameObject)
        {
            if(lastSelected != null){
                GameManager.am.Play("UISelect");
            }
            lastSelected = eventSystem.currentSelectedGameObject;
            Debug.Log("Selection Changed " + lastSelected);
        }
    }
    public void PlayGame()
    {
        GameManager.gm.StartGame();
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
