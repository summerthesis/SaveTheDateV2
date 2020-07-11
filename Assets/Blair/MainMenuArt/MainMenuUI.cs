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
        DeselectAll();
        ActivateSelection(0);  
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
        PlaySoundOneShot("event:/UI/Up");
    }
    void InputDown()
    {
        DeselectAll();
        if (Selection == 3) Selection = 0;
        else
            Selection++;
        ActivateSelection(Selection);
        PlaySoundOneShot("event:/UI/Down");
    }
    void InputAccept()
    {
        if(Selection == 0)
        {
            SceneManager.LoadScene(1);
            PlaySoundOneShot("event:/UI/UI_Forward");
        }
    }
    void InputCancel()
    {
        PlaySoundOneShot("event:/UI/UI_Back");
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

    void PlaySoundOneShot(string path)
    {
        FMODUnity.RuntimeManager.PlayOneShot(path, GetComponent<Transform>().position);
    }

}
