using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Core
{
    [CreateAssetMenu(fileName = "GameEventsSO", menuName = "Core/GameEvents")]
    public class GameEvents : ScriptableObject
    {
        public UnityAction deathBossEvent = delegate { };
        public UnityAction deathMonsterEvent = delegate { };
        public UnityAction conversationEvent = delegate { };
        public UnityAction conversationExitEvent = delegate { };
        public UnityAction usedPotionEvent = delegate { };
        public UnityAction usedSkillEvent = delegate { };
        public UnityAction hitEvent = delegate { };

        private void OnEnable()
        {
            deathBossEvent = null;
            deathMonsterEvent = null;
            conversationEvent = null;
            conversationExitEvent = null;
            usedPotionEvent = null;
            usedSkillEvent = null;
            hitEvent = null;
        }

        public void OnDeathBossEvent()
        {
            if (deathBossEvent != null)
                deathBossEvent.Invoke();
        }

        public void OnhitEvent()
        {
            if (hitEvent != null)
                hitEvent.Invoke();
        }

        public void OnUsedSkillEvent()
        {
            if (usedSkillEvent != null)
                usedSkillEvent.Invoke();
        }

        public void OnUsedPotionEvent()
        {
            if (usedPotionEvent != null)
                usedPotionEvent.Invoke();
        }

        public void OnConversationEvent()
        {
            if (conversationEvent != null)
                conversationEvent.Invoke();
        }

        public void OnConversationExitEvent()
        {
            if (conversationExitEvent != null)
                conversationExitEvent.Invoke();
        }

        public void OnDeathMonsterEvent()
        {
            Debug.Log("온데스 호출");
            if (deathMonsterEvent != null)
                deathMonsterEvent.Invoke();
        }

    }
}

