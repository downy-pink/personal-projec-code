using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Helper
{
    [CreateAssetMenu(fileName = "TransformValue", menuName = "Helper/TransformValue")]
    //객체 트랜스폼 접근 용도
    public class TransformValueSO : ScriptableObject
    {
        //transform null확인
        [HideInInspector] public bool isSet = false;

        private Transform _variable;
        public Transform Variable
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
