using UnityEngine;
using UnityEngine.EventSystems;
using MoreMountains.Feedbacks;

public class ImageHoverDetector : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private bool isHovering = false;
    public MMFeedbacks mMFeedbacks;
    public GameObject gameObjects;

    // This method is called when the mouse pointer enters the UI element
    public void OnPointerEnter(PointerEventData eventData)
    {
        isHovering = true;
        Debug.Log("Mouse entered " + gameObjects.name);
        mMFeedbacks?.PlayFeedbacks();

        // Add your desired actions here, e.g., change image color, show tooltip
    }

    // This method is called when the mouse pointer exits the UI element
    public void OnPointerExit(PointerEventData eventData)
    {
        isHovering = false;
        Debug.Log("Mouse exited " + gameObjects.name);
        // Add your desired actions here, e.g., revert image color, hide tooltip
    }

    // You can access the 'isHovering' boolean in other parts of your script or from other scripts
    public bool IsMouseOverImage()
    {
        return isHovering;
    }
}