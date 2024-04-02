using UnityEngine;

public class Camera_Controller : MonoBehaviour
{
    [SerializeField] private Transform player;

    private void Update()
    {
        //make the camera follow the player object
        transform.position = new Vector3(player.position.x, player.position.y, transform.position.z);
    }
}
