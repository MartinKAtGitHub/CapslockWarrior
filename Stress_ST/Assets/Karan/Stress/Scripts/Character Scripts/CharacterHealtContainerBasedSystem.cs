
using UnityEngine;
using UnityEngine.UI;

public abstract class CharacterHealtContainerBasedSystem : MonoBehaviour
{

    private const int HealthPointsPerContainer = 4;
    [SerializeField] protected Image[] HeartContainer_Imgs; // Maybe make a list and sorting by number so that the Containers arent random
    private int maxHealtPoints;
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




    /* void Start()
	{		
		CalculateMaxHealthOrMana(ref maxHealtPoints, ref currentHealtPoints, PointsPerContainer, HeartContainers);
		CalculateMaxHealthOrMana(ref maxManaPoints, ref currentManaPoints, PointsPerContainer, ManaContainers);

		//Debug.Log(currentHealtPoints);

		UIContainerChecks(HeartContainers);
		UIContainerChecks(ManaContainers);
	}*/


    private void UIContainerChecks(Image[] containers)
    {
        for (int i = 0; i < containers.Length; i++)
        {
            if (containers[i].type != Image.Type.Filled)
            {
                Debug.LogError("Wrong Fill Type, needs front img");
            }
        }
    }

    // So the idea here is that 1 container contains X amount of HP so we need to calculat the maximum amount of HP and use that as Base/limit for ouer calcualtions
    private void CalculateMaxHealthOrMana(ref int maxPoints, ref int currentPoints, int pointsPerContainer, Image[] containers)
    {
        maxPoints = pointsPerContainer * containers.Length;
        currentPoints = maxPoints;
    }

    protected void ClampHealth()
    {
        character.Stats.Health = Mathf.Clamp(character.Stats.Health, 0, maxHealtPoints);
        //OnHealthOrManaChanged(character.Stats.Health, HeartContainer_Imgs);
    }


    protected void OnHealthOrManaChanged(int currentHealth, Image[] container)
    {
        int containerIndex = currentHealth / HealthPointsPerContainer;
        //Debug.Log("Current Container (" + containerIndex + ")");
        float fill = currentHealth % HealthPointsPerContainer;
        //Debug.Log("Current Fill (" + fill + ")");


        if (fill == 0)
        {
            if (containerIndex == container.Length)//indicates full HP
            {

                container[containerIndex - 1].fillAmount = 1;
                return; // This breaks the Rest But mayeb ElseIf would fix issue
            }
            if (containerIndex > 0)// indicates anything but 0 health where there are only whole wearts or empty hearts
            {

                container[containerIndex].fillAmount = 0;
                container[containerIndex - 1].fillAmount = 1;

            }
            else // 0 health
            {
                container[containerIndex].fillAmount = 0;
            }
            return;
        }
        container[containerIndex].fillAmount = fill / (float)HealthPointsPerContainer;
    }

    protected void IsCharacterDead()
    {
        if (Character.Stats.Health <= 0)
        {
            Debug.Log("<color=blue>" + name + " Is Dead</color>:");
            OnCharacterDeath();
        }
    }
    protected abstract void OnCharacterDeath(); //Maybe make Abstarct and let the player/enemy handle its Death

    public  void TakeDamage(int dmg)
    {
        Debug.Log("Player Taking Damage!");
        for (int i = 0; i < (Mathf.FloorToInt(dmg)); i++) //PERFORMANCE the system only works for 1 dmg(value)... so i need to calulate for every instance of dmg
        {
            Character.Stats.Health -= 1;
            ClampHealth();
            OnHealthOrManaChanged((int)Character.Stats.Health, HeartContainer_Imgs);
        }
    }
}
