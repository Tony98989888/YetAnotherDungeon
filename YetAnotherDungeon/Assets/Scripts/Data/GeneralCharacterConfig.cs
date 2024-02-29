using UnityEngine;


[CreateAssetMenu(fileName = "NewCharacterConfig", menuName = "Character/GeneralCharacterConfig")]
public class GeneralCharacterConfig : ScriptableObject
{
    public Sprite CharacterImage;
    public int Attack;
    public int Health;
    public int Defense;
    public int Speed;
    public string Name;
}
