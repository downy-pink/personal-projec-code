using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StateStructure
{
   //움직임을 제어할때 과 나감은 필수-> 행동과 상태에 쓰임
   interface IStateFlow
   {
        
		void OnEnter();

        
        void OnExit();
   }
}
