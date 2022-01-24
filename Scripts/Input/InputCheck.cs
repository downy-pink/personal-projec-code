using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TagHolder;

namespace input
{
    //모든 입력의 반응을 체크해주는 클래스
    public class InputCheck : MonoBehaviour
    {

        [SerializeField]InputBroadCaster inputso = default;
        Vector2 moveInput;

        // Update is called once per frame
        void Update()
        {
            MouseMoveInput();
            MoveInput();
            JumpInput();
            AttackInput();
            MouseRightKey();
            EvasionInput();
            Interaction();
            QInput();
            EInput();
            RInput();

        }

        void RInput()
        {
            if (Input.GetKeyDown(KeyCode.R))
                inputso.OnRInput(true);
            else if (Input.GetKeyUp(KeyCode.R))
                inputso.OnRInput(false);
        }

        void EInput()
        {
            if (Input.GetKeyDown(KeyCode.E))
                inputso.OnEInput(true);
            else if (Input.GetKeyUp(KeyCode.E))
                inputso.OnEInput(false);
        }

        void QInput()
        {
            if (Input.GetKeyDown(KeyCode.Q))
                inputso.OnQInput(true);
            else if (Input.GetKeyUp(KeyCode.Q))
                inputso.OnQInput(false);
        }

        void EvasionInput()
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
                inputso.OnEvasionKey(true);
            else if (Input.GetKeyUp(KeyCode.LeftShift))
                inputso.OnEvasionKey(false);
        }

        void MouseRightKey()
        {
            if (Input.GetMouseButtonDown(TagHolder.KeyInput.MOUSE_RIGHT))
                inputso.OnRightMouseKey(true);
            else if (Input.GetMouseButtonUp(TagHolder.KeyInput.MOUSE_RIGHT))
                inputso.OnRightMouseKey(false);
        }
        
        void MouseMoveInput()
        {
            Vector2 _mouseMoveInput = new Vector2(Input.GetAxisRaw(TagHolder.KeyInput.MOUSE_X),
                  -Input.GetAxisRaw(TagHolder.KeyInput.MOUSE_Y));
            if (_mouseMoveInput != Vector2.zero)
                inputso.OnRotateCamera(_mouseMoveInput);
        }

        void MoveInput()
        {
            Vector2 _moveInput = new Vector2(Input.GetAxisRaw(TagHolder.KeyInput.HORIZONTAL), Input.GetAxisRaw(TagHolder.KeyInput.VERTICAL));
            if (_moveInput != Vector2.zero)
            {
                inputso.OnMove(_moveInput);
                moveInput = _moveInput;
            }
            else if (_moveInput == Vector2.zero && moveInput != Vector2.zero)
                inputso.OnMove(_moveInput);
        }

        void JumpInput()
        {
            if (Input.GetKeyDown(KeyCode.Space))
                inputso.OnJump(true);
            else if (Input.GetKeyUp(KeyCode.Space))
                inputso.OnJump(false);
        }

        void AttackInput()
        {
            if (Input.GetMouseButtonDown(0))
                inputso.OnAttack(true);
        }

        void Interaction()
        {
            if (Input.GetKeyDown(KeyCode.G))
                inputso.OnInterAction();
        }
    }

}
