using System.Transactions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountdownController : MonoBehaviour
{
    // There is likely a much better way to do this but this is how I did it on an old project.
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

    // I should prob add parameters here for customzitation on levels. Maybe a certain level has higher timer.
    public static void resetTimer() {
        currentTime = 30f;
    }

    // I also should add parameters here to specify the aiTimer range. 
    public static void generateAITimer() {
        aiTimer = Random.Range(27f, 29f);
    }
}
