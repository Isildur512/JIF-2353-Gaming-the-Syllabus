using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class LevelLoader : MonoBehaviour
{
    public Animator transition;
    [SerializeField] private string curLevel;

    // Update is called once per frame
    void Update()
    {
        /*
        if (Mouse.current.leftButton.wasPressedThisFrame) {
            LoadNextLevel();
        }
        */
        /*if(transform.position == GameObject.Find("Player").transform.position) {
            LoadLevel();
        }*/
        /*if (GameObject.Find("Player").transform.position.y  > 22){
            SceneManager.LoadScene("TestLevel");
        }
        Debug.Log("Y position" + GameObject.Find("Player").transform.position.y);  */
        

    }

    public void LoadLevel(Scene toLoad) {
        /*if (curLevel == "SampleScene") {
            StartCoroutine(LoadLevel("TestLevel"));
        } else if (curLevel == "TestLevel") {
            StartCoroutine(LoadLevel("Combat"));
        } else if (curLevel == "Combat") {
            StartCoroutine(LoadLevel("TestLevel (NoEnemy)"));
        }*/
        string sceneName = toLoad.name;
        SceneManager.LoadScene(sceneName);
    }
   /* private void OnCollision2D(Collision2D tile){
        if(tile.gameObject.name == "Portal"){
            LoadNextLevel();
        }
    }*/

    IEnumerator LoadLevel(string LevelName) {
        //Play animation
        transition.SetTrigger("Start");
        //Finish animation
        yield return new WaitForSeconds(1);
        //Load new level
        SceneManager.LoadScene(LevelName);
    }
}
