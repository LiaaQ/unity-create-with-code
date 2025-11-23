using System.Collections;
using UnityEngine;

public class CubeManager : MonoBehaviour
{
    public GameObject cubePrefab;
    public float offsetY = 4f;

    private GameObject currCube;
    private CameraManager camManager;
    private ScoreManager scoreManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        camManager = Camera.main.GetComponent<CameraManager>();
        scoreManager = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();
        StartCoroutine(SpawnCubes());
    }

    IEnumerator SpawnCubes()
    {
        while(!scoreManager.isGameOver)
        {
            Vector3 spawnPos = new Vector3(0, Camera.main.transform.position.y + offsetY, 0);
            currCube = Instantiate(cubePrefab, spawnPos, Quaternion.identity);

            yield return new WaitUntil(() => currCube.GetComponent<Cube>().hasSettled);

            float cubeTop = currCube.transform.position.y;

            camManager.UpdateTargetY(cubeTop);

            yield return new WaitForSeconds(0.5f);
        }
    }
}
