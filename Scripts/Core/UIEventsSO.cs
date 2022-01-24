 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Core
{
    [CreateAssetMenu(fileName = "UIEventsSO", menuName = "Core/UIEvents")]
    public class UIEventsSO : ScriptableObject
    {
        public UnityAction dialogueEvents = delegate { };
        public UnityAction interactionEvents = delegate { };
        public UnityAction<int> dialogueInteractionEvents = delegate { };
        public UnityAction<float> hpChangeEvent = delegate { };
        public UnityAction<float> staminaChangeEvent = delegate { };
        public UnityAction<float> potionCoolTimeEvent = delegate { };
        public UnityAction<float> windMoveCoolTimeEvent = delegate { };
        public UnityAction<float> swordSkillCoolTimeEvent = delegate { };

        public void OnWindMoveCoolTimeEvent(float _coolTime)
        {
            if (windMoveCoolTimeEvent != null)
                windMoveCoolTimeEvent.Invoke(_coolTime);
        }

        public void OnSwordSkillCoolTimeEvent(float _coolTime)
        {
            if (swordSkillCoolTimeEvent != null)
                swordSkillCoolTimeEvent.Invoke(_coolTime);
        }

        public void OnPotionCoolTime(float _coolTime)
        {
            if (potionCoolTimeEvent != null)
                potionCoolTimeEvent.Invoke(_coolTime);
        }

        public void OnStaminaEvent(float _stamina)
        {
            if (staminaChangeEvent != null)
                staminaChangeEvent.Invoke(_stamina);
        }

        public void OnHpChangeEvent(float _hp)
        {
            if (hpChangeEvent != null)
                hpChangeEvent.Invoke(_hp);
        }


        public void OnDialogueInteractionEvents(int _id)
        {
            if (dialogueInteractionEvents != null)
                dialogueInteractionEvents.Invoke(_id);
        }



        public void OnInteractionEvents()
        {
            if (interactionEvents != null)
                interactionEvents.Invoke();
        }

        public void OnDialogueEvents()
        {
            if(dialogueEvents != null)
            dialogueEvents.Invoke();
        }
    }
}

