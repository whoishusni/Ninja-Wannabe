using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMan : MonoBehaviour
{
   
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            exitGame();
        }
    }

    public void exitGame()
    {
        Application.Quit();
    }

    public void retryScene()
    {
        SceneManager.LoadScene("Gameplay");
    }

    public void mainMenu()
    {
        SceneManager.LoadScene("Main");
    }


}
