using UnityEngine;

public class WeaponSlotController : MonoBehaviour
{
    public GameObject obstacle;
    public void OnMouseDown()
    {
        Debug.Log("The button is clicked");
        Debug.Log("The current game state is: " + GameManager.instance.gameState);  
        if (GameManager.instance.gameState == GameState.PlaceWeapon)
        {
            GameEvents.Publish(new WeaponSlotClickedEvent(obstacle));
        }
        // Destroy the button after clicking
        Destroy(gameObject);
    }       
}