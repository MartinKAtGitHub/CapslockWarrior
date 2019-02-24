using System.Collections.Generic;
using UnityEngine;

public class StatusEffectManager : MonoBehaviour
{
    //TODO UI TIMER EFFECT POWER SO THAT UI DOSENT LOOKS BUGGED OUT

    public List<StatusEffect> ActiveStatusEffectList;

    private float strongestEffect; // What if its a stun or a confuse or whatever. Cant have 1 variable for them all it will just keep overriding this
    private int strongestEffectIndex;

    // Update is called once per frame
    void Update()
    {
        if (ActiveStatusEffectList.Count > 0)
        {
            ALTSort();

            for (int i = 0; i < ActiveStatusEffectList.Count; i++)
            {
                var activeStatusEffectTimer = ActiveStatusEffectList[i].ActiveCountDown();

                if (activeStatusEffectTimer <= 0)
                {
                    ActiveStatusEffectList[i].EndEffect();

                    for (int j = 0; j < ActiveStatusEffectList.Count; j++) // reset
                    {
                        ActiveStatusEffectList[j].ResetPotancy();
                    }

                    ActiveStatusEffectList.RemoveAt(i);
                    ALTSort();

                }
                else // this might be longer the the time given bacouse we are calling this for every spell not just the time given to the spell
                {
                    ActiveStatusEffectList[ActiveStatusEffectList[i].StrongestEffectIndex].Effect();
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
        for (int i = 0; i < ActiveStatusEffectList.Count; i++)
        {
            //if (StatusEffectList[i].Power > StatusEffectList[i].GetPotancy())
            if (ActiveStatusEffectList[i].Power >= ActiveStatusEffectList[i].Potancy)
            {
                ActiveStatusEffectList[i].Potancy = ActiveStatusEffectList[i].Power;
                ActiveStatusEffectList[i].StrongestEffectIndex = i;
            }
        }
    }
}
