using System;
using UnityEngine;
using UnityEditor;
using Interactable.Runtime;
using MyCursor.Runtime;

namespace PlayerController.Runtime
{
    public class PlayerControllerFPS : MonoBehaviour
    {
        #region Exposed

        [Header("Variables")]
        [SerializeField, Min(0.0f)]
        private float _moveSpeed = 5.0f;

        [SerializeField, Min(0.0f)]
        private float _rotationSpeed = 5.0f;

        [SerializeField]
        private Vector2 _verticalCameraRange = new Vector2(-89.0f, 89.0f);

        [SerializeField, Min(0.0f)]
        private float _interactionRange = 5.0f;

        [SerializeField]
        private LayerMask _interactionLayer;

        [Header("Reference")]
        [SerializeField]
        private Transform _transform;

        [SerializeField]
        private Transform _cameraTransform;

        #endregion


        #region Properties
        public float MoveSpeed
        {
            get => _moveSpeed;

            set
            {
                _moveSpeed = (value < 0.0f) ? 0.0f : value;   
            }
        }

        public float RotationSpeed
        {
            get => _rotationSpeed;

            set
            {
                _rotationSpeed = (value < 0.0f) ? 0.0f : value;
            }
        }

        public float InteractionRange
        {
            get => _interactionRange;

            set
            {
                _interactionRange = (value < 0.0f) ? 0.0f : value;
            }
        }

        #endregion


        #region Unity API

        private void OnValidate() 
        {
            MoveSpeed = _moveSpeed;
            RotationSpeed = _rotationSpeed;
            InteractionRange = _interactionRange;
        }

        private void Awake() 
        {
            CursorManager.UpdateCursor();    
        }

        private void Update() 
        {
            Rotate();
            TryInteract();  
            TryToggleCursor();  
        }

        private void FixedUpdate() 
        {
            ScanInteractable();
            Move();    
        }

        #endregion


        #region Main

        private void Move()
        {
            if(CursorManager.IsVisible) return;

            ComputeDirection();

            _transform.Translate(_direction * _moveSpeed * Time.fixedDeltaTime, Space.Self);
        }

        private void Rotate()
        {
            if(CursorManager.IsVisible) return;

            ComputeRotation();

            _transform.Rotate(_bodyRotation * _rotationSpeed);
            RotateCameraClamped();            
        }

        private void ScanInteractable()
        {
            var ray = new Ray(_cameraTransform.position, _cameraTransform.forward);
                
            if(Physics.Raycast(ray, out RaycastHit hit, _interactionRange, _interactionLayer))
            {
                _targetInteractable = hit.collider.gameObject.GetComponent<IInteractable>();
            }
            else
            {
                _targetInteractable = null;
            }
        }

        private void TryInteract()
        {
            if(_targetInteractable != null && Input.GetKeyDown(KeyCode.E))
            {
                Interact();
            }
        }

        private void Interact()
        {
            _targetInteractable.Interacted(this);
        }

        private void TryToggleCursor()
        {
            if(Input.GetKeyDown(KeyCode.A))
            {
                CursorManager.ToggleCursor();
            }
        }

        #endregion


        #region Utils

        private void ComputeDirection()
        {
            var x = Input.GetAxis("Horizontal");
            var z = Input.GetAxis("Vertical");

            _direction = new Vector3(x, 0.0f, z).normalized;
        }

        private void ComputeRotation()
        {
            var x = Input.GetAxis("Mouse X");
            var y = Input.GetAxis("Mouse Y");

            _bodyRotation = new Vector3(0.0f, x, 0.0f);
            _cameraRotation = new Vector3(-y, 0.0f, 0.0f);
        }

        private void RotateCameraClamped()
        {
            if(!CanRotate()) return;

            _verticalAngle += _cameraRotation.x * _rotationSpeed;
            _verticalAngle = Mathf.Clamp(_verticalAngle, _verticalCameraRange.x, _verticalCameraRange.y);

            var clampedCameraRotation = new Vector3(_verticalAngle, 0.0f, 0.0f);

            _cameraTransform.localRotation = Quaternion.Euler(clampedCameraRotation);
        }

        private bool CanRotate()
        {
            if(_cameraRotation.x < 0 && _verticalAngle <= _verticalCameraRange.x) return false;
            if(_cameraRotation.x > 0 && _verticalAngle >= _verticalCameraRange.y) return false;

            return true;
        }

        #endregion


        #region Private

        private float _verticalAngle;
        private Vector3 _direction;
        private Vector3 _bodyRotation;
        private Vector3 _cameraRotation;
        private IInteractable _targetInteractable;

        #endregion
    }
}