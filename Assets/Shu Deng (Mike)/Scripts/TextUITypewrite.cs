using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextUITypewrite : MonoBehaviour
{
    public float outputInterval = 0.1f;

    private Text m_textObject;
    private string m_wholeText, m_currentText;
    void Awake()
    {
        m_textObject = GetComponentInChildren<Text>();
        m_wholeText = m_textObject.text;
        m_textObject.text = "";
        m_currentText = "";
    }

    private void Start()
    {
        StartCoroutine(TypeWrite());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator TypeWrite()
    {
        while (m_currentText.Length < m_wholeText.Length)
        {
            m_currentText += m_wholeText[m_currentText.Length];
            m_textObject.text = m_currentText;
            yield return new WaitForSeconds(outputInterval);
        }        
    }
}
