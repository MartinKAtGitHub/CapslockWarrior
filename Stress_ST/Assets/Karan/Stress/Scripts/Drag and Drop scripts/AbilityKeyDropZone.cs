using UnityEngine;
using UnityEngine.EventSystems;

public class AbilityKeyDropZone : MonoBehaviour, IDropHandler
{
    //bool isKeyoccupied;
    Draggable curretnDraggable;

    public void OnDrop(PointerEventData eventData)
    {
        //If isKeyoccupied swap

        if (curretnDraggable == null)
        {
            curretnDraggable = eventData.pointerDrag.GetComponent<Draggable>();
            if (curretnDraggable != null)
            {
                curretnDraggable.ParentDropZone = transform;
                // curretnDraggable.transform.position = transform.position;
            }
        }
        else
        {
            /*curretnDraggable.transform.SetParent(curretnDraggable.PlaceHolder.transform.parent);
            curretnDraggable.transform.SetSiblingIndex(curretnDraggable.PlaceHolder.transform.GetSiblingIndex());
            Destroy(curretnDraggable.PlaceHolder);
            curretnDraggable = eventData.pointerDrag.GetComponent<Draggable>();*/
        }
    }
}
