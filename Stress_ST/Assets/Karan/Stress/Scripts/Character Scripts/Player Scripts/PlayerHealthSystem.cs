
using UnityEngine;

public class PlayerHealthSystem : CharacterHealthSystem
{
    // PLAYER HEALT WILL ALSO HAVE ALOT OF UI ENEMIES WILL NOT
    private void Awake()
    {
        Character = GetComponent<Player>();
    }

    // If i need to override somthing player spesific do it here. Dont make checks to see what system is beeing used just override the call

    public override void TakeDamage(int dmg)
    {
        Debug.Log("Player Taking Damage!");
        for (int i = 0; i < (Mathf.FloorToInt(dmg)); i++) //PERFORMANCE the system only works for 1 dmg(value)... so i need to calulate for every instance of dmg
        {
            Character.Stats.Health -= 1;
            ClampHealth();
            OnHealthOrManaChanged(Character.Stats.Health, HeartContainer_Imgs);
        }
    }
     

    protected override void OnCharacterDeath()
    {
        Debug.Log("Trigger on PLAYER DEATH EVENT");
    }
}
