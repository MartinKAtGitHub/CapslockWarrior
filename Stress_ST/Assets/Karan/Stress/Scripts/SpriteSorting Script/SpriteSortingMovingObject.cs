using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[ExecuteAlways]
public class SpriteSortingMovingObject : MonoBehaviour
{
    [SerializeField]SpriteRenderer spriteRenderer;

    private void Awake()
    {
       spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    void Update()
    {
        spriteRenderer.sortingOrder = (int)(spriteRenderer.transform.position.y * -10);
    }
}
