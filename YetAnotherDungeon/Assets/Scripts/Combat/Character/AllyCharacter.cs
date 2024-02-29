using System;

public class AllyCharacter : Character
{
    private void Start()
    {
        IsAlly = true;
    }

    // 可以添加友军特有的属性和方法

    protected override void UseSkill()
    {
        // 实现友军特有的技能逻辑
        base.UseSkill(); // 如果需要通用逻辑，调用基类方法
    }

    protected override void Die()
    {
        // 实现友军特有的死亡逻辑
        base.Die(); // 如果需要通用逻辑，调用基类方法
    }
}
