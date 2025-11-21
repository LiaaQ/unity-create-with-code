using System.Collections;
using UnityEngine;

public class CubeManager : MonoBehaviour
{
    public GameObject cubePrefab;
    public float offsetY = 4f;

    [SerializeField] private float border = 7f;
    [SerializeField] private float speed = 2f;

    private int direction = 1;
    private GameObject currCube;
    private CameraManager camManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        camManager = Camera.main.GetComponent<CameraManager>();
        StartCoroutine(CubeSpawner());
    }

    IEnumerator CubeSpawner()
    {
        while(true)
        {
            Vector3 spawnPos = new Vector3(0, Camera.main.transform.position.y + offsetY, 0);
            currCube = Instantiate(cubePrefab, spawnPos, Quaternion.identity);
            Rigidbody rb = currCube.GetComponent<Rigidbody>();
            rb.useGravity = false;

            while (!Input.GetKeyDown(KeyCode.Space))
            {
                MoveCube();
                yield return null;
            }

            rb.useGravity = true;

            yield return new WaitForSeconds(1f);

            yield return new WaitUntil(() => rb.angularVelocity.magnitude < 0.1f);

            float cubeTop = currCube.transform.position.y;

            camManager.UpdateTargetY(cubeTop);

            yield return new WaitForSeconds(0.5f);
        }
    }

    void MoveCube()
    {
        if (currCube == null) return;

        float move = speed * direction * Time.deltaTime;
        currCube.transform.position += new Vector3(move, 0, 0);

        Debug.Log(currCube.transform.position.x);

        if (currCube.transform.position.x > border) direction = -1;
        else if (currCube.transform.position.x < -border) direction = 1;
    }
}
