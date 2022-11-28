using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class LevelLoader : MonoBehaviour
{
    public Animator transition;
    public string curLevel;

    // Update is called once per frame
    void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame) {
            LoadNextLevel();
        }
    }

    public void LoadNextLevel() {
        if (curLevel == "SampleScene") {
            StartCoroutine(LoadLevel("TestLevel"));
        } else if (curLevel == "TestLevel") {
            StartCoroutine(LoadLevel("Combat"));
        }
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
