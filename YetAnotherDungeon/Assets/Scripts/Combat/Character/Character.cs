using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public int Health;
    public int Attack;
    public int Defense;
    public string Name;
    public Sprite Avatar;
    public float Speed;
    public bool IsAlly;

    public List<Skill> Skills;
    
    // 技能
    protected virtual void UseSkill()
    {
        // 实现技能的通用逻辑
    }
    
    // 受到攻击
    public void TakeDamage(int damage)
    {
        int damageTaken = damage - Health;
        Health -= damageTaken > 0 ? damageTaken : 0;
        if (Health <= 0)
        {
            Die();
        }
    }
    
    // 死亡
    protected virtual void Die()
    {
        // 实现死亡逻辑，例如播放死亡动画，从场景中移除等
    }
}