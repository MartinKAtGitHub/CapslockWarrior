using UnityEngine;

[ExecuteAlways]
public class SortingLayerScriptForStaticObject : MonoBehaviour
{
    Renderer spriteRendrer;

    // Start should work in editor mode But also in final version
    void Start()
    {
        spriteRendrer = GetComponent<Renderer>();//TODO FIX static sprite sorting to find in children and not attach script antop of GFX
        //spriteRenderer.sortingOrder = (int)(spriteRenderer.transform.position.y * -10);
        spriteRendrer.sortingOrder = (int)(transform.position.y * -10);
    }

    /*
    * The tag means the code will only run when the game is played from/in the unity Editor. 
    * With the addition of [ExecuteAlways] the Update loop SHOULD play in (only)editor and not in final product
    */
#if UNITY_EDITOR
    void Update() 
    {
        //spriteRenderer.sortingOrder = (int)(spriteRenderer.transform.position.y * -10);

        spriteRendrer.sortingOrder = (int)(transform.position.y * -10);
        //Debug.Log("TEST in EDITOR");
    }
#endif

}
