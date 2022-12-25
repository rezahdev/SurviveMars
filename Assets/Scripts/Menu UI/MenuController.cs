using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEditor;

public class MenuController : MonoBehaviour
{
    [SerializeField]
    private Button startButton;

    // Start is called before the first frame update
    void Start()
    {
        startButton.onClick.AddListener(StartGame);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void StartGame()
    {
        print("hello");
        Object[]objects = Resources.FindObjectsOfTypeAll(typeof(GameObject));

        foreach(Object o in objects)
        {
            if(typeof(GameObject).IsAssignableFrom(o.GetType()))
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
}
