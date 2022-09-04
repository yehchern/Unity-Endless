using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Text type variable is a UI element
using TMPro;

public class CharacterManager : MonoBehaviour
{
    public CharacterDatabase characterDB;

    public TextMeshProUGUI nameText;
    public SpriteRenderer artworkSprite;

    private int selectedOption = 0;//keep track which character is be in selection
    
    // Start is called before the first frame update
    void Start()
    {
        UpdateCharacter(selectedOption);
    }

    //This function will be called when player click the next button
    public void NextOption()
    {
        selectedOption ++;

        if(selectedOption >= characterDB.CharacterCount)
        {
            selectedOption = 0;
        }

        UpdateCharacter(selectedOption);
    }

    public void BackOption()
    {
        selectedOption--;

        if(selectedOption < 0)
        {
            selectedOption = characterDB.CharacterCount - 1;
        }

        UpdateCharacter(selectedOption);
    }

    //Update character sprite and character name
    private void UpdateCharacter(int selectedOption)
    {
        //Character charaacter = characterDB.GetCharacter(selectedOption);
        //artworkSprite.sprite = character.characterSprite;
        //nameText.text = character.characterName; 
    }
}
