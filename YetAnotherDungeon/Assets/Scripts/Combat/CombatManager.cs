using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    public List<CharacterBase> characters; // 所有参与战斗的角色
    public bool isCombatActive; // 战斗是否进行中

    private void Start()
    {
        BeginCombat();
    }

    public void BeginCombat()
    {
        // 战斗开始时，根据速度排序角色
        characters = characters.OrderByDescending(c => c.GeneralConfig.Speed).ToList();
        isCombatActive = true;
        StartCoroutine(CombatRoutine());
    }

    private IEnumerator CombatRoutine()
    {
        while(isCombatActive)
        {
            foreach (CharacterBase character in characters)
            {
                if (!isCombatActive) break; // 如果战斗结束则退出循环

                // 执行攻击
                if (character.GeneralConfig.Health > 0)
                {
                    // 这里简化处理，攻击列表中的下一个角色
                    var target = characters.FirstOrDefault(c => c != character && c.characterData.health > 0);
                    if (target != null)
                    {
                        Attack(character, target);
                    }
                }

                // 检查战斗是否结束
                if (characters.All(c => c.characterData.health <= 0) || characters.Where(c => !c.isPlayer).All(c => c.characterData.health <= 0))
                {
                    EndCombat();
                    yield break; // 停止协程
                }

                yield return new WaitForSeconds(1f); // 等待一秒，模拟攻击间隔
            }
        }
    }

    private void Attack(CharacterBase attacker, CharacterBase defender)
    {
        int damage = attacker.GeneralConfig.Attack - defender.GeneralConfig.Defense;
        damage = Mathf.Max(damage, 0); // 确保最小伤害为0

        // 应用伤害
        defender.GeneralConfig.Health -= damage;
        Debug.Log($"{attacker.GeneralConfig.Name} attacks {defender.GeneralConfig.Name} for {damage} damage.");

        // 检查防御者是否被击败
        if (defender.GeneralConfig.Health <= 0)
        {
            Debug.Log($"{defender.GeneralConfig.Name} has been defeated!");
        }
    }

    private void EndCombat()
    {
        isCombatActive = false;
        Debug.Log("Combat has ended.");
        // 在这里可以添加其他战斗结束后的逻辑，比如游戏状态切换、奖励发放等
    }
}