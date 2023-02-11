using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AbilityPageUIManager : Singleton<AbilityPageUIManager>
{

    public GameObject abilityPage;
    public GameObject abilityButtonPrefab;

    public GameObject abilitiesGameObject;



    private void Awake()
    {
        InitializeSingleton();
    }
    // Start is called before the first frame update
    public IEnumerator Start()
    {
        while (!CombatManager.isDone)
            yield return null;

        foreach (PlayerAbility ability in CombatManager.player.abilities)
        {
            GameObject abilityButton = Instantiate(abilityButtonPrefab, abilitiesGameObject.transform);
            abilityButton.name = ability.abilityName;
            
            Button button = abilityButton.GetComponent<Button>();
            button.onClick.AddListener(() => {AbilityController.UseAttackAbility(ability);
                                                abilityPage.SetActive(false);});

            abilityButton.GetComponent<Tooltip>().message = ability.abilityDesc;
            abilityButton.GetComponentInChildren<TMP_Text>().text = ability.abilityName;
        }

        abilityPage.SetActive(false);
    }

    public static void AppendAbilityToPage(PlayerAbility ability)
    {
        GameObject abilityButton = Instantiate(_instance.abilityButtonPrefab, _instance.abilitiesGameObject.transform);
        abilityButton.name = ability.abilityName;
        
        Button button = abilityButton.GetComponent<Button>();
        button.onClick.AddListener(() => {AbilityController.UseAttackAbility(ability);
                                            _instance.abilityPage.SetActive(false);});

        abilityButton.GetComponent<Tooltip>().message = ability.abilityDesc;
        abilityButton.GetComponentInChildren<TMP_Text>().text = ability.abilityName;
    }

}
