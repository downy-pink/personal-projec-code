using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using input;
using SonVectorMath;
using Helper;
using Core;

namespace SonCamera
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] GameEvents gameEventsSO;
        [SerializeField] InputBroadCaster inputBroadCaster = default;
        [SerializeField] TransformValueSO cameraT;
        [Range(0f, 90f)]
        [SerializeField] float upperVerticalLimit = 60f;
        [Range(0f, 90f)]
        [SerializeField] float lowerVerticalLimit = 60f;

        [Range(1f, 50f)]
        [SerializeField] float cameraSmoothingFactor = 30f; 
        [SerializeField] float cameraSpeed = 300f;

        //카메라의 수직, 수평성분
        Vector3 forwardDirection;
        Vector3 upDirection;

        float currentXAngle;
        float currentYAngle;

        float oldHorizontalInput;
        float oldVerticalInput;

        [SerializeField] float mouseSensitivity = 0.01f;
        Vector2 cameraMovement;

        bool cameraMovementLock = false;
        // Start is called before the first frame update
        void Start()
        {
            currentXAngle = transform.localRotation.eulerAngles.x;
            currentYAngle = transform.localRotation.eulerAngles.y;
            RotateCamera(0, 0);
            Cursor.lockState = CursorLockMode.Locked;
            gameEventsSO.conversationEvent += OnConversation;
            gameEventsSO.conversationExitEvent += OnConversationExit;
            
        }

        void OnConversation()
        {
            Cursor.lockState = CursorLockMode.None;
            inputBroadCaster.cameraMoveEvent -= OnCameraMove;
        }

        void OnConversationExit()
        {
            Cursor.lockState = CursorLockMode.Locked;
            inputBroadCaster.cameraMoveEvent += OnCameraMove;
        }

        private void OnEnable()
        {
            inputBroadCaster.cameraMoveEvent += OnCameraMove;
            cameraT.Variable = transform;
        }

        private void OnDisable()
        {
            inputBroadCaster.cameraMoveEvent -= OnCameraMove;
        }

        // Update is called once per frame
        void Update()
        {
            RotateCamera(cameraMovement.x, cameraMovement.y);
        }



        void RotateCamera(float _newHorizontalInput, float _newVerticalInput)
        {
            oldHorizontalInput = Mathf.Lerp(oldHorizontalInput, _newHorizontalInput, Time.deltaTime * cameraSmoothingFactor);
            oldVerticalInput = Mathf.Lerp(oldVerticalInput, _newVerticalInput, Time.deltaTime * cameraSmoothingFactor);

            //인풋자체에 deltatime이 곱해져있음
            currentXAngle += oldVerticalInput * cameraSpeed /** Time.deltaTime*/;
            currentYAngle += oldHorizontalInput * cameraSpeed /** Time.deltaTime*/;

            currentXAngle = Mathf.Clamp(currentXAngle, -upperVerticalLimit, lowerVerticalLimit);
            UpdateRotation();
        }

        protected void UpdateRotation()
        {
            transform.localRotation = Quaternion.Euler(new Vector3(0, currentYAngle, 0));

            ////3인칭 컨트롤러는 수평과 수직성분을 나눠줄 필요
            //forwardDirection = transform.forward;
            //upDirection = transform.up;

            transform.localRotation = Quaternion.Euler(new Vector3(currentXAngle, currentYAngle, 0));
            cameraMovement = Vector2.zero;
        }


        //보류
        public void RotateTowardPosition(Vector3 _position, float _lookSpeed)
        {
            Vector3 _direction = (_position - transform.position);

            RotateTowardDirection(_direction, _lookSpeed);
        }

        public void RotateTowardDirection(Vector3 _direction, float _lookSpeed)
        {
            _direction.Normalize();

            
            Vector3 _currentLookVector = transform.forward;

            float _xAngleDifference = VectorMath.GetAngle(new Vector3(0f, _currentLookVector.y, 1f), new Vector3(0f, _direction.y, 1f), Vector3.right);

            _currentLookVector.y = 0f;
            _direction.y = 0f;
            float _yAngleDifference = VectorMath.GetAngle(_currentLookVector, _direction, Vector3.up);

            Vector2 _currentAngles = new Vector2(currentXAngle, currentYAngle);
            Vector2 _angleDifference = new Vector2(_xAngleDifference, _yAngleDifference);

            float _angleDifferenceMagnitude = _angleDifference.magnitude;
            if (_angleDifferenceMagnitude == 0f)
                return;
            Vector2 _angleDifferenceDirection = _angleDifference / _angleDifferenceMagnitude;

            if (_lookSpeed * Time.deltaTime > _angleDifferenceMagnitude)
            {
                _currentAngles += _angleDifferenceDirection * _angleDifferenceMagnitude;
            }
            else
                _currentAngles += _angleDifferenceDirection * _lookSpeed * Time.deltaTime;

            currentYAngle = _currentAngles.y;

            currentXAngle = Mathf.Clamp(_currentAngles.x, -upperVerticalLimit, lowerVerticalLimit);

            UpdateRotation();
        }

        //마우스이동으로만 적용되는 카메라 이동 수치, 이벤트함수에 등록
        void OnCameraMove(Vector2 _cameraMovement)
        {

            if (cameraMovementLock)
                return;

            if (Time.timeScale > 0f /*&& Time.deltaTime > 0f*/)
            {
                //_cameraMovement /= Time.deltaTime;
                _cameraMovement *= Time.timeScale;
            }
            else
                _cameraMovement = Vector2.zero;

            _cameraMovement.x *= mouseSensitivity;
            _cameraMovement.y *= mouseSensitivity;
            cameraMovement = _cameraMovement;
        }
    }

}
