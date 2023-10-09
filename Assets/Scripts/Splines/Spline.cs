using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Splines;

namespace Splines
{
    public class Spline : MonoBehaviour
    {
        [SerializeField] private SplineContainer _spline;

        private List<BezierKnot> _points;

        private void Start()
        {
            _points = new();
        }

        public BezierKnot[] Build(List<Vector3> points)
        {
            _points.Clear();
            
            float pointXCorrection = 10f;
            float pointZCorrection = 20f;
            float resolvedPointZCorrection = 200f;
            
            UnityEngine.Splines.Spline spline = new();
            Vector3 previousPoint = Vector3.zero;

            points.ForEach(point =>
            {
                Vector3 pointResolved = new(
                    point.x * pointXCorrection,
                    transform.position.y,
                    point.y * pointZCorrection);
                
                if (Vector3.Distance(pointResolved, previousPoint) <= 1f && previousPoint != Vector3.zero)
                {
                    previousPoint = pointResolved;
                    return;
                }
                
                pointResolved.z -= resolvedPointZCorrection;
                BezierKnot knot = new(pointResolved);
                spline.Add(knot);
                previousPoint = pointResolved;
                _points.Add(knot);
            });
            
            _spline.Spline = spline;
            return _points.ToArray();
        }
    }
}