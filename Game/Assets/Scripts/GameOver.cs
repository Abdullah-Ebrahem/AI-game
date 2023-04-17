using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    public void retry()
    {
        SceneManager.LoadScene(1);
        Time.timeScale= 1.0f;
    }
    public void home()
    {
        SceneManager.LoadScene(0);
        Time.timeScale= 1.0f;
    }
}
