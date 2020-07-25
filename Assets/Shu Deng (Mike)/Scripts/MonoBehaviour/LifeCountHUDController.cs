using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeCountHUDController : MonoBehaviour
{
    public Transform lifeCountUI;
    public AnimationCurve lifeCountIconScaling;
    public int initialNumberOfLives = 3;

    private Transform[] m_LifeCountIcons = new Transform[9];
    private int m_NumOfLifes;
    private float m_Time = 0, m_AnimationLength;

    // Start is called before the first frame update
    void Awake()
    {
        for (int i = 0; i < 9; ++i)
        {
            m_LifeCountIcons[i] = lifeCountUI.GetChild(i);
        }
        m_NumOfLifes = initialNumberOfLives;
        m_LifeCountIcons[m_NumOfLifes - 1].gameObject.SetActive(true);
        m_AnimationLength = lifeCountIconScaling.keys[lifeCountIconScaling.length - 1].time;
    }

    public void GainedLife()
    {
        if (++m_NumOfLifes > 9)
        {
            m_NumOfLifes = 9;
        }
        m_LifeCountIcons[m_NumOfLifes - 1].gameObject.SetActive(true);
        m_LifeCountIcons[m_NumOfLifes - 2].gameObject.SetActive(false);
        StartCoroutine(OnGainedLife());
    }

    private IEnumerator OnGainedLife()
    {
        float value;
        while (m_Time < m_AnimationLength)
        {
            value = lifeCountIconScaling.Evaluate(m_Time);
            m_LifeCountIcons[m_NumOfLifes - 1].localScale = new Vector3(value, value, value);
            m_Time += Time.deltaTime;
            yield return null;
        }
        m_Time = m_AnimationLength;
        value = lifeCountIconScaling.Evaluate(m_Time);
        m_LifeCountIcons[m_NumOfLifes - 1].localScale = new Vector3(value, value, value);
        m_Time = 0;
    }
}
