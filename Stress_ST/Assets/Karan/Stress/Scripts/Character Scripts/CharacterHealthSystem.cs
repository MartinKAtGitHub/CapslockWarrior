
using UnityEngine;
using UnityEngine.UI;

public class CharacterHealthSystem : MonoBehaviour
{
    private Character character;
    private const int HealthPointsPerContainer = 4;
    [SerializeField] private Image[] HeartContainer_Imgs;

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

    private void CalculateMaxHealthOrMana(ref int maxPoints, ref int currentPoints, int pointsPerContainer, Image[] containers)
    {
        maxPoints = pointsPerContainer * containers.Length;
        currentPoints = maxPoints;
    }

    private void ClampHealth()
    {
        character.Stats.Health = Mathf.Clamp(character.Stats.Health, 0, character.Stats.BaseHealth);
        OnHealthOrManaChanged(character.Stats.Health, HeartContainer_Imgs);
    }


    private void OnHealthOrManaChanged(int currentValue, Image[] container)
    {
        int containerIndex = currentValue / HealthPointsPerContainer;
        //Debug.Log("Current Container (" + containerIndex + ")");
        int fill = currentValue % HealthPointsPerContainer;
        //Debug.Log("Current Fill (" + fill + ")");


        if (fill == 0)
        {
            if (containerIndex == container.Length)//indicates full HP
            {
                container[containerIndex - 1].fillAmount = 1;
                return;
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

    private bool IsCharacterAlive(string name)
    {
        if (character.Stats.Health <= 0)
        {
            Debug.Log("<color=blue>" + name + " Is Dead</color>:");
            return true;
        }
        else
        {
            return false;
        }
    }

    public void TakeDamage(int dmg)
    {
        for (int i = 0; i < (Mathf.FloorToInt(dmg)); i++) //PERFORMANCE the system only works for 1 dmg(value)... so i need to calulate for every instance of dmg
        {
            character.Stats.Health -= 1;
            ClampHealth();
        }
    }
}
