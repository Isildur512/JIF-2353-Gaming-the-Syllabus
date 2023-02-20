using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    public string levelName;
    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D Player) {
        SceneManager.LoadScene(levelName); 
    }
}
