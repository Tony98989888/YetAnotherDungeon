using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    public static CombatManager Instance { get; private set; }


    public readonly List<Character> Allies = new List<Character>(); // 存储所有友军角色
    public readonly List<Character> Enemies = new List<Character>(); // 存储所有敌人角色
    private int RoundCounter = 0; // 回合计数器

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // 开始战斗
    public void StartBattle(List<Character> allies, List<Character> enemies)
    {
        RoundCounter = 0;
        Allies.Clear();
        Enemies.Clear();

        Allies.AddRange(allies);
        Enemies.AddRange(enemies);

        StartCoroutine(BeginBattle());
    }

    // 战斗回合的协程
    IEnumerator BeginBattle()
    {
        while (!IsBattleOver())
        {
            // 执行我方角色行动
            foreach (Character ally in Allies)
            {
                if (ally.Health > 0)
                {
                    PerformAction(ally);
                    yield return new WaitForSeconds(1.0f); // 等待一段时间模拟行动
                }
            }

            // 执行敌方角色行动
            foreach (Character enemy in Enemies)
            {
                if (enemy.Health > 0)
                {
                    PerformAction(enemy);
                    yield return new WaitForSeconds(1.0f); // 等待一段时间模拟行动
                }
            }

            // 回合结束
            RoundCounter++;

            yield return null; // 等待下一帧，防止无限循环
        }

        // 战斗结束
        Debug.Log("Battle Over. " + (AllEnemiesDefeated() ? "You Win!" : "You Lose!"));
    }

    // 判断战斗是否结束
    bool IsBattleOver()
    {
        return AllAlliesDefeated() || AllEnemiesDefeated();
    }

    // 检查所有友军是否阵亡
    bool AllAlliesDefeated()
    {
        foreach (Character ally in Allies)
        {
            if (ally.Health > 0)
                return false;
        }

        return true;
    }

    // 检查所有敌人是否阵亡
    bool AllEnemiesDefeated()
    {
        foreach (Character enemy in Enemies)
        {
            if (enemy.Health > 0)
                return false;
        }

        return true;
    }

    // 执行角色行动
    void PerformAction(Character character)
    {
        // 在这里可以实现选择技能和目标的逻辑
        // 例如，优先使用冷却时间已经结束的技能
        // 按照降序排序技能，cd高的先释放
        SkillConfig skillConfigToUse = null;
        foreach (SkillConfig skill in character.Skills.OrderByDescending(x => x.CoolDown))
        {
            if (RoundCounter % skill.CoolDown == 0)
            {
                skillConfigToUse = skill;
                break; // 找到可用的技能后跳出循环
            }
        }

        if (skillConfigToUse != null)
        {
            // 假设技能需要目标，而且是攻击技能，选择第一个存活的对方角色
            Character target = character is AllyCharacter
                ? Enemies.Find(e => e.Health > 0)
                : Allies.Find(a => a.Health > 0);
            if (target != null)
            {
                skillConfigToUse.CastSkill(character, target); // 使用技能
            }
        }
        else
        {
            // 如果没有可用技能，可以进行默认行动，比如普通攻击
            // 或者是角色的其他行为，比如防御或待命
        }
    }
}

// 请注意，你的Skill类需要有一个initialCooldown属性来存储技能的初始冷却时间。
// ActivateSkill方法需要被实现，以便应用技能效果。