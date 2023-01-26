using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    // Start is called before the first frame update
    public static float currentTime = 0f;
    float startTime = 5f;
    public static float aiTimer;

    void Start() {
        currentTime = startTime;
    }

    void Update() {
        currentTime -= 1 * Time.deltaTime;
        if (currentTime <= 0) {
            currentTime = 0;
            Debug.Log(currentTime);
        }
    }

    public static void resetTimer() {
        currentTime = 5f;
    }

    public static void generateAITimer() {
        aiTimer = Random.Range(2f, 5f);
        Debug.Log($"Successfully generated time of {aiTimer}");
    }
}
