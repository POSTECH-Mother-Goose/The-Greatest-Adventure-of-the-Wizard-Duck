using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
using UnityEngine.InputSystem;
#endif

namespace PlayerInputs
{
    public class PlayerObject : MonoBehaviour
    {
        [SerializeField] public Block block;
        [SerializeField] float speed = 5f;
        Vector3[] destinations;
        int num = -1;

        public Vector2 look;
        [Header("Cinemachine")]
        [Tooltip("The follow target set in the Cinemachine Virtual Camera that the camera will follow")]
        public GameObject CinemachineCameraTarget;
        [Tooltip("How far in degrees can you move the camera up")]
        public float TopClamp = 70.0f;
        [Tooltip("How far in degrees can you move the camera down")]
        public float BottomClamp = -30.0f;
        [Tooltip("Additional degress to override the camera. Useful for fine tuning camera position when locked")]
        public float CameraAngleOverride = 0.0f;
        [Tooltip("For locking the camera position on all axis")]
        public bool LockCameraPosition = false;

        // cinemachine
        private float _cinemachineTargetYaw;
        private float _cinemachineTargetPitch;

        private PlayerInput _playerInput;

        private const float _threshold = 0.01f;
        private bool IsCurrentDeviceMouse => _playerInput.currentControlScheme == "KeyboardMouse";

        // Start is called before the first frame update
        void Start()
        {
            _playerInput = GetComponent<PlayerInput>();
        }

        // Update is called once per frame
        void Update()
        {
            if (num >= 0)
            {
                if (transform.position.y != destinations[num].y) // 다른 블록의 waypoint인 걸 y값이 다른 걸로 구분
                {
                    transform.position = destinations[num];
                    //Thread.Sleep((int)(0.5 / speed * 1000));    // 거리 / 속도 * ms/s
                    num++;
                }
                else
                {
                    transform.position = Vector3.MoveTowards(transform.position, destinations[num], speed * Time.deltaTime);
                    if (transform.position == destinations[num])
                    {
                        num++;
                    }
                }

                if (destinations[num] == Vector3.down)  // 마지막 destination이라는 신호
                {
                    num = -1;
                    return;
                }
            }
        }

        private void LateUpdate()
        {
            CameraRotation();
        }

        public void moveSet(Vector3[] _destinations)
        {
            destinations = _destinations;
            num = 0;
        }

        public void LookInput(Vector2 newLookDirection)
        {
            look = newLookDirection;
        }
        private static float ClampAngle(float lfAngle, float lfMin, float lfMax)
        {
            if (lfAngle < -360f) lfAngle += 360f;
            if (lfAngle > 360f) lfAngle -= 360f;
            return Mathf.Clamp(lfAngle, lfMin, lfMax);
        }

        private void CameraRotation()
        {
            // if there is an input and camera position is not fixed
            if (look.sqrMagnitude >= _threshold && !LockCameraPosition)
            {
                //Don't multiply mouse input by Time.deltaTime;
                float deltaTimeMultiplier = IsCurrentDeviceMouse ? 1.0f : Time.deltaTime;

                _cinemachineTargetYaw += look.x * deltaTimeMultiplier;
                _cinemachineTargetPitch += look.y * deltaTimeMultiplier;
            }

            // clamp our rotations so our values are limited 360 degrees
            _cinemachineTargetYaw = ClampAngle(_cinemachineTargetYaw, float.MinValue, float.MaxValue);
            _cinemachineTargetPitch = ClampAngle(_cinemachineTargetPitch, BottomClamp, TopClamp);

            // Cinemachine will follow this target
            CinemachineCameraTarget.transform.rotation = Quaternion.Euler(_cinemachineTargetPitch + CameraAngleOverride, _cinemachineTargetYaw, 0.0f);
        }
    }
}