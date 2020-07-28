using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FMODScript : MonoBehaviour
{
    [FMODUnity.EventRef]
    public string GearPickupString;
    
    public FMOD.Studio.EventInstance GearPickup;

    private Collider playerCollider;

    [SerializeField] int _gearnumber = 0;
    public FMOD.Studio.PARAMETER_ID GearNumberId;

    private void Start()
    {
        GearPickup = FMODUnity.RuntimeManager.CreateInstance(GearPickupString);
        playerCollider = GetComponent<BoxCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "GearPickup")
        {
            
            GearPickup.setParameterByName("Gear Number", _gearnumber);
            GearPickup.start();

            //FMODUnity.RuntimeManager.PlayOneShot(, GetComponent<Transform>().position);

            Debug.LogError(_gearnumber);

            _gearnumber++;

            if (_gearnumber == 5)
            {
                _gearnumber = 0;
            }
        }
    }
}
