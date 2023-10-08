using System;
using System.Collections.Generic;
using Dreamteck.Splines;
using UnityEngine;

namespace Splines
{
    public class SplineScript : MonoBehaviour
    {
        [SerializeField] private SplineComputer _spline;

        private void Start()
        {
            Vector3 initialPoint = new Vector3(0, 0, 0);
            Vector3 endPoint = new Vector3(20, 0, 20);
            List<SplinePoint> result = new();
            float chunkAmount = 20f;
            float divider = 1f / chunkAmount;
            float linear = 0f;

            for (int i = 0; i < chunkAmount; i++) 
            {
                if (i == 0) 
                { 
                    linear = divider / 2;
                }
                else 
                { 
                    linear += divider; //Add the divider to it to get the next distance
                } 
                // Debug.Log("Loop " + i + ", is " + linear);
                result.Add(new(Vector3.Lerp(initialPoint, endPoint, linear)));
            }
            
            _spline.SetPoints(result.ToArray());
        }
    }
}