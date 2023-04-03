using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    [SerializeField] private string levelName;
    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D Player) {
        SceneManager.LoadScene(levelName); 
    }

    public void buttonLoad() {
        SceneManager.LoadScene(levelName);
    }
}
