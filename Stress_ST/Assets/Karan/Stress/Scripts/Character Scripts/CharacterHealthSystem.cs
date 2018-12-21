
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

    private void Awake()
    {
        character = GetComponent<Character>();
    }

    public abstract void TakeDamage(int dmg);
    
    protected abstract void OnCharacterDeath(); //Maybe make Abstarct and let the player/enemy handle its Death

}

