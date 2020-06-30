using System.Collections;
using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class TimeController : MonoBehaviour
{
    PlayerInputAction controls;
    private float Energy;
    private float MaxEnergy = 1000;
    private float MaxCastRange = 15;
    public float EnergyCost = 9;
    public float StopCost = 750;
    private int Count;  
    private GameObject[] TimeTaggedObjects;
    private GameObject Player;
    //private GameObject EnergyBar;
    private bool Stopping;

    private enum TimeStates
    {
        Unassigned,
        Available,
        Slowing,
        FastForwarding
    }
    private TimeStates TimeState = TimeStates.Available;

    // Added by Shu Deng (Mike)
    public EnergyBarController EnergyBarController;
    [ColorUsage(true, true)]
    public Color HighlightFastforward, HighlightNeutral, HighlightPause, HighlightSlow;
    private int ShaderIDColor, ShaderIDIsTwinkling, ShaderIDIsHighlighted;
    private GameObject[] AllTimeTaggedObjects;    
    
    void Awake()
    {
        SetupControls();
        Player = GameObject.FindGameObjectWithTag("Player");        

        // Added by Shu Deng (Mike)
        AllTimeTaggedObjects = GameObject.FindGameObjectsWithTag("TimeInteractable");
        ShaderIDColor = Shader.PropertyToID("Color_5D1C9DC");
        ShaderIDIsTwinkling = Shader.PropertyToID("Boolean_CFDDD5C1");
        ShaderIDIsHighlighted = Shader.PropertyToID("Boolean_82F39996");
    }

    void Update()
    {
        // Added by Shu Deng (Mike)
        TimeTaggedObjects = FindObjectsWithinRange().ToArray();

        switch (TimeState)
        {
            case TimeStates.Available:
                Energy += 4; if (Energy > MaxEnergy) Energy = MaxEnergy;
                SetEnergyBarScale();

                // Added by Shu Deng (Mike)
                ChangeHightlightColor(HighlightNeutral, 1);
                break;

            case TimeStates.Slowing:
                Energy -= EnergyCost; if (Energy < 0) { Energy = 0; EndSlow(); }
                SetEnergyBarScale();

                // Added by Shu Deng (Mike)
                ChangeHightlightColor(HighlightSlow, 0);
                break;

            case TimeStates.FastForwarding:
                Energy -= EnergyCost; if (Energy < 0) { Energy = 0; EndFastForward(); }
                SetEnergyBarScale();

                // Added by Shu Deng (Mike)
                ChangeHightlightColor(HighlightFastforward, 0);
                break;

            default:
                break;
        }
        if(Stopping)
        {
            Count++; 
            if(Count > 120) 
            { 
                Stopping = false; 
                Count = 0; 
                LoopThroughObjects("RestoreToNormal", false);
                TimeState = TimeStates.Available;
            }

            // Added by Shu Deng (Mike)
            ChangeHightlightColor(HighlightPause, 0);
        }
    }
    void LoopThroughObjects(string SendThisMessage, bool CheckDistance)
    {
        if (TimeTaggedObjects.Length > 0)
            foreach (GameObject obj in TimeTaggedObjects)
            {
                /*
                float distance = Vector3.Distance(Player.transform.position, obj.transform.position);
                if (CheckDistance)
                {
                    if (distance < MaxCastRange)
                    obj.gameObject.SendMessage(SendThisMessage);
                }
                else
                    obj.gameObject.SendMessage(SendThisMessage);
                */

                // Added by Shu Deng (Mike)
                obj.SendMessage(SendThisMessage);
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
        // controls = new PlayerInputAction();

        // Added by Shu Deng (Mike)
        controls = InputManagerSingleton.Instance;

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
        
        // Added by Shu Deng (Mike)
        EnergyBarController.UpdateEnergyBar(EnergyBarScale);
    }
    private void OnEnable() { controls.Enable(); }
    private void OnDisable() { controls.Disable(); }

    // Added by Shu Deng (Mike), needs space querying optimization in the future
    IEnumerable<GameObject> FindObjectsWithinRange()
    {
        foreach (var member in AllTimeTaggedObjects)
        {
            if (Vector3.Distance(Player.transform.position, member.transform.position) < MaxCastRange)
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
                            material.SetFloat(ShaderIDIsHighlighted, 0);
                        }
                    }
                }
            }
        }
    }

    // Added by Shu Deng (Mike)
    void ChangeHightlightColor(Color destinationColor, int isTwinkling)
    {
        foreach (GameObject obj in TimeTaggedObjects)
        {
            MeshRenderer[] meshRenderers = obj.GetComponentsInChildren<MeshRenderer>();
            if (meshRenderers != null)
            {
                foreach (var meshRenderer in meshRenderers)
                {
                    Material[] materials = meshRenderer.materials;
                    foreach (var material in materials)
                    {
                        material.SetFloat(ShaderIDIsHighlighted, 1);
                        material.SetColor(ShaderIDColor, destinationColor);
                        material.SetInt(ShaderIDIsTwinkling, isTwinkling);
                    }
                }
            }
        }
    }
}
