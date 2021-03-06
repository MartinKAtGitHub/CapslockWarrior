﻿
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public abstract class Ability : ScriptableObject // TODO change the name to Ability
{
    
    protected Player player; //I need a direct ref to this using Getcomponant, If the Player was a SO the referance would be the object which is in the Folders

    [SerializeField] protected Sprite abilityIcon;
    [SerializeField] protected float manaCost;
    [SerializeField] protected float cooldownTime; // put in Active ? This is Base CD


    protected Image uIElement_Icon;
    /// <summary>
    /// The Dark mask on top of the Icon, used to create CD effect
    /// </summary>
    protected Image uIElement_IconMask;
    /// <summary>
    /// The text used to display the current cooldown value
    /// </summary>
    protected TextMeshProUGUI uIElement_cooldownNumText;

    /// <summary>
    /// Only purpose of this Timer is to be used for the the CD img effect.
    /// </summary>
    protected float cooldownEffectTimer; // put in Active ? // This is for Effect

    public Image UIElement_Icon { get => uIElement_Icon;}
    public Sprite AbilityIcon { get => abilityIcon; }

    //DISCRIPTION() Somthing to tell the user what this ability is aboutMaybe take a Discr SO

    /// <summary>
    /// The actual logic of the Ability
    /// </summary>
    /// <returns>If the ability was succesesfully cast</returns>
    protected abstract bool AbilityLogic();

    //protected void PayManaCost()
    //{
    //    player.Stats.Mana -= manaCost;
    //}
    //protected abstract void RestCoolDownImgEffect();
    //protected abstract bool IsAbilityOnCooldown(); // What if its a Passiv ability --> overider to do nothing
    //protected abstract bool CanPayManaCost(); // What if its a Passiv ability --> overider to do nothing
    //public abstract void CoolDownImgEffect(); // What if its a Passiv ability --> overider to do nothing

    public virtual void InitializeAbility(Player player, Image uIElement_Icon, Image uIElement_IconMask, TextMeshProUGUI uIElement_cooldownNumText)
    {
        this.player = player;
        this.uIElement_Icon = uIElement_Icon;
        this.uIElement_IconMask = uIElement_IconMask;
        this.uIElement_cooldownNumText = uIElement_cooldownNumText;

        SetSpriteToAbilityUIElements();
    }


    
    private void SetSpriteToAbilityUIElements()
    {
        uIElement_Icon.sprite = abilityIcon;
       // uIElement_IconMask.sprite = abilityIcon;
    }




}
