using System.Transactions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountdownController : MonoBehaviour
{
    public static float currentTime = 0f;
    float startingTime = 30f;
    public static float aiTimer;


    void Start() {
        currentTime = startingTime;
    }

    void Update()
    {
        currentTime -= 1 * Time.deltaTime;
        if (currentTime <= 0) {
            currentTime = 0;
        }
        // Debug.Log(currentTime);
    }

    public static void resetTimer() {
        currentTime = 30f;
    }

    public static void generateAITimer() {
        aiTimer = Random.Range(27f, 29f);
    }
}
