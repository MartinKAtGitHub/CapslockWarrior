using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NadeSpriteSorting : MonoBehaviour
{
    [SerializeField] private GameObject nadeGFX;
    [SerializeField] private GameObject nadeShadowGFX;

    private SpriteRenderer nadeSprite;
    private SpriteRenderer nadeShadowSprite;

    private void Awake()
    {
        nadeSprite = nadeGFX.GetComponent<SpriteRenderer>();
        nadeShadowSprite = nadeShadowGFX.GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        SpriteSorting();
    }

    private void SpriteSorting()
    {

        nadeSprite.sortingOrder = (int)(nadeShadowGFX.transform.position.y * -10) + 1;
        nadeShadowSprite.sortingOrder = (int)(nadeShadowGFX.transform.position.y * -10);

        //var selecSortOrgin = Target.position.y - transform.position.y;
        // Debug.Log("SORT = " + transform.position.y);

        // var sortFromThisPosition = transform.position.y - 0.3;

        //  Debug.Log("Sorting Nade From  = " + (int)sortFromThisPosition);
        // spriteRenderer.sortingOrder = (int)sortFromThisPosition * -10;

    }
}
