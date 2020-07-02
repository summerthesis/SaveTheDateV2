using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalendarPieceHUDController : MonoBehaviour
{
    public Transform calendarPieceUI;

    private Transform[] m_CalendarIcon = new Transform[4];

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 4; ++i)
        {
            m_CalendarIcon[i] = calendarPieceUI.GetChild(i);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PieceCollected(int pieceNumber)
    {
        m_CalendarIcon[pieceNumber - 1].gameObject.SetActive(true);
    }
}
