using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Database", menuName = "Database/Character")]
public class CharacterDatabase : ScriptableObject
{
    public List<CharacterData> characters;

    public CharacterData GetByID(int id)
    {
        return characters.Find(c => c.id == id);
    }
}