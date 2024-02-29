using UnityEngine;

[CreateAssetMenu(fileName = "NewHealSkill", menuName = "Skill System/HealSkill")]
public class HealSkill : Skill
{
    public override void CastSkill(Character user, Character target)
    {
        base.CastSkill(user, target);
        Debug.Log($"{user.name} Cast Skill {this.GetType()} to {target.Name}");
    }
}
