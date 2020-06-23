using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnProjectilesScript : MonoBehaviour {

    public float delay;
    public Camera cam;
    public RotateToMouseScript rotateToMouse;
    public GameObject firePoint;
    public List<GameObject> VFXs = new List<GameObject> ();

	private int count = 0;
	private float timeToFire = 0f;
	private GameObject effectToSpawn;
    private Animator anim;
    private ProjectileMoveScript currentProjectileScript;
    private MovementInput movementInput;

    void Start () {
        
        anim = GetComponent<Animator>();
        movementInput = GetComponent<MovementInput>();

        if (cam != null && rotateToMouse != null)
        {
            rotateToMouse.SetCamera(cam);
            rotateToMouse.StartUpdateRay();
        }
        else
            Debug.Log("Please assign a camera and the RotateToMouse script from the fire point");

        if (VFXs.Count > 0)
        {
            effectToSpawn = VFXs[0];
            currentProjectileScript = effectToSpawn.GetComponent<ProjectileMoveScript>();
            timeToFire = currentProjectileScript.fireRate;
        }
        else
            Debug.Log("Please assign one or more VFXs in inspector");       
    }

	void Update ()
    {
		if (Input.GetMouseButton (0) && Time.time >= timeToFire)
        {
            anim.SetTrigger("Attack01");
			timeToFire = Time.time + 1f / currentProjectileScript.fireRate;
			StartCoroutine(SpawnVFX ());	
		}

		if (Input.GetKeyDown (KeyCode.Alpha1))
			Next ();

		if (Input.GetKeyDown (KeyCode.Alpha2)) 
			Previous ();	
	}

	IEnumerator SpawnVFX ()
    {
        movementInput.StopMovementTemporarily (delay + 0.5f, false);

        yield return new WaitForSeconds(delay);

		GameObject vfx;

		if (firePoint != null)
        {
			vfx = Instantiate (effectToSpawn, firePoint.transform.position, Quaternion.identity);

            if (rotateToMouse != null)
            {
                vfx.transform.localRotation = rotateToMouse.GetRotation();
            }
        }
		else
			vfx = Instantiate (effectToSpawn);		
	}

	public void Next ()
    {
		count++;

		if (count > VFXs.Count)
			count = 0;

		for(int i = 0; i < VFXs.Count; i++)
        {
            if (count == i)
            {
                effectToSpawn = VFXs[i];
                currentProjectileScript = effectToSpawn.GetComponent<ProjectileMoveScript>();
                timeToFire = currentProjectileScript.fireRate;
            }
		}
	}

	public void Previous ()
    {
		count--;

		if (count < 0)
			count = VFXs.Count;

		for (int i = 0; i < VFXs.Count; i++)
        {
            if (count == i)
            {
                effectToSpawn = VFXs[i];
                currentProjectileScript = effectToSpawn.GetComponent<ProjectileMoveScript>();
                timeToFire = currentProjectileScript.fireRate;
            }
		}
	}
}
