using System.Collections;
using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{

    public GameObject[] MenuItems;
    public GameObject Menu;
    public PlayerInputAction controls;
    public bool GameIsPaused = false;
    

    private int Selection;//0 new, 1 options, 2 credits, 3 exit;
    void Awake()
    {
        controls = GameManager.PlayerInput;
        controls.MainMenu.Up.performed += ctx => InputUp();
        controls.MainMenu.Down.performed += ctx => InputDown();
        controls.MainMenu.Accept.performed += ctx => InputAccept();
        controls.MainMenu.Cancel.performed += ctx => InputCancel();
        controls.MainMenu.StartButton.performed += ctx => StartPushed();
    }

    void Start()
    {
        DeselectAll();
        ActivateSelection(0);
    }
    void Update()
    {

    }

    void StartPushed()
    { 
        if(GameIsPaused)
        {
            Debug.Log("Attempted to close menu with startbutton");
            CloseMenu();
            return;
        }
        if(!GameIsPaused)
        {
            OpenMenu();
            return;
        }
    }

    void CloseMenu()
    {
        GameIsPaused = false;
        Menu.SetActive(false);
        Time.timeScale = 1f;
    }

    void OpenMenu()
    {
        GameIsPaused = true;
        Menu.SetActive(true);
        Time.timeScale = 0f;
        Selection = 0;
        DeselectAll();
        ActivateSelection(0);
    }

    void InputUp()
    {
        if (!GameIsPaused) return;
        DeselectAll();
        if (Selection == 0) Selection = 2;
        else
            Selection--;
        ActivateSelection(Selection);
        PlaySoundOneShot("event:/UI/Up");
    }
    void InputDown()
    {
        if (!GameIsPaused) return;
        DeselectAll();
        if (Selection == 2) Selection = 0;
        else
            Selection++;
        ActivateSelection(Selection);
        PlaySoundOneShot("event:/UI/Down");
    }
    void InputAccept()
    {
        if (!GameIsPaused) return;
        if (Selection == 0)
        {
            CloseMenu();
        }
    }
    void InputCancel()
    {
        if (!GameIsPaused) return;
        PlaySoundOneShot("event:/UI/UI_Back");
        if (GameIsPaused)
        {
            CloseMenu();
        }
    }

    void DeselectAll()
    {
        if (!GameIsPaused) return;
        for (int i = 0; i < MenuItems.Length; i++)
        {
            MenuItems[i].SetActive(false);
        }
    }
    void ActivateSelection(int Selection)
    {
        if (!GameIsPaused) return;
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

    void PlaySoundOneShot(string path)
    {
        FMODUnity.RuntimeManager.PlayOneShot(path, GetComponent<Transform>().position);
    }

}
