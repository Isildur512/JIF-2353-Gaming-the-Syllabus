using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum combatState {START, PLAYERTURN, ENEMYTURN, WIN, LOST, END}

public class CombatManager : MonoBehaviour
{

    public combatState state;
    public GameObject playerPrefab;
    public GameObject enemyPrefab;
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
    }

}
