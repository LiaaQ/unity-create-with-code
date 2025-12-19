using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class CharacterSwitcher : MonoBehaviour
{
    [SerializeField] private Transform charactersParent;
    private List<Character> characters = new();
    private int currentIndex = 0;
    public Character CurrentCharacter => characters[currentIndex];

    private void Awake()
    {
        foreach (Transform child in charactersParent)
        {
            Character character = child.GetComponent<Character>();
            if (character != null)
            {
                characters.Add(character);
                Debug.Log($"Added character: {character.name}");
            }
        }

        ActivateCharacter(0);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            SwitchToNext();
        }
    }

    private void SwitchToNext()
    {
        int nextIndex = (currentIndex + 1) % characters.Count;
        ActivateCharacter(nextIndex);
    }

    private void ActivateCharacter(int index)
    {
        Vector3 position = characters[currentIndex].transform.position;

        characters[currentIndex].gameObject.SetActive(false);

        currentIndex = index;

        characters[currentIndex].gameObject.SetActive(true);
        characters[currentIndex].transform.position = position;

        Debug.Log($"Switched to character: {characters[currentIndex].name}");
    }
}
