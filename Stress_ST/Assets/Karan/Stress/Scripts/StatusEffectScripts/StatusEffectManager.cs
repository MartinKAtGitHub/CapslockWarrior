using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusEffectManager : MonoBehaviour
{
    //TODO UI TIMER EFFECT POWER SO THAT UI DOSENT LOOKS BUGGED OUT

    public List<StatusEffect> StatusEffectList;


    // private List<StatusEffect> ActiveStatusEffectList;
   
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (StatusEffectList.Count > 0)
        {
            //SortStrongestEffect();
            ALTSort();

            for (int i = 0; i < StatusEffectList.Count; i++)
            {
                StatusEffectList[i].Time -= Time.deltaTime;

                if (StatusEffectList[i].Time <= 0) // the END effect put in slow class
                {
                    StatusEffectList[i].EndEffect();

                    for (int j = 0; j < StatusEffectList.Count; j++) // reset
                    {
                        StatusEffectList[j].ResetPotancy();
                    }

                    StatusEffectList.RemoveAt(i);
                    // SortStrongestEffect(); // PERFORMANCE idk if this should be here but i need to UPDATE the INDEX of all my status effect or they go out of bounds
                    ALTSort();

                }
                else // this might be longer the the time given bacouse we are calling this for every spell not just the time given to the spell
                {
                    StatusEffectList[StatusEffectList[i].StrongestEffectIndex].Effect();
                }
            }
        }
        else
        {
            Debug.Log("Player Is Not Slowed");
        }
    }

    private void ALTSort()
    {
        for (int i = 0; i < StatusEffectList.Count; i++)
        {
            //if (StatusEffectList[i].Power > StatusEffectList[i].GetPotancy())
            if (StatusEffectList[i].Power > StatusEffectList[i].Potancy)
            {
                StatusEffectList[i].Potancy = StatusEffectList[i].Power;
                StatusEffectList[i].StrongestEffectIndex = i;
            }
        }
    }
}
