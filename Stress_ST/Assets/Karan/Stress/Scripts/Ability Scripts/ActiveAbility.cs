
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public abstract class ActiveAbility : Ability
{
    /// <summary>
    /// The target Time.Time when the ability will be ready to be cast. So at the start it will be 0 But when cast The curretn time will be saved and added whith the cooldown time -> TimeWhenAbilityIsReady = Time.time + CooldownTime
    /// </summary>
    protected float TimeWhenAbilityIsReady;

    public override void InitializeAbility(Player player, Image uIElement_Icon, Image uIElement_IconMask, TextMeshProUGUI uIElement_cooldownNumText)
    {
        base.InitializeAbility(player, uIElement_Icon, uIElement_IconMask, uIElement_cooldownNumText);

        TimeWhenAbilityIsReady = 0f; // So the ability is ready to go immediately after spawn;
        IsAbilityOnCD(false, false);

        // Debug.Log("INIT Active Ability");
    }

    public void CoolDownImgEffect() // Do somthing els for passiv ab
    {
        if (cooldownEffectTimer > 0)
        {
            cooldownEffectTimer -= Time.deltaTime;
            float roundedCd = Mathf.Round(cooldownEffectTimer);
            uIElement_cooldownNumText.text = roundedCd.ToString();

            uIElement_IconMask.fillAmount = (cooldownEffectTimer / cooldownTime);
        }
        else
        {
            IsAbilityOnCD(false, false);
            return;
        }
    }

    /// <summary>
    /// Dose all the checks before calling AbilityLogic().
    /// </summary>
    public void CastAbility()
    {
        var abilityName = name;
        if (player != null)
        {
            if (IsAbilityOnCooldown())
            {
                if (CanPayManaCost())
                {

                    var abilityStatus = AbilityLogic();

                    if (abilityStatus)
                    {
                        Debug.Log(abilityName + " <= Cast Succsesful");

                        PayManaCost();
                        SetNewTimeWhenAbilityIsReadyOnSuccsefulcast();
                        RestCoolDownImgEffect();
                        IsAbilityOnCD(true, true);
                    }
                    else
                    {
                        Debug.Log(abilityName + " <= Cast Failed");
                    }
                }
                else
                {
                    Debug.Log(" <color=blue>NO MANA BZZZZZZ MAKE SOUND OR ICON TO INDICATE THIS</color>");
                }
            }
            else
            {
                Debug.Log("<color=darkblue> " + abilityName + " On CD</color>");
            }
        }
        else
        {
            Debug.LogError(abilityName + " Dose not have a Payer Have you Initialized the Ability");
        }
    }



    private bool IsAbilityOnCooldown()
    {
        if (Time.time > TimeWhenAbilityIsReady)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private bool CanPayManaCost()
    {
        if (player.Stats.Mana >= manaCost)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void PayManaCost()
    {
        player.Stats.Mana -= manaCost;
    }

    private void RestCoolDownImgEffect()
    {
        cooldownEffectTimer = cooldownTime;
    }
    /// <summary>
    /// When the ability has successfully cast, we want to give it a new CD time so we add ouer ouer CD time to ouer curretn time to get a new target time to aim for.
    /// </summary>
    protected void SetNewTimeWhenAbilityIsReadyOnSuccsefulcast()
    {
        TimeWhenAbilityIsReady = cooldownTime + Time.time;
    }

    /// <summary>
    /// Fuction Updates the UI elements to show if the ability is on cooldown or not
    /// </summary>
    /// <param name="iconMaskStatus">UI Element which reperesents the Mask on top of the Ability Icon</param>
    /// <param name="cooldownNumTextStatus">UI Element which reperesents the cooldown num text</param>
    protected void IsAbilityOnCD(bool iconMaskStatus, bool cooldownNumTextStatus)
    {
        uIElement_IconMask.enabled = iconMaskStatus;
        uIElement_cooldownNumText.enabled = cooldownNumTextStatus;
    }

    /// <summary>
    /// This methods will allow for Img cd Effect.
    /// </summary>


}
