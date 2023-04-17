using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class Overview : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void StartGame()
    {
        SceneManager.LoadScene("SampleScene");
    }
    public void HandleInputData(int val)
    {
        if (val == 1){
            SceneManager.LoadScene("Overview");
        }
        else if(val ==2){
            SceneManager.LoadScene("Overview2");
        }
    }

}
