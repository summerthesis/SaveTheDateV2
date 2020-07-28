using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeCountHUDController : MonoBehaviour
{
    public Transform lifeCountUI;
    public AnimationCurve lifeCountIconScaling;
    public int initialNumberOfLives = 3;

    private Transform[] m_LifeCountIcons = new Transform[9];
    private int m_NumOfLives;
    private float m_Time = 0, m_AnimationLength;
    private enum State
    {
        ILDE,
        SCALING_NUMBER
    }
    private State m_State = State.ILDE;

    void Awake()
    {
        for (int i = 0; i < 9; ++i)
        {
            m_LifeCountIcons[i] = lifeCountUI.GetChild(i);
        }
        m_NumOfLives = initialNumberOfLives;
        m_LifeCountIcons[m_NumOfLives - 1].gameObject.SetActive(true);
        m_AnimationLength = lifeCountIconScaling.keys[lifeCountIconScaling.length - 1].time;
    }

    private void Update()
    {
        switch (m_State)
        {
            case State.SCALING_NUMBER:
                float value;
                if (m_Time < m_AnimationLength)
                {
                    value = lifeCountIconScaling.Evaluate(m_Time);
                    m_LifeCountIcons[m_NumOfLives - 1].localScale = new Vector3(value, value, value);
                    m_Time += Time.deltaTime;
                }
                else
                {
                    m_Time = m_AnimationLength;
                    value = lifeCountIconScaling.Evaluate(m_Time);
                    m_LifeCountIcons[m_NumOfLives - 1].localScale = new Vector3(value, value, value);
                    m_Time = 0;
                    m_State = State.ILDE;
                }
                break;
        }
    }

    public void GainedLife()
    {
        ResetCurrentState();
        if (++m_NumOfLives > 9)
        {
            m_NumOfLives = 9;
        }
        m_LifeCountIcons[m_NumOfLives - 1].gameObject.SetActive(true);
        m_LifeCountIcons[m_NumOfLives - 2].gameObject.SetActive(false);
        m_State = State.SCALING_NUMBER;
        //FMODUnity.RuntimeManager.PlayOneShot("event:/Characters/Player/NewLife");
    }

    public void LostLife()
    {
        ResetCurrentState();
        if (--m_NumOfLives > 0)
        {
            m_LifeCountIcons[m_NumOfLives - 1].gameObject.SetActive(true);
            m_LifeCountIcons[m_NumOfLives].gameObject.SetActive(false);
            m_State = State.SCALING_NUMBER;
        }
        else  // m_NumOfLives == 0
        {
            ///TODO FAIL SCENE
        }
    }

    private void ResetCurrentState()
    {
        if (m_State == State.SCALING_NUMBER)
        {
            m_LifeCountIcons[m_NumOfLives - 1].localScale = new Vector3(1, 1, 1);
            m_Time = 0;
        }
    }
}
