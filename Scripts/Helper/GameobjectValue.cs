using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Helper
{
    [CreateAssetMenu(fileName = "GameobjectValue", menuName = "Helper/GameobjectValue")]
    public class GameobjectValue : ScriptableObject
    {
        //transform null»Æ¿Œ
        [HideInInspector] public bool isSet = false;

        private GameObject _variable;
        public GameObject Variable
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
