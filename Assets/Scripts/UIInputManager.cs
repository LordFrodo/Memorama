using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class UIInputManager : MonoBehaviour
{
    Button btn_start, btn_quit;
    bool intro = true;
    
    // Start is called before the first frame update
    void OnEnable()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;
        root.Q<VisualElement>("Intro").visible = true;
        root.Q<VisualElement>("Game").visible = false;

        btn_start = root.Q<Button>("StartGame");
        btn_quit = root.Q<Button>("Quit");

        btn_start.clicked += StartGame;
        btn_quit.clicked += QuitGame;
        
    }
    private void OnDisable()
    {
        btn_start.clicked -= StartGame;
        btn_quit.clicked -= QuitGame;
    }

    private void StartGame()
    {
        SceneManager.LoadScene("Game");
    }
    private void QuitGame()
    {
        Application.Quit();
    }

}
