
using UnityEngine;

[CreateAssetMenu(fileName = "NewAttackSkill", menuName = "Skill System/AttackSkill")]
public class AttackSkill : Skill
{
    public override void CastSkill(Character user, Character target)
    {
        base.CastSkill(user, target);
        Debug.Log($"{user.name} Cast Skill {this.GetType()} to {target.Name}");
    }
}
