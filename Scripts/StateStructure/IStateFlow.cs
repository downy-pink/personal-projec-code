using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StateStructure
{
   //�������� �����Ҷ� �� ������ �ʼ�-> �ൿ�� ���¿� ����
   interface IStateFlow
   {
        
		void OnEnter();

        
        void OnExit();
   }
}
