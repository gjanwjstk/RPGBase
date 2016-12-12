using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

[CreateAssetMenu(fileName ="New Skill", menuName ="ChoARPG_Skill", order =999)]
public class SkillTemplate : ScriptableObject 
{
    public SKILL_TYPE skillType;
    public SKILL_ATTACK_TYPE skillAttackType;
    public ENTITY_CLASS learnClassType;

    public Sprite skillImage;
    public bool learnDefault;
    [TextArea(1, 30)]
    public string tooltip;
    public SkillLevel[] levels = new SkillLevel[] { new SkillLevel() };

    public GameObject effectCastSkill;//스킬 시전 이펙트
    public GameObject effectSkillRange;//스킬 범위 지정 이펙트
    public GameObject effectHitSkill;//스킬 타격 이펙트

    [System.Serializable]
    public struct SkillLevel
    {
        //Bse Skill Data;
        public int manaCost;
        public float castTime;
        public float castRange;
        public float cooldown;
        public float aoeRadious;

        //Attack Types
        public int damage;

        //Buff Types
        public float buffTime;
        public int buffDamage;
        public int buffDefense;
        public int buffHpMax;
        public int buffMpMax;
        public int buffHeal;

        //require
        public int requiredLevel;
        public int requiredSkillPoint;

        public Projecttile projectTile;

        static Dictionary<string, SkillTemplate> skilldict = null;
        public static Dictionary<string, SkillTemplate> SkillDict
        {
            get
            {
                if (skilldict == null)
                {
                    skilldict = Resources.LoadAll<SkillTemplate>("").
                        ToDictionary(item => item.name, item =>item);
                }
                return skilldict;
            }
        }
    }
}
