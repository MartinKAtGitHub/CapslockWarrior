using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusEffectManager : MonoBehaviour
{
    //TODO UI TIMER EFFECT POWER SO THAT UI DOSENT LOOKS BUGGED OUT

    public List<StatusEffect> StatusEffectList;

    // Update is called once per frame
    void Update()
    {
        if (StatusEffectList.Count > 0)
        {
            ALTSort();

            for (int i = 0; i < StatusEffectList.Count; i++)
            {
                var currentStatusEffectTime = StatusEffectList[i].CountDown();

                if (currentStatusEffectTime <= 0) // the END effect put in slow class
                {
                    StatusEffectList[i].EndEffect();

                    for (int j = 0; j < StatusEffectList.Count; j++) // reset
                    {
                        StatusEffectList[j].ResetPotancy();
                    }

                    StatusEffectList.RemoveAt(i);
                    ALTSort();

                }
                else // this might be longer the the time given bacouse we are calling this for every spell not just the time given to the spell
                {
                    StatusEffectList[StatusEffectList[i].StrongestEffectIndex].Effect();
                   // Debug.Log("( " + StatusEffectList[i].name + " "+ i + "|" + currentStatusEffectTime + " )");
                }
            }
        }
        else
        {
          //  Debug.Log("Player Is Not Slowed");
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
