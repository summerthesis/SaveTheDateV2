using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadingCurtainController : MonoBehaviour
{
    public float fadeInSpeed = 0.5f, fadeOutSpeed = 0.25f;
    public bool FinishedFading
    {
        get;
        private set;
    }
    private Image m_CurtainImage;

    // Start is called before the first frame update
    void Awake()
    {
        m_CurtainImage = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CurtainFadeIn()
    {
        FinishedFading = false;
        StartCoroutine(FadeIn());
    }

    public void CurtainFadeOut()
    {
        FinishedFading = false;
        StartCoroutine(FadeOut());
    }

    private IEnumerator FadeIn()
    {
        Color newColor = new Color(0, 0, 0, 0);
        while ((newColor.a += fadeInSpeed * Time.deltaTime) < 1f)
        {
            m_CurtainImage.color = newColor;
            yield return null;
        }
        newColor.a = 1f;
        m_CurtainImage.color = newColor;
        FinishedFading = true;
    }

    private IEnumerator FadeOut()
    {
        Color newColor = new Color(0, 0, 0, 1);
        while ((newColor.a -= fadeInSpeed * Time.deltaTime) > 0f)
        {
            m_CurtainImage.color = newColor;
            yield return null;
        }
        newColor.a = 0f;
        m_CurtainImage.color = newColor;
        FinishedFading = true;
    }
}
