using System.Linq;
using System.Reflection.Emit;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    [SerializeField] CombatManager combatManager;

    // Start is an Enumerator because I want combatManager to be loaded in first. Therefore it waits for it to be initialized.
    IEnumerator Start() {
        yield return new WaitUntil(() => combatManager.isInitialized);
    }

    void Update() {
        if (CombatManager.turnQueue.Peek() is not Player) {
            // Once currentTime is less than aiTimer, then the enemy performs their action. This is to add a smooth experience.
            if (CountdownController.currentTime <= CountdownController.aiTimer) {                               
                CombatManager.turnQueue.Peek().PerformTurn(Enemy.Actions.basicAttack);
            }
        }
    }

    // public static void resetTimer() {
    //     currentTime = 5f;
    // }

    // public static void generateAITimer() {
    //     aiTimer = Random.Range(2f, 5f);
    //     Debug.Log($"Successfully generated time of {aiTimer}");
    // }
}
