using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Core;
using DG.Tweening;
using Config;

namespace UI
{
    //상태의 시각화
    public class UIstats : MonoBehaviour
    {
        [SerializeField] Image image;
        [SerializeField] UIEventsSO uiEventsSO;
        [SerializeField] bool hpMode;
        [SerializeField] bool staminaMode;
        [SerializeField] HpConfigSO hpConfigSO;
        [SerializeField] StaminaConfig staminaConfigSO;
        private void OnEnable()
        {
            if(hpMode)
            uiEventsSO.hpChangeEvent += OnStatsUpdate;
            if(staminaMode)
                uiEventsSO.staminaChangeEvent += OnStatsUpdate;
        }

        private void OnDisable()
        {
            if (hpMode)
                uiEventsSO.hpChangeEvent -= OnStatsUpdate;
            if (staminaMode)
                uiEventsSO.staminaChangeEvent -= OnStatsUpdate;
        }

        void OnStatsUpdate(float _stat)
        {
            if(hpMode)
            image.DOFillAmount(_stat/ hpConfigSO.maxHp, 0.35f);
            if(staminaMode)
                image.DOFillAmount(_stat / staminaConfigSO.maxStamina, 0.35f);
        }
    }
}

