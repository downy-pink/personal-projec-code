using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Config
{
    [CreateAssetMenu(fileName = "EvasionStaminaConsumeConfig", menuName = "EntityConfig/EvasionStaminaConsume Config")]
    public class EvasionStaminaConsumeConfigSO : ScriptableObject
    {
        [Tooltip("Hp¼³Á¤")]
        [SerializeField] private int _evasionConsume;

        [HideInInspector] public int evasionConsume => _evasionConsume;
    }

}
