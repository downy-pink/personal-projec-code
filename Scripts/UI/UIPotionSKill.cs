using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class UIPotionSKill : UISkill
    {
        public override void EnrollSkillCoolTime()
        {
            uiEventsSO.potionCoolTimeEvent += OnSkillCoolTime;
        }

        public override void ReleaseSkillCoolTime()
        {
            uiEventsSO.potionCoolTimeEvent -= OnSkillCoolTime;
        }
    }

}
