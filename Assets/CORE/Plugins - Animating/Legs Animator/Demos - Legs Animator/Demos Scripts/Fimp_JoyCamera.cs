using UnityEngine;

namespace FIMSpace.Basics
{
    [DefaultExecutionOrder(10000)]
    public class Fimp_JoyCamera : MonoBehaviour
    {
        public Transform FollowObject;
        public float HeightOffset = 2f;
        public float DistanceOffset = 7f;
        public float SideOffset = 0f;

        [Space(5)]
        public Fimp_JoystickInput joystickInput;
        public Vector2 VerticalClamp = new Vector2(-40, 40);

        [Space(5)]
        [Range(0f, 1f)] public float FollowSpeed = 0.9f;
        [Range(0f, 1f)] public float RotationSpeed = 0.9f;

        [Space(5)]
        public EControl MouseControl = EControl.None;
        public float MouseControlSensitivity = 1f;

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
        }

        private void LateUpdate()
        {
            if (FollowObject == null) return;

            HandleMouseInput();
            HandleJoystickInput();

            ApplyRotation();
            ApplyPosition();
        }

        private void HandleMouseInput()
        {
            if (MouseControl == EControl.LockCursor)
            {
                if (Cursor.visible || Cursor.lockState == CursorLockMode.None)
                    SwitchLockCursor(false);

                if (Input.GetMouseButtonDown(2)) SwitchLockCursor(true);
                if (Input.GetKey(KeyCode.Escape) || Input.GetKey(KeyCode.Tab)) SwitchLockCursor(false);

                if (Cursor.lockState == CursorLockMode.Locked)
                {
                    float sensitivity = ((Screen.width + Screen.height) / 2f) * 0.02f * MouseControlSensitivity;
                    targetSphericalRot.x -= Input.GetAxis("Mouse Y") * sensitivity * joystickInput.ValuePower * joystickInput.ScaleOutput.x;
                    targetSphericalRot.y += Input.GetAxis("Mouse X") * sensitivity * joystickInput.ValuePower * joystickInput.ScaleOutput.y;
                }
            }
            else if (MouseControl == EControl.OnRMBHold)
            {
                if (Input.GetMouseButton(1) || Input.GetMouseButton(2))
                {
                    float sensitivity = ((Screen.width + Screen.height) / 2f) * 0.02f * MouseControlSensitivity;
                    targetSphericalRot.x -= Input.GetAxis("Mouse Y") * sensitivity;
                    targetSphericalRot.y += Input.GetAxis("Mouse X") * sensitivity;
                }
            }
        }

        private void HandleJoystickInput()
        {
            targetSphericalRot.x -= joystickInput.OutputValue.y;
            targetSphericalRot.y += joystickInput.OutputValue.x;

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
