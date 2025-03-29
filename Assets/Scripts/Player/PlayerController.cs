using PPJam.Player;
using PPJam.SO;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

namespace PPJam.Payer
{
    public record CarriedObject
    {
        public GameObject GameObject;
        public ObjectType Type;

        public string RepairKey;
    }

    public enum ObjectType
    {
        Drone,
        Repair
    }

    public class PlayerController : MonoBehaviour
    {
        [SerializeField]
        private PlayerInfo _info;

        [SerializeField]
        private Transform _handsTransform;

        [SerializeField]
        private Transform _head;
        private float _headRotation;

        private CharacterController _controller;
        private bool _isSprinting;
        private float _verticalSpeed;

        private Vector2 _mov;

        private List<IInteractable> _interactions = new();

        private CarriedObject _carriedObject;
        public CarriedObject CarriedObject
        {
            set
            {
                _carriedObject = value;
                if (value != null)
                {
                    _carriedObject.GameObject.transform.parent = _handsTransform;
                    _carriedObject.GameObject.transform.localPosition = Vector3.zero;
                }
            }
            get => _carriedObject;
        }

        public void EmptyHands()
        {
            Destroy(CarriedObject.GameObject);
            CarriedObject = null;
        }

        private void Awake()
        {
            _controller = GetComponent<CharacterController>();

            Cursor.lockState = CursorLockMode.Locked;

            var tArea = GetComponentInChildren<TriggerArea>();
            tArea.OnTriggerEnterEvent.AddListener((Collider c) =>
            {
                if (c.TryGetComponent<IInteractable>(out var i))
                {
                    _interactions.Add(i);
                }
            });
            tArea.OnTriggerExitEvent.AddListener((Collider c) =>
            {
                if (c.gameObject.TryGetComponent<IInteractable>(out var i))
                {
                    _interactions.RemoveAll(x => x.GameObject.GetInstanceID() == i.GameObject.GetInstanceID());
                }
            });
        }

        private void Update()
        {
            if (!_controller.enabled)
                return;

            var pos = _mov;
            Vector3 desiredMove = transform.forward * pos.y + transform.right * pos.x;

            // Get a normal for the surface that is being touched to move along it
            Physics.SphereCast(transform.position, _controller.radius, Vector3.down, out RaycastHit hitInfo,
                               _controller.height / 2f, Physics.AllLayers, QueryTriggerInteraction.Ignore);
            desiredMove = Vector3.ProjectOnPlane(desiredMove, hitInfo.normal).normalized;

            Vector3 moveDir = Vector3.zero;
            moveDir.x = desiredMove.x * _info.ForceMultiplier * (_isSprinting ? _info.SpeedRunningMultiplicator : 1f);
            moveDir.z = desiredMove.z * _info.ForceMultiplier * (_isSprinting ? _info.SpeedRunningMultiplicator : 1f);

            if (_controller.isGrounded && _verticalSpeed < 0f) // We are on the ground and not jumping
            {
                moveDir.y = -.1f; // Stick to the ground
                _verticalSpeed = -_info.GravityMultiplicator;
            }
            else
            {
                // We are currently jumping, reduce our jump velocity by gravity and apply it
                _verticalSpeed += Physics.gravity.y * _info.GravityMultiplicator * Time.deltaTime;
                moveDir.y += _verticalSpeed;
            }

            var p = transform.position;
            _controller.Move(moveDir * _info.MovementSpeed * Time.deltaTime);
        }

        private void OnGUI()
        {
#if UNITY_EDITOR
            GUI.Label(new Rect(10f, 10f, 400f, 400f), $"Interaction count: {_interactions.Count}");
#endif
        }

        public void OnMovement(InputAction.CallbackContext value)
        {
            _mov = value.ReadValue<Vector2>().normalized;
        }

        public void OnLook(InputAction.CallbackContext value)
        {
            var rot = value.ReadValue<Vector2>();

            transform.rotation *= Quaternion.AngleAxis(rot.x * _info.HorizontalLookMultiplier, Vector3.up);

            _headRotation -= rot.y * _info.VerticalLookMultiplier; // Vertical look is inverted by default, hence the -=

            _headRotation = Mathf.Clamp(_headRotation, -89, 89);
            _head.transform.localRotation = Quaternion.AngleAxis(_headRotation, Vector3.right);
        }

        public void OnJump(InputAction.CallbackContext value)
        {
            if (_controller.isGrounded)
            {
                _verticalSpeed = _info.JumpForce;
            }
        }

        public void OnSprint(InputAction.CallbackContext value)
        {
            _isSprinting = value.ReadValueAsButton();
        }

        public void OnInteract(InputAction.CallbackContext value)
        {
            if (value.phase == InputActionPhase.Started && _interactions.Any() && _interactions[0].CanInteract(this))
            {
                _interactions[0].Interact(this);
            }
        }
    }
}
