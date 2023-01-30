using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum combatState {START, PLAYERTURN, ENEMYTURN, WIN, LOST, END}

public class CombatManager : MonoBehaviour
{

    public combatState state;
    public GameObject playerPrefab;
    public GameObject enemyPrefab;
    public GameObject levelLoader;
    Unit playerUnit;
    Unit enemyUnit;
    public Text dialogueText;
    public CombatHUD playerHUD;
    public CombatHUD enemyHUD;


    // Start is called before the first frame update
    void Start()
    {
        state = combatState.START;
        setupCombat();
    }

    void setupCombat() {
        GameObject playerGO = Instantiate(playerPrefab);
        playerUnit = playerGO.GetComponent<Unit>();
        GameObject enemyGO = Instantiate(enemyPrefab);
        enemyUnit = enemyGO.GetComponent<Unit>();

        //dialogueText.text = enemyUnit.unitName + " approahces.";

        playerHUD.setHUD(playerUnit);
        enemyHUD.setHUD(enemyUnit);

        state = combatState.PLAYERTURN;
        PlayerTurn();
    }

    void PlayerTurn() {

    }

    IEnumerator PlayerAttack() {
        bool isDead = enemyUnit.TakeDamage(playerUnit.dmg);

        enemyHUD.setHp(enemyUnit.curHP);

        yield return new WaitForSeconds(2f);

        if (isDead) {
            state = combatState.WIN;
            EndBattle();
        } else {
            state = combatState.ENEMYTURN;
            StartCoroutine(EnemyTurn());        
        }
    }

    void EndBattle() {
        if(state == combatState.WIN) {
            SceneManager.LoadScene("TestLevel (NoEnemy)");
        } else {
            //Load menu
        }
    }

    IEnumerator EnemyTurn() {
        bool isDead = playerUnit.TakeDamage(enemyUnit.dmg);
        playerHUD.setHp(playerUnit.curHP);

        yield return new WaitForSeconds(1f);

        if (isDead) {
            state = combatState.LOST;
        } else {
            state = combatState.PLAYERTURN;
            PlayerTurn();
        }
    }

    public void OnAttackButton() {
        if (state != combatState.PLAYERTURN){
            return;
        }
        StartCoroutine(PlayerAttack());
    }

}
