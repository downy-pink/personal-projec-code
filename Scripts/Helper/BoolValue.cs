using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Helper
{
    [CreateAssetMenu(fileName = "BoolValue", menuName = "Helper/BoolValue")]
    public class BoolValue : ScriptableObject
    {
        //bool null»Æ¿Œ
        [HideInInspector] public bool isSet = false;

        private bool _variable;
        public bool Variable
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

