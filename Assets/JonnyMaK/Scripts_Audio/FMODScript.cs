using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FMODScript : MonoBehaviour
{
    [FMODUnity.EventRef]
    public string GearPickupString;
    
    public FMOD.Studio.EventInstance GearPickup0;
    public FMOD.Studio.EventInstance GearPickup1;
    public FMOD.Studio.EventInstance GearPickup2;
    public FMOD.Studio.EventInstance GearPickup3;
    public FMOD.Studio.EventInstance GearPickup4;

    private Collider playerCollider;

    [SerializeField] int _gearnumber = 0;
    public FMOD.Studio.PARAMETER_ID GearNumberId;

    private void Start()
    {
        GearPickup0 = FMODUnity.RuntimeManager.CreateInstance(GearPickupString);
        GearPickup1 = FMODUnity.RuntimeManager.CreateInstance(GearPickupString);
        GearPickup2 = FMODUnity.RuntimeManager.CreateInstance(GearPickupString);
        GearPickup3 = FMODUnity.RuntimeManager.CreateInstance(GearPickupString);
        GearPickup4 = FMODUnity.RuntimeManager.CreateInstance(GearPickupString);
        playerCollider = GetComponent<BoxCollider>();
        GearPickup0.setParameterByName("Gear Number", 0);
        GearPickup1.setParameterByName("Gear Number", 1);
        GearPickup2.setParameterByName("Gear Number", 2);
        GearPickup3.setParameterByName("Gear Number", 3);
        GearPickup4.setParameterByName("Gear Number", 4);


    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("gearpickup");
        if (other.tag == "GearPickup")

            
        {
            switch(_gearnumber)
                
            {
                case 0:
                    {
                        GearPickup0.start();
                        break;
                    }
                case 1:
                    {
                        GearPickup1.start();
                        break;
                    }
                case 2:
                    {
                        GearPickup2.start();
                        break;
                    }
                case 3:
                    {
                        GearPickup3.start();
                        break;
                    }
                case 4:
                    {
                        GearPickup4.start();
                        break;
                    }
                default:
                    break;
            }

            //FMODUnity.RuntimeManager.PlayOneShot(, GetComponent<Transform>().position);

            //Debug.LogError(_gearnumber);

            _gearnumber++;

            if (_gearnumber == 5)
            {
                _gearnumber = 0;
            }
        }
    }
}
