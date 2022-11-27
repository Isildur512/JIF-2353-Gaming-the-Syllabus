using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public Animator transition;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            LoadNextLevel();
        }
    }

    public void LoadNextLevel() {
        StartCoroutine(LoadLevel("TestLevel"));

    }

    IEnumerator LoadLevel(string LevelName) {
        //Play animation
        transition.SetTrigger("Start");
        //Finish animation
        yield return new WaitForSeconds(1);
        //Load new level
        SceneManager.LoadScene(LevelName);
    }
}
