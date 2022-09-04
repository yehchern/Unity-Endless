using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Text type variable is a UI element
using TMPro;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public CharacterDatabase characterDB;

    //public TextMeshProUGUI nameText;
    public SpriteRenderer artworkSprite;

    private int selectedOption = 0;//keep track which character is be in selection


    // Start is called before the first frame update
    void Start()
    {
        if (!PlayerPrefs.HasKey("selectedOption"))
        {
            selectedOption = 0;
        }
        else
        {
            Load();

        }

        UpdateCharacter(selectedOption);
    }

    //Update character sprite and character name
    private void UpdateCharacter(int selectedOption)
    {
        Character character = characterDB.GetCharacter(selectedOption);
        artworkSprite.sprite = character.characterSprite;
        //nameText.text = character.characterName;
    }

    private void Load()
    {
        selectedOption = PlayerPrefs.GetInt("selectedOption");
        Debug.Log("?????????????????");
        Debug.Log(selectedOption);
    }
}
