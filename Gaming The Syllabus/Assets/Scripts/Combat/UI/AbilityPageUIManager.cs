using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityPageUIManager : Singleton<AbilityPageUIManager>
{

    public Transform abilityPage;
    public GameObject abilityButtonPrefab;

    private void Awake()
    {
        InitializeSingleton();
        StartCoroutine(Start());
    }
    // Start is called before the first frame update
    public IEnumerator Start()
    {
        while (!CombatManager.isDone)
            yield return null;

        foreach (PlayerAbility ability in CombatManager.player.abilities)
        {
            GameObject abilityButton = Instantiate(abilityButtonPrefab, abilityPage);
            abilityButton.name = ability.abilityName;
            Debug.Log($"Player abilities count: {CombatManager.player.abilities.Count}");
        }
    }

}
