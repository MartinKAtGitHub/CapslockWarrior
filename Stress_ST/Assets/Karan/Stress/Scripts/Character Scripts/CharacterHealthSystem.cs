
using UnityEngine;
using UnityEngine.UI;

public abstract class CharacterHealthSystem : MonoBehaviour
{
    
    private Character character;
  
    public Character Character
    {
        get
        {
            return character;
        }

        set
        {
            character = value;
        }
    }

   /* protected void Awake() // To easy to forget base.Awake in childe class so just use 1 Awake
    {
        character = GetComponent<Character>();
    }*/

    public abstract void TakeDamage(int dmg);
    
    protected abstract void OnCharacterDeath(); //Maybe make Abstarct and let the player/enemy handle its Death
    protected abstract void GetCharacterComponant();
    protected abstract void SetInitialHP();

}

