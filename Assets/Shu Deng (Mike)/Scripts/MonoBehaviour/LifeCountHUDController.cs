using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeCountHUDController : MonoBehaviour
{
    public AnimationCurve lifeCountIconScaling;
    public int initialNumberOfLives = 3;
    private Transform[] m_LifeCountIcons = new Transform[5];
    private int m_NumOfLifes;
    private float m_Time = 0;
    private enum State
    {
        Normal,
        GainedLife
    }
    private State m_State = State.Normal;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 5; ++i)
        {
            m_LifeCountIcons[i] = transform.Find("Life Counts").GetChild(i);
        }
        m_NumOfLifes = initialNumberOfLives;
        m_LifeCountIcons[m_NumOfLifes - 1].gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GainedLife()
    {
        if (++m_NumOfLifes > 5)
        {
            m_NumOfLifes = 5;
        }
        m_State = State.GainedLife;
        m_LifeCountIcons[m_NumOfLifes - 1].gameObject.SetActive(true);
        m_LifeCountIcons[m_NumOfLifes - 2].gameObject.SetActive(false);
        StartCoroutine(OnGainedLife());
    }

    private IEnumerator OnGainedLife()
    {
        while (m_Time < 1f)
        {
            float value = lifeCountIconScaling.Evaluate(m_Time);
            m_LifeCountIcons[m_NumOfLifes - 1].localScale = new Vector3(value, value, value);
            m_Time += Time.deltaTime;
            yield return null;
        }           
    }
}
