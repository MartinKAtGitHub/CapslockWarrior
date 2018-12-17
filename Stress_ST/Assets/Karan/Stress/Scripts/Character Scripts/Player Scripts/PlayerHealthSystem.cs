
using UnityEngine;

public class PlayerHealthSystem : CharacterHealthSystem
{

    private void Awake()
    {
        Character = GetComponent<Player>();
    }

}
