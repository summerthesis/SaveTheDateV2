using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearIconHUDController : MonoBehaviour
{
    public Transform gearCountUI;

    private Transform[] m_GearIcons = new Transform[5];
    private LifeCountHUDController m_LifeCount;
    private int m_NumOfGears = 0;
    enum State
    {
        Normal,
        CollectedFive
    }
    State m_GearHUDState = State.Normal;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 5; ++i)
        {
            m_GearIcons[i] = gearCountUI.GetChild(i);
        }

        m_LifeCount = GetComponent<LifeCountHUDController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (m_GearHUDState == State.Normal)
        {
            if (m_NumOfGears >= 5)
            {
                m_NumOfGears -= 5;
                for (int i = 0; i < 5; ++i)
                {
                    m_GearIcons[i].GetChild(0).gameObject.SetActive(false);
                    m_GearIcons[i].GetChild(1).gameObject.SetActive(true);
                }
                StartCoroutine(OnCollectedFive());
            }
        }
    }

    public void GearCollected()
    {
        ++m_NumOfGears;
        if (m_GearHUDState == State.Normal)
        {
            if (m_NumOfGears < 5)
            {
                m_GearIcons[m_NumOfGears - 1].GetChild(0).gameObject.SetActive(false);
                m_GearIcons[m_NumOfGears - 1].GetChild(1).gameObject.SetActive(true);
            }
            
        }             
    }

    private IEnumerator OnCollectedFive()
    {
        m_GearHUDState = State.CollectedFive;
        yield return new WaitForSeconds(0.1f);
        for (int j = 0; j < 3; ++j)
        {
            for (int i = 0; i < 5; ++i)
            {
                m_GearIcons[i].GetChild(0).gameObject.SetActive(true);
                m_GearIcons[i].GetChild(1).gameObject.SetActive(false);
            }
            yield return new WaitForSeconds(0.1f);
            for (int i = 0; i < 5; ++i)
            {
                m_GearIcons[i].GetChild(0).gameObject.SetActive(false);
                m_GearIcons[i].GetChild(1).gameObject.SetActive(true);
            }
            yield return new WaitForSeconds(0.1f);            
        }
        
        for (int i = 0; i < 5; ++i)
        {
            m_GearIcons[i].GetChild(0).gameObject.SetActive(true);
            m_GearIcons[i].GetChild(1).gameObject.SetActive(false);
            yield return new WaitForSeconds(0.1f);
        }

        m_LifeCount.GainedLife();
        if (m_NumOfGears > 0 && m_NumOfGears < 5)
        {
            for (int i = 0; i < m_NumOfGears; ++i)
            {
                m_GearIcons[i].GetChild(0).gameObject.SetActive(false);
                m_GearIcons[i].GetChild(1).gameObject.SetActive(true);
            }
        }
        m_GearHUDState = State.Normal;
    }
}
