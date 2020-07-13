using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
using UnityEngine;

public class IntroController : MonoBehaviour
{
    public double vidTime, currentTime;
    // Start is called before the first frame update
    void Start()
    {
        vidTime = 23;   
    }

    // Update is called once per frame
    void Update()
    {
        currentTime = this.GetComponent<VideoPlayer>().time;
        if(currentTime >= vidTime)
        {
            NextScene();
        }
    }

    void NextScene()
    {
        Debug.Log("Attempting to move to scene 2");
        SceneManager.LoadScene(2);
    }

}
