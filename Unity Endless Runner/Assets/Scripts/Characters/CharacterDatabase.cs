using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]// tell unity to make it possible to create this object through "CreateAssetMenu" 
public class CharacterDatabase : ScriptableObject
{
    public Character[] character;

    public int CharacterCount//count character 
    {
        get
        {
            return character.Length;
        }
    }

    public Character GetCharacter(int index)
    {// retrieve the selected character information
        return character[index];
    }
}
