using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class UISwordSkill : UISkill
    {
        public override void EnrollSkillCoolTime()
        {
            uiEventsSO.swordSkillCoolTimeEvent += OnSkillCoolTime;
        }

        public override void ReleaseSkillCoolTime()
        {
            uiEventsSO.swordSkillCoolTimeEvent -= OnSkillCoolTime;
        }

    }
}

