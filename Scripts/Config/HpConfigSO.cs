using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Config
{
    [CreateAssetMenu(fileName = "HpConfig", menuName = "EntityConfig/Hp Config")]
    public class HpConfigSO : ScriptableObject
    {
        [Tooltip("Hp¼³Á¤")]
        [SerializeField] private int _maxHp;

        [HideInInspector] public int maxHp => _maxHp;
    }
}

