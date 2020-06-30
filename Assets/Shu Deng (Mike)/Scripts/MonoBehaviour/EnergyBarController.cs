using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyBarController : MonoBehaviour
{
    public Transform EnergyBarUI;

    private Transform m_EnergyBarEmpty, m_RotationCenter;

    // Start is called before the first frame update
    void Start()
    {
        m_EnergyBarEmpty = EnergyBarUI.GetChild(1);
        m_RotationCenter = EnergyBarUI.GetChild(2);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateEnergyBar(float energyBarScale)
    {
        float angle = energyBarScale * 103;
        m_EnergyBarEmpty.transform.localPosition = new Vector3(0, 0, 0);
        m_EnergyBarEmpty.transform.localEulerAngles = new Vector3(0, 0, 0);
        m_EnergyBarEmpty.RotateAround(m_RotationCenter.position, new Vector3(0, 0, 1), angle);
    }
}
