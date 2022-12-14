using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Text type variable is a UI element
using TMPro;
using UnityEngine.SceneManagement;

public class CharacterManager : MonoBehaviour
{
    public CharacterDatabase characterDB;

    public TextMeshProUGUI nameText;
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

        //UpdateCharacter(selectedOption);
        
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
        Save();
    }

    public void BackOption()
    {
        selectedOption--;

        if(selectedOption < 0)
        {
            selectedOption = characterDB.CharacterCount - 1;
        }

        UpdateCharacter(selectedOption);
        Save();
    }

    //Update character sprite and character name
    private void UpdateCharacter(int selectedOption)
    {
        Character character = characterDB.GetCharacter(selectedOption);
        artworkSprite.sprite = character.characterSprite;
        nameText.text = character.characterName; 
    }

    private void Load()
    {
        selectedOption = PlayerPrefs.GetInt("selectedOption");
        Debug.Log("?????????????????");
        Debug.Log(selectedOption);
    }

    private void Save()
    {//save date
        PlayerPrefs.SetInt("selectedOption", selectedOption);
        //save variable to the key name
    }
    /**/
    public void ChangeScene(int sceneID)
    {
        SceneManager.LoadScene(sceneID);
    }
}
