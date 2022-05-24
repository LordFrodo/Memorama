using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
using System;
using System.IO;

public class GameManager : MonoBehaviour
{
    JSONReader json_file;
    Button[] buttons;
    Button exit, restart;
    float time;
    int click_counter;
    bool win;
    Button selected1, selected2;
    int win_condition = 4;
    int counter;


    Results results = new Results();
    private string path = "";
    private string persistentPath = "";

    // Start is called before the first frame update
    void Start()
    {
        json_file = GetComponent<JSONReader>();
        SetPaths();
        buttons = new Button[json_file.myBlocklist.blocks.Length];
        var root = GameObject.Find("GameScreen").GetComponent<UIDocument>().rootVisualElement;
        root.Q<VisualElement>("Intro").visible = false;
        root.Q<VisualElement>("Game").visible = true;
        exit = root.Q<Button>("Exit");
        exit.clicked += ReturnMenu;
        restart = root.Q<Button>("Restart");
        restart.clicked += Restart;
        int temp = 0;
        for (int i = 0; i < json_file.myBlocklist.blocks.Length / 3; i++)
        {
            for (int j = 0; j < json_file.myBlocklist.blocks.Length / 4; j++)
            {
                string name = (i + 1).ToString() + "-" + (j + 1).ToString();
                buttons[temp] = root.Q<Button>(name);
                //buttons[temp].text = json_file.myBlocklist.blocks[temp].number;
                temp++;
            }
        }
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].clickable.clickedWithEventInfo += WichBtn;
        }
        temp = 1;
    }

    private void ReturnMenu()
    {
        SceneManager.LoadScene("Intro");
    }
    private void Restart()
    {
        SceneManager.LoadScene("Game");
    }

    public void SelectCard(string name, Button btn)
    {

        for (int i = 0; i < json_file.myBlocklist.blocks.Length; i++)
        {
            if (name == json_file.myBlocklist.blocks[i].R + "-" + json_file.myBlocklist.blocks[i].C) btn.text = json_file.myBlocklist.blocks[i].number;
        }
        click_counter++;
        if (selected1 != null && selected2 != null) CheckNumber();


    }
    public void WichBtn(EventBase btnEvent)
    {
        if (selected1 == null || selected2 == null)
        {
            Button button = btnEvent.target as Button;
            button.SetEnabled(false);
            if (selected1 == null) selected1 = button;
            else selected2 = button;
            SelectCard(button.name, button);
        }
    }
    public void CheckNumber()
    {
        if (selected1.text == selected2.text)
        {
            selected1 = null;
            selected2 = null;
            counter++;
        }
        else Invoke("ResetCards", 0.5f);
    }
    public void ResetCards()
    {
        selected1.text = "";
        selected2.text = "";
        selected1.SetEnabled(true);
        selected2.SetEnabled(true);
        selected1 = null;
        selected2 = null;
    }
    private void SetPaths()
    {
        path = Application.dataPath + Path.AltDirectorySeparatorChar + "SaveData.json";
        persistentPath = Application.persistentDataPath + Path.AltDirectorySeparatorChar + "SaveData.json";
    }

    // Update is called once per frame
    void Update()
    {
        if (win == false) time += Time.deltaTime;
        if (counter == win_condition)
        {
            win = true;
            SaveData();
        }

    }
    public void SaveData()
    {
        var root = GameObject.Find("GameScreen").GetComponent<UIDocument>().rootVisualElement;
        root.Q<VisualElement>("Win").visible = true;
        results.total_clicks = click_counter;
        results.total_time = time;

        string json = JsonUtility.ToJson(results);
        using StreamWriter writer = new StreamWriter(path);
        writer.Write(json);
    }
}
