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
    public Transform ResetDropZone = null;
    public Transform OriginalParent = null; // If i cant drop in valid spot go back to this

    /// <summary>
    /// This will be the Parent that holds all the Drag and drop elements. If a Draggable obj is parented to this, the obj will not have any restrictions to movement due to Layoutgroups
    /// </summary>
    [SerializeField] private Transform freeMotionParent;

    /// <summary>
    /// Used to calculate drag point. This allows you to drag from anywhere on the img. If we didnt use this it would snap to center of img
    /// </summary>
    Vector3 offset;
    Vector2 startPos;
    /// <summary>
    /// Canvas group allows us use certain options to allow us to register PointerEventData through draggable objects.
    /// </summary>
    CanvasGroup canvasGroup;
    LayoutElement layoutElement;
    RectTransform rectTransform;

    public GameObject PlaceHolder;

    public bool OnDropZone;

    public AbilityKeyDropZone keyDropZone;

    

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        layoutElement = GetComponent<LayoutElement>();
        rectTransform = GetComponent<RectTransform>();

        OriginalParent = transform.parent;
       



        //You need to this before Play anyways so LUL
        /* layoutElement.preferredWidth = rectTransform.sizeDelta.x;
         layoutElement.preferredHeight = rectTransform.sizeDelta.y;
         layoutElement.flexibleWidth = 0;
         layoutElement.flexibleHeight = 0;
         */
    }


    public void OnBeginDrag(PointerEventData eventData)
    {
        startPos = eventData.position;
        offset = (Vector2)transform.position - startPos;

        if (!OnDropZone)
        {
            CreatePlaceHolderObj();
        }

        ResetDropZone = transform.parent; // Only realy relavent if on dropzone

        FreeDragMode();

        if (keyDropZone != null)
        {
            keyDropZone.IsKeyOccupied = false;
            keyDropZone = null;
            // Disconnect AB from KEY
        }

    }

    private void FreeDragMode()
    {
        transform.SetParent(freeMotionParent); // This takes us out of the Pnl(layout group) so we can freely move the UI element
        canvasGroup.blocksRaycasts = false; // after pick up we turn this off to allow PointerEventData to go through the draggable obj

        OnDropZone = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position + (Vector2)offset;
    }

    public void OnEndDrag(PointerEventData eventData) // THIS FIRES AFTER ONDROP () in AbilityKeyDropZone
    {
        if(OnDropZone) // If i am on a dropZone
        {
            Debug.Log("OnEndDrag -> Drop zone TRUE REST TO -> " + ResetDropZone.name);
            transform.SetParent(ResetDropZone);
        }else
        {
            RestBackToOriginalPosition();
        }

        

        //transform.SetSiblingIndex(PlaceHolder.transform.GetSiblingIndex());
        //Destroy(PlaceHolder);

        canvasGroup.blocksRaycasts = true; // after pick up we turn this off to allow PointerEventData to go through the draggable obj
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


    private void RestBackToOriginalPosition()
    {
        transform.SetParent(OriginalParent);
        transform.SetSiblingIndex(PlaceHolder.transform.GetSiblingIndex());
        Destroy(PlaceHolder);
    }

}
