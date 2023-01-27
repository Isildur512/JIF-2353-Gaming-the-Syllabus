// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.UI;
// using UnityEngine.SceneManagement;

// public enum combatState {START, PLAYERTURN, ENEMYTURN, WIN, LOST, END}

// public class CombatManager : MonoBehaviour
// {

//     public combatState state;
//     public GameObject playerPrefab;
//     public GameObject enemyPrefab;
//     public GameObject levelLoader;
//     Player playerInfo;
//     Enemy enemyInfo;
//     public Text dialogueText;
//     public CombatHUD playerHUD;
//     public CombatHUD enemyHUD;


//     // Start is called before the first frame update
//     void Start()
//     {
//         state = combatState.PLAYERTURN;
//         setupCombat();
//     }

//     void setupCombat() {
//         GameObject playerGO = Instantiate(playerPrefab);
//         playerInfo = playerGO.GetComponent<Player>();
//         GameObject enemyGO = Instantiate(enemyPrefab);
//         enemyInfo = enemyGO.GetComponent<Enemy>();

//         Debug.Log(playerInfo.getHealth());

//     }

//     void switchTurn() {
//         if (state == combatState.PLAYERTURN) {
//             state = combatState.ENEMYTURN;
//             AIController.generateAITimer();
//         } else if (state == combatState.ENEMYTURN) {
//             state = combatState.PLAYERTURN;
//         }
//     }

//     public void attackEnemy() {
//         if (state == combatState.PLAYERTURN) {
//             enemyInfo.setHealth(enemyInfo.getHealth() - playerInfo.getDamage());
//             switchTurn();
//         } else {
//             Debug.Log("IT IS NOT YOUR TURN!");
//         }
//         Debug.Log(enemyInfo.getHealth());
//     }

//     public void attackPlayer() {
//         if (state == combatState.ENEMYTURN) {
            
//         }
//     }

//     // void EndBattle() {
//     //     if(state == combatState.WIN) {
//     //         SceneManager.LoadScene("TestLevel (NoEnemy)");
//     //     } else {
//     //         //Load menu
//     //     }
//     // }

//     // IEnumerator EnemyTurn() {
//     //     bool isDead = playerUnit.TakeDamage(enemyInfo.stats.health);
//     //     playerHUD.setHp(playerInfo.stats.health);

//     //     yield return new WaitForSeconds(1f);

//     //     if (isDead) {
//     //         state = combatState.LOST;
//     //     } else {
//     //         state = combatState.PLAYERTURN;
//     //         PlayerTurn();
//     //     }
//     // }

//     // public void OnAttackButton() {
//     //     if (state != combatState.PLAYERTURN){
//     //         return;
//     //     }
//     //     StartCoroutine(PlayerAttack());
//     // }

// }
