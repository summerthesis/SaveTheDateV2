using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.Events;

public class CameraShakeSimpleScript : MonoBehaviour {

	private bool isRunning = false;
	private Animation anim;
    private CinemachineFreeLook freeCamera;
    private CinemachineBasicMultiChannelPerlin freeCameraNoise;
    private float shakeElapsedTime = 0;
    private float originalAmp;
    private float originalFreq;
    private float ampSubtract;
    private float freqSubtract;

    void Start () {
        freeCamera = GetComponent<CinemachineFreeLook>();
        if (freeCamera != null)
        {
            freeCameraNoise = freeCamera.GetRig(1).GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
            originalAmp = freeCameraNoise.m_AmplitudeGain;
            originalFreq = freeCameraNoise.m_FrequencyGain;
        }
        anim = GetComponent<Animation> ();
	}

    //Animation Shake
	public void ShakeCamera() {	
		if (anim != null)
			anim.Play (anim.clip.name);
		else
			ShakeCaller (0.25f, 0.1f);
	}

	//Transform shake
	public void ShakeCaller (float amount, float duration){
		StartCoroutine (Shake(amount, duration));
	}

	IEnumerator Shake (float amount, float duration){
		isRunning = true;

		Vector3 originalPos = transform.localPosition;
		int counter = 0;

		while (duration > 0.01f) {
			counter++;

			var x = Random.Range (-1f, 1f) * (amount/counter);
			var y = Random.Range (-1f, 1f) * (amount/counter);

			transform.localPosition = Vector3.Lerp (transform.localPosition, new Vector3 (originalPos.x + x, originalPos.y + y, originalPos.z), 0.5f);

			duration -= Time.deltaTime;
			
			yield return new WaitForSeconds (0.1f);
		}

		transform.localPosition = originalPos;

		isRunning = false;
	}

    //Cinemachine shake
    public void ShakeCameraCine(float duration, float amp, float freq) {
        shakeElapsedTime = duration;
        ampSubtract = (amp - originalAmp) / (duration / 0.1f);
        freqSubtract = (freq - originalFreq) / (duration / 0.1f);
        StartCoroutine(ShakeCameraCineCo(amp, freq));
    }

    IEnumerator ShakeCameraCineCo(float shakeAmp, float shakeFreq) {
        if (freeCamera != null && freeCameraNoise != null)
        {
            while (shakeElapsedTime > 0) {
                freeCameraNoise.m_AmplitudeGain = shakeAmp;
                freeCameraNoise.m_FrequencyGain = shakeFreq;
                
                shakeElapsedTime -= Time.deltaTime;
                if (shakeAmp > originalAmp)
                    shakeAmp -= ampSubtract;
                else
                    shakeAmp = originalAmp;

                if (shakeFreq > originalFreq)
                    shakeFreq -= freqSubtract;
                else
                    shakeFreq = originalFreq;

                yield return new WaitForSeconds(0.1f);
            }

            freeCameraNoise.m_AmplitudeGain = originalAmp;
            freeCameraNoise.m_FrequencyGain = originalFreq;
            shakeElapsedTime = 0f;
        }
    }
}
