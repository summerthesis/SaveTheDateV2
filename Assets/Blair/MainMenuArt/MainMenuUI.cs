using System.Collections;
using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MainMenuUI : MonoBehaviour
{
    
    public GameObject[] MenuItems;
    public PlayerInputAction controls;
    
    private int Selection;//0 new, 1 options, 2 credits, 3 exit;
    void Awake()
    {
        controls = GameManager.PlayerInput;
        controls.MainMenu.Up.performed += ctx => InputUp();
        controls.MainMenu.Down.performed += ctx => InputDown();
        controls.MainMenu.Accept.performed += ctx => InputAccept();
        controls.MainMenu.Cancel.performed += ctx => InputCancel();
    }

    void Start()
    {
        MenuItems[0] = GameObject.Find("Highlighter1");
        MenuItems[1] = GameObject.Find("Highlighter2");
        MenuItems[2] = GameObject.Find("Highlighter3");
        MenuItems[3] = GameObject.Find("Highlighter4");
        DeselectAll();
    }
    void Update()
    {
    
    }
    void InputUp()
    {
        DeselectAll();
        if (Selection == 0) Selection = 3;
        else
            Selection--;
        ActivateSelection(Selection);        
    }
    void InputDown()
    {
        DeselectAll();
        if (Selection == 3) Selection = 0;
        else
            Selection++;
        ActivateSelection(Selection);
    }
    void InputAccept()
    {
        if(Selection == 0)
        {
            SceneManager.LoadScene(1);
        }
    }
    void InputCancel()
    {
      
    }

    void DeselectAll()
    {
        for(int i = 0; i<MenuItems.Length; i++)
        {
            MenuItems[i].SetActive(false);
        }
    }
    void ActivateSelection(int Selection)
    {
        MenuItems[Selection].SetActive(true);
    }

    void OnEnable()
    {
        controls.MainMenu.Enable();
    }
    void OnDisable()
    {
        controls.MainMenu.Disable();
    }


    
}
