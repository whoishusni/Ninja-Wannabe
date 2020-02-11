using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Loading : MonoBehaviour
{
    float waktu = 0;
    float nextScene = 1.3f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        waktu += Time.deltaTime;
        if(waktu > nextScene)
        {
            SceneManager.LoadScene("Gameplay");
        }
    }
}
