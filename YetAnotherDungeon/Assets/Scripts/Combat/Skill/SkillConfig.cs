using UnityEngine;

[CreateAssetMenu(fileName = "NewSkill", menuName = "Skill System/Skill")]
public class SkillConfig : ScriptableObject
{
    public string Name; // 技能名称
    public float CoolDown; // 冷却时间

    public enum SkillType // 技能类型枚举
    {
        Attack,
        Heal,
        Dodge,
        // 可以根据需要添加更多类型
    }

    public SkillType Type; // 技能类型
    public Sprite Icon; // 技能图标

    // 技能效果的实现
    public virtual void CastSkill(Character user, Character target)
    {
    }
}