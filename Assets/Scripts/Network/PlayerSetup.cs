using UnityEngine;

public class PlayerSetup : MonoBehaviour
{
    [SerializeField] private Movement movement;
    [SerializeField] private Camera mainCam;

    public void IsLocalPlayer()
    {
        movement.enabled = true;
        mainCam.gameObject.SetActive(true);
    }
}
