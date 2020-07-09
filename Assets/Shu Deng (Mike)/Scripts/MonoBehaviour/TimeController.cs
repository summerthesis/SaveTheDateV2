using System.Collections;
using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class TimeController : MonoBehaviour
{
    [FMODUnity.EventRef]
    public string SlowSound = "event:/Characters/Player/Ability/Slow_Time";
    FMOD.Studio.EventInstance SlowSoundEvent;

    public float slowEnergyCostRate = 9,
        fastforwardEnergyCostRate = 9,
        stopEnergyCost = 100,
        stopDuration = 5,
        energyReplenishRate = 10,  
        maxEnergy = 500, 
        maxCastRange = 15;
    [ColorUsage(true, true)]
    public Color highlightFastforward, 
        highlightNeutral, 
        highlightPause, 
        highlightSlow;

    private int m_ShaderIDColor, 
        m_ShaderIDIsTwinkling, 
        m_ShaderIDIsHighlighted;
    private float m_Energy = 0, m_StopTimer = 0;
    private GameObject[] m_AllTimeTaggedObjects, 
        m_TimeTaggedObjects;
    private GameObject oTimeVfx;
    private PlayerInputAction m_Controls;
    private EnergyBarController m_EnergyBarController;
    private enum TimeStates
    {
        Available,
        Slowing,
        FastForwarding,
        Stop
    }
    private TimeStates m_TimeState = TimeStates.Available;

    void Awake()
    {
        SetupControls();
        m_EnergyBarController = GameObject.FindGameObjectWithTag("HUD").GetComponentInChildren<EnergyBarController>();
        m_AllTimeTaggedObjects = GameObject.FindGameObjectsWithTag("TimeInteractable");
        m_ShaderIDColor = Shader.PropertyToID("_FresnelColour");
        m_ShaderIDIsTwinkling = Shader.PropertyToID("_IsTwinkling");
        m_ShaderIDIsHighlighted = Shader.PropertyToID("_IsHighlighted");
        oTimeVfx = GameObject.Find("TimeVfx");
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
                ApplyTimeControlEffect("RestoreToNormal", highlightNeutral, 1);
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
                    ApplyTimeControlEffect("TimeSlow", highlightSlow, 0);
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
                    ApplyTimeControlEffect("TimeFastForward", highlightFastforward, 0);
                }
                SetEnergyBarScale();                
                break;

            case TimeStates.Stop:                
                m_Energy += energyReplenishRate * Time.deltaTime;
                if (m_Energy > maxEnergy)
                {
                    m_Energy = maxEnergy;
                }
                SetEnergyBarScale();
                m_StopTimer += Time.deltaTime;
                if (m_StopTimer > stopDuration)
                {                   
                    m_TimeState = TimeStates.Available;
                }
                else
                {
                    ApplyTimeControlEffect("TimeStop", highlightPause, 0);
                }                
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

    void Stop()
    {
        if (m_Energy >= stopEnergyCost)
        {
            m_TimeState = TimeStates.Stop;            
            m_Energy -= stopEnergyCost;
            SetEnergyBarScale();
            m_StopTimer = 0;
        }
    }

    void JumpForward()
    {
        
    }
    
    private void SetupControls()
    {
        m_Controls = InputManagerSingleton.Instance;
        m_Controls.TimeControls.TimeFastForward.performed += ctx => FastForward();
        m_Controls.TimeControls.TimeFastForward.started += ctx => SendVfxFast();
        m_Controls.TimeControls.TimeFastForward.canceled += ctx => EndFastForward();
        m_Controls.TimeControls.TimeSlow.performed += ctx => Slow();
        m_Controls.TimeControls.TimeSlow.started += ctx => SendVfxSlow();
        m_Controls.TimeControls.TimeSlow.canceled += ctx => EndSlow();        
        m_Controls.TimeControls.TimeJumpForward.canceled += ctx => JumpForward();        
        m_Controls.TimeControls.TimeStop.canceled += ctx => Stop();
        m_Controls.TimeControls.TimeStop.canceled += ctx => SendVfxStop();
    }
    private void SendVfxSlow()
    {
        oTimeVfx.SendMessage("Slow");
        SlowSoundEvent.setParameterByName("SlowTimeEnd", 0.0f, true);
        SlowSoundEvent.start();
    }
    private void SendVfxStop()
    {
        oTimeVfx.SendMessage("Stop");
        PlaySoundOneShot("event:/Characters/Player/Ability/Pause Time");
    }
    private void SendVfxFast()
    {
        oTimeVfx.SendMessage("Fast");
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
                MeshRenderer[] meshRenderers = member.GetComponentsInChildren<MeshRenderer>();
                if (meshRenderers != null)
                {
                    foreach (var meshRenderer in meshRenderers)
                    {
                        Material[] materials = meshRenderer.materials;
                        foreach (var material in materials)
                        {
                            material.SetFloat(m_ShaderIDIsHighlighted, 0);
                        }
                    }
                }
                member.SendMessage("RestoreToNormal");
            }
        }
    }

    private void ApplyTimeControlEffect(string message, Color destinationColor, int isTwinkling)
    {
        foreach (GameObject obj in m_TimeTaggedObjects)
        {
            MeshRenderer[] meshRenderers = obj.GetComponentsInChildren<MeshRenderer>();
            if (meshRenderers != null)
            {
                foreach (var meshRenderer in meshRenderers)
                {
                    Material[] materials = meshRenderer.materials;
                    foreach (var material in materials)
                    {
                        material.SetFloat(m_ShaderIDIsHighlighted, 1);
                        material.SetColor(m_ShaderIDColor, destinationColor);
                        material.SetInt(m_ShaderIDIsTwinkling, isTwinkling);
                    }
                }
            }
            obj.SendMessage(message);
        }
    }
    void PlaySoundOneShot(string path)
    {
        FMODUnity.RuntimeManager.PlayOneShot(path, GetComponent<Transform>().position);
    }
}
