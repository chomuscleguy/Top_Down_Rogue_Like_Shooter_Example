[System.Serializable]
public struct PlayerData
{
    public CharacterData character;
    public PlayerProgress progress;

    public PlayerData(CharacterData character, PlayerProgress progress)
    {
        this.character = character;
        this.progress = progress;
    }
}