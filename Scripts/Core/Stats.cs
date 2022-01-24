using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Config;
using DG.Tweening;
using Actors;
using SonVectorMath;
using UnityEngine.Events;

namespace Core
{
    //캐릭터 상태 정보, 상태를 관리
    public class Stats : MonoBehaviour
    {
        [SerializeField] HpConfigSO hpSO;
        [SerializeField] StaminaConfig staminaSO;
        [SerializeField] float staminaRegeneration; //초당 스태미나 재생
        [SerializeField] GameEvents gameEventsSO;
        [SerializeField] UIEventsSO uiEventsSO;

        float hp;
        float stamina;

        // Start is called before the first frame update
        void Start()
        {
            if(hpSO != null)
            //hp설정
            hp = hpSO.maxHp;
            if (staminaSO != null)
                //stamina설정
                stamina = staminaSO.maxStamina;

        }

        private void Update()
        {
            //Debug.Log(stamina);
            if (stamina + staminaRegeneration * Time.deltaTime <= 100)
            {
                stamina += staminaRegeneration * Time.deltaTime;
                if (uiEventsSO != null)
                    uiEventsSO.OnStaminaEvent(stamina);
            }
        }


        public void staminaConsume(float _staminaConsume)
        {
            if (stamina - _staminaConsume <= 0)
            {
                stamina = 0;
                if(uiEventsSO != null)
                uiEventsSO.OnStaminaEvent(stamina);
            }
            else
            {
                stamina -= _staminaConsume;
                if (uiEventsSO != null)
                    uiEventsSO.OnStaminaEvent(stamina);
            }
        }

        public void DeacreaseHP(float _hp)
        {
            hp -= _hp;
            if (uiEventsSO != null)
                uiEventsSO.OnHpChangeEvent(hp);
        }



        public void InCreaseHP(float _hp)
        {
            if (hp + _hp > hpSO.maxHp)
                hp = hpSO.maxHp;
            else
            {
                hp += _hp;
                gameEventsSO.OnUsedPotionEvent();
            }
            if (uiEventsSO != null)
                uiEventsSO.OnHpChangeEvent(hp);
        }

        public float GetHP()
        {
            return hp;
        }

        public float GetStamina()
        {
            return stamina;
        }
    }

}
