using UnityEngine;

namespace CodeBase
{
    public static class Utils
    {
        public static float InRange(this float value, float min, float max) => Mathf.Clamp(value, min, max);
        
        public static Vector3 GetBezierPoint(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
        {
            Vector3 a = Vector3.Lerp(p0,p1, t);
            Vector3 b = Vector3.Lerp(p1, p2, t);
            Vector3 c = Vector3.Lerp(p2, p3, t);
            
            Vector3 d = Vector3.Lerp(a, b, t);
            Vector3 e = Vector3.Lerp(b, c, t);
            
            return Vector3.Lerp(d, e, t);
        }

        public static Vector3 GetBezierTangent(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
        {
            Vector3 a = Vector3.Lerp(p0, p1, t);
            Vector3 b = Vector3.Lerp(p1, p2, t);
            Vector3 c = Vector3.Lerp(p2, p3, t);

            Vector3 d = Vector3.Lerp(a, b, t);
            Vector3 e = Vector3.Lerp(b, c, t);

            return (d - e).normalized;
        }

        public static Vector3 GetNormal3D(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t, Vector3 up)
        {
           var tangent =  GetBezierTangent(p0, p1, p2, p3, t);
           var binormal = Vector3.Cross(up, tangent).normalized;
           return Vector3.Cross(tangent, binormal);
        }

        public static Quaternion GetOrientation3D(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t, Vector3 up)
        {
            var tangent =  GetBezierTangent(p0, p1, p2, p3, t);
            var normal = GetNormal3D(p0, p1, p2, p3, t, up);
            return Quaternion.LookRotation(tangent, normal);
        }
    }
}