using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelTransition : MonoBehaviour
{
    public string levelName;
    // Update is called once per frame
    void Update()
    {
        SceneManager.LoadScene(levelName);     
    }
}
