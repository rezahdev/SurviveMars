using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameplayController : MonoBehaviour
{
    [SerializeField]
    private Button startButton, exitButton;

    private bool isPaused = false;

    void Awake()
    {
        startButton.onClick.AddListener(StartGame);
        exitButton.onClick.AddListener(ExitGame);
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && !GlobalSettings.IsGamePaused)
        {
            PauseGame();
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && GlobalSettings.IsGamePaused)
        {
            StartGame();
        }
        else if((Input.GetKeyDown(KeyCode.X) || Input.GetKeyDown(KeyCode.X)) && GlobalSettings.IsGamePaused)
        {
            EndGame();
        }
    }
    void StartGame()
    {
        GlobalSettings.IsGamePlaying = true;
        GlobalSettings.IsGamePaused = false;
        GlobalSettings.IsGameFinished = false;

        Object[] objects = Resources.FindObjectsOfTypeAll(typeof(GameObject));

        foreach (Object o in objects)
        {
            if (typeof(GameObject).IsAssignableFrom(o.GetType()))
            {
                GameObject go = (GameObject)o;
                if (go.name == "Player"
                    || go.name == "Crosshair Canvas"
                    || go.name == "Alien"
                    || go.name == "Gameplay Canvas"
                    || go.name == "Enemy Manager")
                {
                    go.SetActive(true);
                }
                else if (go.name == "Menu Canvas" || go.name == "Menu Camera")
                {
                    go.SetActive(false);
                }
            }
        }
    }

    void PauseGame()
    {
        GlobalSettings.IsGamePlaying = false;
        GlobalSettings.IsGamePaused = true;
        GlobalSettings.IsGameFinished = false;

        Object[] objects = Resources.FindObjectsOfTypeAll(typeof(GameObject));

        foreach (Object o in objects)
        {
            if (typeof(GameObject).IsAssignableFrom(o.GetType()))
            {
                GameObject go = (GameObject)o;
                if (go.name == "Player"
                    || go.name == "Crosshair Canvas"
                    || go.name == "Alien"
                    || go.name == "Enemy Manager"
                    || go.name == "Alien(Clone)")
                {
                    go.SetActive(false);
                }
                else if (go.name == "Menu Canvas" || go.name == "Menu Camera")
                {
                    go.SetActive(true);
                }
                else if(go.name == "Start Button")
                {
                    go.GetComponent<Button>().onClick.AddListener(StartGame);
                }
                else if (go.name == "Start Button Text")
                {
                    go.GetComponent<TextMeshProUGUI>().text = "Press Esc to Resume";
                }
                else if(go.name == "End Button")
                {
                    go.GetComponent<Button>().gameObject.SetActive(true);
                    go.GetComponent<Button>().onClick.AddListener(EndGame);
                }
                else if (go.name == "Welcome Text")
                {
                    go.GetComponent<TextMeshProUGUI>().text = "Game is Paused!";
                }
            }
        }
    }

    public void EndGame()
    {
        print("hsgfhdfgh");
        GlobalSettings.IsGamePlaying = false;
        GlobalSettings.IsGamePaused = false;
        GlobalSettings.IsGameFinished = true;

        if (Cursor.lockState == CursorLockMode.Locked)
        {
            Cursor.lockState = CursorLockMode.None;
        }
   
        Object[] objects = Resources.FindObjectsOfTypeAll(typeof(GameObject));

        foreach (Object o in objects)
        {
            if (typeof(GameObject).IsAssignableFrom(o.GetType()))
            {
                GameObject go = (GameObject)o;
                if (go.name == "Player"
                   || go.name == "Crosshair Canvas"
                   || go.name == "Alien"
                   || go.name == "Enemy Manager"
                   || go.name == "Gameplay Canvas"
                   || go.name == "Alien(Clone)")
                {
                    go.SetActive(false);
                }
                else if (go.name == "Menu Canvas" || go.name == "Menu Camera")
                {
                    go.SetActive(true);
                }
                else if (go.name == "Player" && go.GetComponent<PlayerStats>())
                {
                    go.GetComponent<PlayerStats>().SetScoreToZero();
                }
                else if (go.name == "Start Button")
                {
                    go.GetComponent<Button>().onClick.AddListener(RestartGame);
                }
                else if (go.name == "Start Button Text")
                {
                    go.GetComponent<TextMeshProUGUI>().text = "Main Menu";
                }
                else if(go.name == "Welcome Text")
                {
                    go.GetComponent<TextMeshProUGUI>().text = "Your Score is: " + GlobalSettings.PlayerScore;
                }
                else if (go.name == "End Button")
                {
                    go.GetComponent<Button>().gameObject.SetActive(false);
                }
                else if (go.name == "Alien(Clone)")
                {
                    Destroy(go);
                }
            }
        }
    }

    public void RestartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("SampleScene");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
