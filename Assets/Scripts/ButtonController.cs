using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
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

        if (gameObject.name == "RestartLevel")
            playerConroller.RestartScene();
        else if(gameObject.name == "NextLevel")
            playerConroller.NextLevel();
        else if (gameObject.name == "ReturnToMenu")
            playerConroller.ReturnToMenu();
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        playerConroller.ReturnToNormal();
    }

}
