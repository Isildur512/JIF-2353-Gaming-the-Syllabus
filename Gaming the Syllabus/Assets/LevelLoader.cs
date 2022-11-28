using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class LevelLoader : MonoBehaviour
{
    public Animator transition;


    // Update is called once per frame
    void Update()
    {
        /*
        if (Mouse.current.leftButton.wasPressedThisFrame) {
            LoadNextLevel();
        }
        */
        if(GameObject.Find("Portal").transform.position.y <=GameObject.Find("Player").transform.position.y ){
            LoadNextLevel();
        }

    }

    public void LoadNextLevel() {
        StartCoroutine(LoadLevel("TestLevel"));

    }
    private void OnCollision2D(Collision2D tile){
        if(tile.gameObject.name == "Portal"){
            LoadNextLevel();
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
