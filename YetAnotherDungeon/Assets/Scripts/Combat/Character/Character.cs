using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public CharacterAttributes Attributes;
    public int Level;
    public string Name;
    public Sprite Avatar;
    public bool IsAlly;
    public List<SkillConfig> Skills;

    public float Speed => Attributes.CalculateSpeed(Level);
    public int Defense => Attributes.CalculateSpeed(Level);
    public int Health => Attributes.CalculateHealth(Level);
    public int Attack => Attributes.CalculateAttack(Level);

    // 技能
    protected virtual void UseSkill()
    {
        // 实现技能的通用逻辑
    }
}