using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Drawing
{
    public class MouseDrawer : MonoBehaviour
    {
        private Input.Input _input;
        private bool _isDrawing;
        
        private void Start()
        {
            _input = new();
            
            _input.MovePerformed += Move;

            _input.ClickStarted += StartDraw;
            _input.ClickCanceled += StopDraw;
        }

        private void Move(Vector2 position)
        {
            if (!_isDrawing) return;
            
            Debug.Log(position);
        }

        private void StartDraw()
        {
            _isDrawing = true;
        }

        private void StopDraw()
        {
            _isDrawing = false;
        }
        
        
    }
}