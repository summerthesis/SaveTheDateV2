using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextUITypewrite : MonoBehaviour
{
    public float outputInterval = 0.07f;
    [Tooltip("If clear the text after changing to a new line")]
    public bool clearOnNewLine = false;

    private Text m_textObject;
    private string m_wholeText, m_currentText;
    private int m_currentPosition;
    void Awake()
    {
       
    }

    void Start()
    {
        m_textObject = GetComponentInChildren<Text>();
        m_wholeText = m_textObject.text;
        m_textObject.text = "";
        m_currentText = "";
        m_currentPosition = 0;
        StartCoroutine(TypeWrite());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator TypeWrite()
    {
        while (m_currentPosition < m_wholeText.Length)
        {
            if (clearOnNewLine == true && m_wholeText[m_currentPosition] == '\n')
            {
                m_currentText = "";
                                  
            }
            else
            {
                m_currentText += m_wholeText[m_currentPosition];                        
            }
            ++m_currentPosition;
            m_textObject.text = m_currentText;
            yield return new WaitForSeconds(outputInterval);
        }        
    }
}
