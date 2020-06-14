using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Use Input() to store text into this object (not shown on screen)
// Use Output() to typewrite the sotred text onto the screen
// Use finished to test if the typewriting is done
public class TextUITypewrite : MonoBehaviour
{
    [Tooltip("Interval of outputing each characters")]
    public float outputInterval = 0.07f;    
    [Tooltip("If clear the text after changing to a new line")]
    public bool clearOnNewLine = false;
    [Tooltip("Invertal of changing to a new line, only works when ClearOnNewLine is set")]
    public float changeLineInterval = 2f;
    [Tooltip("Time delay to call this behavior finished after outputing the last character")]
    public float delayToFinish = 2f;
    public bool finished { get; private set; } = false;

    private Text m_textObject;
    private string m_wholeText, m_currentText;
    private int m_currentPosition;
    void Awake()
    {
        m_textObject = GetComponentInChildren<Text>();
        m_wholeText = m_textObject.text;
        Reset();
    }

    void Start()
    {        
        
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
                yield return new WaitForSeconds(changeLineInterval);                                  
            }
            else
            {
                m_currentText += m_wholeText[m_currentPosition];                        
            }
            ++m_currentPosition;
            m_textObject.text = m_currentText;
            yield return new WaitForSeconds(outputInterval);
        }
        yield return new WaitForSeconds(delayToFinish);
        finished = true;
    }  

    public void Input(string inputText)
    {
        m_wholeText = inputText;
        Reset();
        finished = false;
    }
    
    public void Output()
    {
        StartCoroutine(TypeWrite());
    }

    private void Reset()
    {
        m_textObject.text = "";
        m_currentText = "";
        m_currentPosition = 0;
    }
}
