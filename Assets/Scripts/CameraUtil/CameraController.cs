using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace CameraUtil
{
    public class CameraController : MonoBehaviour
    {
        public GameObject target; // This is the game object the camera will focus on
        
        private Vector3 _offset; // Offset distance between our transform and the target transform
        private Camera _cam;
        private float _targetZoom;
        private float _velocity = 0.0f;
        private float _zoomSpeed = 10f;
        private float _zoomDampSpeed = 10f;
        private float _maxZoom = 50f;
        void Start()
        {
            _cam = Camera.main;
            _targetZoom = _cam.orthographicSize;
            _offset = transform.position - target.transform.position;
        }

        void Update()
        {
            var scrollMovement = Input.GetAxis("Mouse ScrollWheel");

            _targetZoom -= scrollMovement * _zoomSpeed;
            _targetZoom = Mathf.Clamp(_targetZoom, 5f, _maxZoom);
            _cam.orthographicSize = Mathf.SmoothDamp(_cam.orthographicSize, _targetZoom, ref _velocity, Time.deltaTime * _zoomDampSpeed);
        }

        // LateUpdate is called after Update each frame
        void LateUpdate()
        {
            // Set the position of the camera's transform to be the same as the player's, but offset by the calculated offset distance.
            transform.position = target.transform.position + _offset;
        }
        
    }
}
