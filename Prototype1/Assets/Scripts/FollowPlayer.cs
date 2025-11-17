using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public GameObject player; // Reference to the player GameObject
    [SerializeField] private Vector3 offset = new Vector3(0, 6, -9);

    // Update is called once per frame
    void LateUpdate()
    {
        // Offset the camera position based on the player's position
        transform.position = player.transform.position + offset;
    }
}
