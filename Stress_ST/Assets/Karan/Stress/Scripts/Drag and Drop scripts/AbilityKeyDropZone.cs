using UnityEngine;
using UnityEngine.EventSystems;

public class AbilityKeyDropZone : MonoBehaviour, IDropHandler//, IPointerEnterHandler, IPointerExitHandler
{
   public bool IsKeyoccupied;
    Draggable curretnDraggable;





    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("ON DROP ZONE");

        if(!IsKeyoccupied)
        {
            curretnDraggable = eventData.pointerDrag.GetComponent<Draggable>();
            if (curretnDraggable != null)
            {
                curretnDraggable.ResetDropZone = transform;
                curretnDraggable.OnDropZone = true;
                curretnDraggable.keyDropZone = this;
                IsKeyoccupied = true;
                
                // curretnDraggable.transform.position = transform.position;
            }
        }else
        {
            Debug.Log("Ability Key is occupied ");
        }
    }
}
