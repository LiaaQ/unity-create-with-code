using UnityEngine;
using UnityEngine.Rendering;

public class CameraManager : MonoBehaviour
{

    public float smoothSpeed = 3f;
    public float vertOffset = 3f;

    private float targetY;

    private void Start()
    {
        targetY = transform.position.y;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 currentPos = transform.position;

        float newY = Mathf.Lerp(currentPos.y, targetY, smoothSpeed * Time.deltaTime);

        transform.position = new Vector3(
            currentPos.x,
            newY,
            currentPos.z);
    }

    public void UpdateTargetY(float cubeTop)
    {
        float newY = cubeTop - vertOffset;

        if (newY > targetY)
        {
            targetY = newY;
        }
    }
}
