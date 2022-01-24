using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class UIWindMoveSkill : UISkill
    {
        public override void EnrollSkillCoolTime()
        {
            uiEventsSO.windMoveCoolTimeEvent += OnSkillCoolTime;
        }

        public override void ReleaseSkillCoolTime()
        {
            uiEventsSO.windMoveCoolTimeEvent -= OnSkillCoolTime;
        }

    }
}

