using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Helper
{
    [CreateAssetMenu(fileName = "FloatValue", menuName = "Helper/FloatValue")]
    public class FloatValue : ScriptableObject
    {
        //transform null»Æ¿Œ
        [HideInInspector] public bool isSet = false;

        private float _variable;
        public float Variable
        {
            get { return _variable; }
            set
            {
                _variable = value;
                isSet = _variable != null;
            }
        }

        public void OnDisable()
        {
            _variable = default;
            isSet = false;
        }
    }
}

