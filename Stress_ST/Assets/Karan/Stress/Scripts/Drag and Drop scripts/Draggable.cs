using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup))]
[RequireComponent(typeof(LayoutElement))]
public class Draggable : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    /// <summary>
    /// This is the area/pnl the Ability will be childed to. we store it so we can use it to go back in case the Drop point is invalid Or assign it as a New Pos for Draggable
    /// </summary>
    public Transform ParentDropZone = null;
    public Transform OriginalParent = null; // If i cant drop in valid spot go back to this

    /// <summary>
    /// This will be the Parent that holds all the Drag and drop elements. If a Draggable obj is parented to this, the obj will not have any restrictions to movement due to Layoutgroups
    /// </summary>
    [SerializeField] private Transform freeMotionParent;

    Vector3 offset;
    Vector2 startPos;
    /// <summary>
    /// Canvas group allows us use certain options to allow us to register PointerEventData through draggable objects.
    /// </summary>
    CanvasGroup canvasGroup;
    LayoutElement layoutElement;
    RectTransform rectTransform;

     public GameObject PlaceHolder;


    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        layoutElement = GetComponent<LayoutElement>();
        rectTransform = GetComponent<RectTransform>();

        //You need to this before Play anyways so LUL
       /* layoutElement.preferredWidth = rectTransform.sizeDelta.x;
        layoutElement.preferredHeight = rectTransform.sizeDelta.y;
        layoutElement.flexibleWidth = 0;
        layoutElement.flexibleHeight = 0;
        */
    }


    public void OnBeginDrag(PointerEventData eventData)
    {

        CreatePlaceHolderObj();

        ParentDropZone = transform.parent;
        transform.SetParent(freeMotionParent); // This takes us out of the Pnl(layout group) so we can freely move the UI element
        canvasGroup.blocksRaycasts = false; // after pick up we turn this off to allow PointerEventData to go through the draggable obj

        startPos = eventData.position;
        offset = (Vector2)transform.position - startPos;
        
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position + (Vector2)offset;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(ParentDropZone);
        transform.SetSiblingIndex(PlaceHolder.transform.GetSiblingIndex());
        canvasGroup.blocksRaycasts = true; // after pick up we turn this off to allow PointerEventData to go through the draggable obj

        Destroy(PlaceHolder);
    }


    private void CreatePlaceHolderObj()
    {
        PlaceHolder = new GameObject();
        PlaceHolder.name = "PlaceHolder (" + name + ") ";
        PlaceHolder.transform.SetParent(transform.parent);
        var le = PlaceHolder.AddComponent<LayoutElement>();
        le.preferredWidth = layoutElement.preferredWidth;
        le.preferredHeight = layoutElement.preferredHeight;
        le.flexibleWidth = 0;
        le.flexibleHeight = 0;

        PlaceHolder.transform.SetSiblingIndex(transform.GetSiblingIndex());
    }
}
