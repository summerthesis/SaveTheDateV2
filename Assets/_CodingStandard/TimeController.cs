using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class TimeController : MonoBehaviour
{
    PlayerInputAction controls;
    private float Energy;
    private float MaxEnergy = 1000;
    private float MaxCastRange = 1000;
    public float EnergyCost = 9;
    public float StopCost = 750;
    private int Count;  
    private GameObject[] TimeTaggedObjects;
    private GameObject Player;
    //private GameObject EnergyBar;
    private bool Stopping;

    // Added by Shu Deng (Mike)
    public EnergyBarController EnergyBarController;
    
    private enum TimeStates
    {
        Available,
        Slowing, 
        FastForwarding
    }
    private TimeStates TimeState = TimeStates.Available;
    void Awake()
    {
        SetupControls();
        Player = GameObject.FindGameObjectWithTag("Player");
        //EnergyBar = GameObject.FindGameObjectWithTag("EnergyBar");
    }

    void Update()
    {
        switch (TimeState)
        {
            case TimeStates.Available:
                Energy += 4; if (Energy > MaxEnergy) Energy = MaxEnergy;
                SetEnergyBarScale();
                break;

            case TimeStates.Slowing:
                Energy -= EnergyCost; if (Energy < 0) { Energy = 0; EndSlow(); }
                SetEnergyBarScale();
                break;

            case TimeStates.FastForwarding:
                Energy -= EnergyCost; if (Energy < 0) { Energy = 0; EndFastForward(); }
                SetEnergyBarScale();
                break;

            default:
                break;
        }
        if(Stopping)
        {
            Count++; 
            if(Count > 120) { Stopping = false; Count = 0; LoopThroughObjects("RestoreToNormal", false); }
        }
    }
    void LoopThroughObjects(string SendThisMessage, bool CheckDistance)
    {
        TimeTaggedObjects = GameObject.FindGameObjectsWithTag("TimeInteractable");

        if (TimeTaggedObjects.Length > 0)
            foreach (GameObject obj in TimeTaggedObjects)
            {
            float distance = Vector3.Distance(Player.transform.position, obj.transform.position);
            if (CheckDistance)
            {
                if (distance < MaxCastRange)
                obj.gameObject.SendMessage(SendThisMessage);
            }
            else
                obj.gameObject.SendMessage(SendThisMessage);
            }
    }


    void Slow()
    {
        if(Energy >= EnergyCost)
        {
            LoopThroughObjects("TimeSlow", true);
            TimeState = TimeStates.Slowing;
        }
        else
        {
            LoopThroughObjects("RestoreToNormal", false);
            TimeState = TimeStates.Available;
        }
    }

    void Stop()
    {
        if (Energy >= StopCost)
        {
            Stopping = true;
            LoopThroughObjects("TimeStop", true);
            Energy -= StopCost;
            SetEnergyBarScale();
        }
    }

    void FastForward()
    {
        if(Energy >= EnergyCost)
        {
            LoopThroughObjects("TimeFastForward", true);
            TimeState = TimeStates.FastForwarding;
        }
        else
        {
            LoopThroughObjects("RestoreToNormal", false);
            TimeState = TimeStates.Available;
        }
    }
    void JumpForward()
    {
        if (Energy >= StopCost)
        {
            LoopThroughObjects("JumpForward", true);
            Energy -= StopCost;
            SetEnergyBarScale();
        } 
    }
    void EndSlow()
    {
        LoopThroughObjects("RestoreToNormal", true);
        TimeState = TimeStates.Available;
    }
    void EndFastForward()
    {
        LoopThroughObjects("RestoreToNormal", false);
        TimeState = TimeStates.Available;       
    }
    void SetupControls()
    {
        controls = new PlayerInputAction();
        controls.TimeControls.TimeFastForward.performed += ctx => FastForward();
        controls.TimeControls.TimeFastForward.canceled += ctx => EndFastForward();
        controls.TimeControls.TimeSlow.performed += ctx => Slow();
        controls.TimeControls.TimeSlow.canceled += ctx => EndSlow();
        
        controls.TimeControls.TimeJumpForward.canceled += ctx => JumpForward();
        
        controls.TimeControls.TimeStop.canceled += ctx => Stop();
    }
    void SetEnergyBarScale()
    {
        float EnergyBarScale = Energy / MaxEnergy;
        //EnergyBar.transform.localScale = new Vector3(EnergyBarScale, 1, 1);

        // Added by Shu Deng (Mike)
        EnergyBarController.UpdateEnergyBar(EnergyBarScale);
    }
    private void OnEnable() { controls.Enable(); }
    private void OnDisable() { controls.Disable(); }
}
