using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayText : MonoBehaviour
{
    public TextMesh text;
    public ParticleSystem sparksParticles;

    private List<string> textToDisplay = new List<string>();

    [SerializeField] private float rotatingSpeed = 1.0f;

    private int currentText = 0;

    // Start is called before the first frame update
    void Start()
    {
        textToDisplay.Add("Congratulations!");
        textToDisplay.Add("All Errors Fixed");

        text.text = textToDisplay[0];

        sparksParticles.Play();
        StartCoroutine(SwitchText());
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up, Time.deltaTime * rotatingSpeed);
    }

    IEnumerator SwitchText()
    {
        while (true)
        {
            yield return new WaitForSeconds(1.5f);
            currentText++;
            currentText %= 2;
            text.text = textToDisplay[currentText];
        }
    }
}