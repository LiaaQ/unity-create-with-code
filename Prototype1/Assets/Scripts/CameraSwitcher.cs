using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    public Camera cam1;
    public Camera cam2;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cam1.enabled = true;
        cam2.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            cam1.enabled = !cam1.enabled;
            cam2.enabled = !cam2.enabled;

            Debug.Log(cam1.enabled.ToString() + cam2.enabled.ToString());
        }
           
    }
}
