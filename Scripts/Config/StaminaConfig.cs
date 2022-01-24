using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Config
{
    [CreateAssetMenu(fileName = "StaminaConfig", menuName = "EntityConfig/Stamina Config")]
    public class StaminaConfig : ScriptableObject
    {

        [Tooltip("Stamina����")]
        [SerializeField] private int _maxStamina;

        [HideInInspector] public int maxStamina => _maxStamina;
    }
}

