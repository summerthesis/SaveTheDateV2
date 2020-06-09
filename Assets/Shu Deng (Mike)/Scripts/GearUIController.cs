using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearUIController : MonoBehaviour
{
    public float rotationTimeLength, rotateSpeed;

    float m_RotationTime;
    int m_GearCount = 0, m_MaxGearCount = 5;
    MeshRenderer[] m_GearsRenderer;
    RectTransform[] m_GearsRectTransform;

    enum State
    {
        Normal,
        CollectedFive
    }
    State m_GearUIState;

    public void GearCollected()
    {
        ++m_GearCount;
    }

    // Start is called before the first frame update
    void Start()
    {       
        m_GearsRenderer = GetComponentsInChildren<MeshRenderer>(true);
        // m_GearsRectTransform also contains the RectTransform of this GameObject
        m_GearsRectTransform = GetComponentsInChildren<RectTransform>(true);
        ResetGears();
    }

    // Update is called once per frame
    void Update()
    {
        switch (m_GearUIState)
        {
            case State.Normal:
                for (int i = 0; i < m_GearCount; ++i)
                {
                    if (m_GearsRenderer[i].enabled == false)
                    {
                        m_GearsRenderer[i].enabled = true;
                    }
                }
                if (m_GearCount == m_MaxGearCount)
                {
                    m_GearUIState = State.CollectedFive;
                    m_GearCount = 0;
                }
                break;
            case State.CollectedFive:
                m_RotationTime += Time.deltaTime;
                if (m_RotationTime > rotationTimeLength)
                {
                    ResetGears();
                }
                else
                {
                    for (int i = 1; i <= m_MaxGearCount; ++i)
                    {
                        m_GearsRectTransform[i].Rotate(new Vector3(0, 0, 1), rotateSpeed * Mathf.Pow(-1, i));
                    }
                }
                break;
        }            
    }

    void ResetGears()
    {
        m_GearUIState = State.Normal;
        m_RotationTime = 0;
        foreach (var renderer in m_GearsRenderer)
        {
            renderer.enabled = false;
        }
        for (int i = 1; i <= m_MaxGearCount; ++i)
        {
            m_GearsRectTransform[i].eulerAngles = new Vector3(90, 0, 0);
        }
    }
}
