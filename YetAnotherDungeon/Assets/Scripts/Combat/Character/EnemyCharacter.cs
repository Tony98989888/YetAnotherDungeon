using System;

public class EnemyCharacter : Character
{
    // 可以添加敌人特有的属性和方法

    private void Start()
    {
        IsAlly = false;
    }

    protected override void UseSkill()
    {
        // 实现敌人特有的技能逻辑
        base.UseSkill(); // 如果需要通用逻辑，调用基类方法
    }

    protected override void Die()
    {
        // 实现敌人特有的死亡逻辑
        base.Die(); // 如果需要通用逻辑，调用基类方法
    }
}
