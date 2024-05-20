using UnityEngine;
using UnityEngine.EventSystems;

public class FlipperButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    PlayerConroller playerConroller;
    private void Start()
    {
        playerConroller = FindObjectOfType<PlayerConroller>();
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if(gameObject.name == "LeftFlipper")
            playerConroller.LeftFlipperUp();
        else if(gameObject.name == "RightFlipper")
            playerConroller.RightFlipperUp();
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        playerConroller.ReturnToNormal();
    }

}
