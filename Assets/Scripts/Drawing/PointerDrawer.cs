using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.EnhancedTouch;
using UnityEngine.UI;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

namespace Drawing
{
    public class PointerDrawer : MonoBehaviour
    {
        [SerializeField] private LineRenderer _lineRenderer;

        private List<Vector3> _points;
        private Camera _camera;
        private Input.Input _input;
        private bool _isDrawing;

        public event Action<List<Vector3>> LineDrawn; 

        private void Start()
        {
            EnhancedTouchSupport.Enable();
            
            _camera = Camera.main;
            _input = new();
            _points = new();
            
            _input.MovePerformed += Move;

            _input.ClickStarted += StartDraw;
            _input.ClickCanceled += StopDraw;
        }

        private void Move(Vector2 pointerPosition)
        {
            if (!_isDrawing || Touch.activeTouches.Count == 0) return;

            Ray ray = _camera.ScreenPointToRay(pointerPosition);
            RaycastHit hit;

            if (!Physics.Raycast(ray, out hit) 
                || !hit.collider.CompareTag("Writeable")
                || _points.Contains(pointerPosition)) return;

            hit.point = new(hit.point.x, hit.point.y, 0f);
            _points.Add ( hit.point );
            _lineRenderer.positionCount = _points.Count;
            _lineRenderer.SetPosition ( _lineRenderer.positionCount - 1, hit.point );
        }

        private void StartDraw()
        {
            _isDrawing = true;
        }

        private void StopDraw()
        {
            LineDrawn?.Invoke(_points);
            _points.Clear();
            _lineRenderer.positionCount = 0;
            _isDrawing = false;
        }
    }
}