using Settings;
using UnityEngine;
using YG;
using Zenject;

namespace FIMSpace.Basics
{
    [DefaultExecutionOrder(10000)]
    public class Fimp_JoyCamera : MonoYandex
    {
        [Inject] 
        private GlobalSettings _globalSettings;
        private bool _isMobile = false;
        
        public Transform FollowObject;
        public float HeightOffset = 2f;
        public float DistanceOffset = 7f;
        public float SideOffset = 0f;

        [Space(5)]
        public Joystick joystickInput;
        public Vector2 VerticalClamp = new Vector2(-40, 40);

        [Space(5)]
        [UnityEngine.Range(0f, 1f)] public float FollowSpeed = 0.9f;
        [UnityEngine.Range(0f, 1f)] public float RotationSpeed = 0.9f;

        [Space(5)]
        public EControl MouseControl = EControl.None;
        public float DefaultMouseSensitivity = 1f;
        public float DefaultJoystickSensitivity = 2;

        public enum EControl
        {
            None,
            LockCursor,
            OnRMBHold
        }

        Vector3 _sd_camPos = Vector3.zero;
        Vector2 _sd_sphRot = Vector2.zero;

        Vector3 followPosition = Vector3.zero;

        private Vector2 targetSphericalRot = Vector2.zero;

        public Vector2 SetTargetSphericalRot
        {
            get { return targetSphericalRot; }
            set { targetSphericalRot = value; }
        }

        private void Start()
        {
            if (FollowObject == null) return;

            targetSphericalRot = transform.eulerAngles;
            followPosition = FollowObject.position;
            SwitchLockCursor(true);
        }
        
        protected override void OnSDK()
        {
            _isMobile = _globalSettings.ForceMobile || YandexGame.EnvironmentData.isMobile;
        }

        private void LateUpdate()
        {
            if (FollowObject == null) return;

            if (_isMobile) HandleJoystickInput();
            else HandleMouseInput();

            ApplyRotation();
            ApplyPosition();
        }

        private void HandleMouseInput()
        {
            if (MouseControl == EControl.LockCursor)
            {
                // if (Cursor.visible || Cursor.lockState == CursorLockMode.None)
                //     SwitchLockCursor(false);
                // if (Input.GetMouseButtonDown(2)) SwitchLockCursor(true);
                // if (Input.GetKey(KeyCode.Escape) || Input.GetKey(KeyCode.Tab)) SwitchLockCursor(false);

                if (Cursor.lockState == CursorLockMode.Locked)
                {
                    float sensitivity = ((Screen.width + Screen.height) / 2f) * 0.02f * DefaultMouseSensitivity;
                    targetSphericalRot.x -= Input.GetAxis("Mouse Y") * sensitivity;
                    targetSphericalRot.y += Input.GetAxis("Mouse X") * sensitivity;
                }
            }
            else if (MouseControl == EControl.OnRMBHold)
            {
                if (Input.GetMouseButton(1) || Input.GetMouseButton(2))
                {
                    float sensitivity = ((Screen.width + Screen.height) / 2f) * 0.02f * DefaultMouseSensitivity;
                    targetSphericalRot.x -= Input.GetAxis("Mouse Y") * sensitivity;
                    targetSphericalRot.y += Input.GetAxis("Mouse X") * sensitivity;
                }
            }
            
            targetSphericalRot.x = Mathf.Clamp(targetSphericalRot.x, VerticalClamp.x, VerticalClamp.y);
        }

        private void HandleJoystickInput()
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            
            targetSphericalRot.x -= joystickInput.Direction.y * DefaultJoystickSensitivity;
            targetSphericalRot.y += joystickInput.Direction.x * DefaultJoystickSensitivity;

            targetSphericalRot.x = Mathf.Clamp(targetSphericalRot.x, VerticalClamp.x, VerticalClamp.y);
        }

        private void ApplyRotation()
        {
            Quaternion targetRotation = Quaternion.Euler(targetSphericalRot.x, targetSphericalRot.y, 0f);

            if (RotationSpeed >= 1f)
            {
                transform.rotation = targetRotation;
            }
            else
            {
                float lerpFactor = Mathf.Lerp(0.2f, 0.005f, RotationSpeed);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, lerpFactor);
            }
        }

        private void ApplyPosition()
        {
            followPosition = FollowObject.position;
            followPosition += Vector3.up * HeightOffset;
            followPosition += transform.right * SideOffset;
            followPosition -= transform.forward * DistanceOffset;

            if (FollowSpeed >= 1f)
            {
                transform.position = followPosition;
            }
            else
            {
                float lerpFactor = Mathf.Lerp(0.5f, 0.02f, FollowSpeed);
                transform.position = Vector3.Lerp(transform.position, followPosition, lerpFactor);
            }
        }

        private void SwitchLockCursor(bool lockCursor)
        {
            Cursor.lockState = lockCursor ? CursorLockMode.Locked : CursorLockMode.None;
            Cursor.visible = !lockCursor;
        }
    }
}
