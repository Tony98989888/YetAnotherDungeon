using UnityEngine;

[CreateAssetMenu(fileName = "NewCharacterAttributes", menuName = "Character System/CharacterAttributes")]
public class CharacterAttributes : ScriptableObject
{
    public int BaseHealth;
    public int BaseAttack;
    public int BaseDefense;
    public int BaseSpeed;

    public float HealthGrowth; // 每级增长的生命值
    public float AttackGrowth; // 每级增长的攻击力
    public float DefenseGrowth; // 每级增长的防御力
    public float SpeedGrowth; // 每级增长的速度

    // 根据等级计算最终属性
    public int CalculateHealth(int level)
    {
        return BaseHealth + Mathf.FloorToInt(HealthGrowth * (level - 1));
    }

    public int CalculateAttack(int level)
    {
        return BaseAttack + Mathf.FloorToInt(AttackGrowth * (level - 1));
    }

    public int CalculateDefense(int level)
    {
        return BaseDefense + Mathf.FloorToInt(DefenseGrowth * (level - 1));
    }

    public int CalculateSpeed(int level)
    {
        return BaseSpeed + Mathf.FloorToInt(SpeedGrowth * (level - 1));
    }
}