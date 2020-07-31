﻿using System.Collections;
using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;

public class TimeController : MonoBehaviour
{
    [FMODUnity.EventRef]
    public string SlowSound = "event:/Characters/Player/Ability/Slow_Time";
    FMOD.Studio.EventInstance SlowSoundEvent;

    public float slowEnergyCostRate = 9,
        fastforwardEnergyCostRate = 9,
        energyReplenishRate = 10,  
        maxEnergy = 500, 
        maxCastRange = 15;
    [ColorUsage(false, true)]
    public Color highlightFastforward, 
        highlightNeutral,  
        highlightSlow;

    private float m_Energy = 0;
    private GameObject[] m_AllTimeTaggedObjects, 
        m_TimeTaggedObjects;
    private GameObject m_OTimeVfx;
    private OutlineCustomPass m_OutlineCustomPassVolume;
    private PlayerInputAction m_Controls;
    private EnergyBarController m_EnergyBarController;
    private enum TimeStates
    {
        Available,
        Slowing,
        FastForwarding,
    }
    private TimeStates m_TimeState = TimeStates.Available;

    void Awake()
    {
        SetupControls();
        m_EnergyBarController = GameObject.FindGameObjectWithTag("HUD").
            GetComponentInChildren<EnergyBarController>();
        m_AllTimeTaggedObjects = GameObject.FindGameObjectsWithTag("TimeInteractable");
        m_OutlineCustomPassVolume = (OutlineCustomPass)GameObject.Find("Custom Pass Volume").
            GetComponent<CustomPassVolume>().customPasses[0];
        m_OTimeVfx = GameObject.Find("TimeVfx");
        SlowSoundEvent = FMODUnity.RuntimeManager.CreateInstance(SlowSound);
    }

    void Update()
    {
        m_TimeTaggedObjects = FindObjectsWithinRange().ToArray();

        switch (m_TimeState)
        {
            case TimeStates.Available:
                m_Energy += energyReplenishRate * Time.deltaTime;
                if (m_Energy > maxEnergy)
                {
                    m_Energy = maxEnergy;
                }
                SetEnergyBarScale();
                ApplyTimeControlEffect("RestoreToNormal", highlightNeutral);
                break;

            case TimeStates.Slowing:
                m_Energy -= slowEnergyCostRate * Time.deltaTime; 
                if (m_Energy < 0) 
                { 
                    m_Energy = 0; 
                    EndSlow(); 
                }
                else
                {
                    ApplyTimeControlEffect("TimeSlow", highlightSlow);
                }
                SetEnergyBarScale();                
                break;

            case TimeStates.FastForwarding:
                m_Energy -= fastforwardEnergyCostRate * Time.deltaTime; 
                if (m_Energy < 0) 
                { 
                    m_Energy = 0; 
                    EndFastForward(); 
                }
                else
                {
                    ApplyTimeControlEffect("TimeFastForward", highlightFastforward);
                }
                SetEnergyBarScale();                
                break;

            default:
                break;
        }
    }

    private void Slow()
    {
        if(m_Energy >= slowEnergyCostRate)
        {            
            m_TimeState = TimeStates.Slowing;
        }
        else
        {            
            m_TimeState = TimeStates.Available;
        }
    }

    private void EndSlow()
    {        
        m_TimeState = TimeStates.Available;
        SlowSoundEvent.setParameterByName("SlowTimeEnd", 1.0f, true);
    }
    
    private void FastForward()
    {
        if(m_Energy >= fastforwardEnergyCostRate)
        {            
            m_TimeState = TimeStates.FastForwarding;
        }
        else
        {            
            m_TimeState = TimeStates.Available;
        }
    }

    private void EndFastForward()
    {        
        m_TimeState = TimeStates.Available;
    }

    private void SetupControls()
    {
        m_Controls = GameManager.PlayerInput;
        m_Controls.TimeControls.TimeFastForward.performed += ctx => FastForward();
        m_Controls.TimeControls.TimeFastForward.started += ctx => SendVfxFast();
        m_Controls.TimeControls.TimeFastForward.canceled += ctx => EndFastForward();
        m_Controls.TimeControls.TimeSlow.performed += ctx => Slow();
        m_Controls.TimeControls.TimeSlow.started += ctx => SendVfxSlow();
        m_Controls.TimeControls.TimeSlow.canceled += ctx => EndSlow();
    }
    private void SendVfxSlow()
    {
        m_OTimeVfx.SendMessage("Slow");
        SlowSoundEvent.setParameterByName("SlowTimeEnd", 0.0f, true);
        SlowSoundEvent.start();
    }
    private void SendVfxFast()
    {
        m_OTimeVfx.SendMessage("Fast");
        FMODUnity.RuntimeManager.PlayOneShot("event:/Characters/Player/Ability/Speed_Time");
    }
    private void SetEnergyBarScale()
    {
        float EnergyBarScale = m_Energy / maxEnergy;
        m_EnergyBarController.UpdateEnergyBar(EnergyBarScale);
    }

    private IEnumerable<GameObject> FindObjectsWithinRange()
    {
        foreach (var member in m_AllTimeTaggedObjects)
        {
            if (Vector3.Distance(transform.position, member.transform.position) < maxCastRange)
            {
                yield return member;
            }
            else
            {
                member.layer = 0;
                member.SendMessage("RestoreToNormal", SendMessageOptions.DontRequireReceiver);
            }
        }
    }

    private void ApplyTimeControlEffect(string message, Color destinationColor)
    {
        foreach (GameObject obj in m_TimeTaggedObjects)
        {
            obj.layer = 11;
            obj.SendMessage(message, SendMessageOptions.DontRequireReceiver);
        }

        m_OutlineCustomPassVolume.outlineColor = destinationColor;
    }
    void PlaySoundOneShot(string path)
    {
        FMODUnity.RuntimeManager.PlayOneShot(path, GetComponent<Transform>().position);
    }
}
