using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;
public class PolygonalArea : MonoBehaviour
{

    public static PolygonalArea CurrentArea;

    void Awake()
    {
        CurrentArea = this;
    }
    private bool CheckInside(Vector3 point)
    {
        float count = 0;
        Vector3 last_dir = transform.GetChild(0).position - point;
        for (int i = 0; i < transform.childCount; i++)
        {
            var new_dir = transform.GetChild(i).position - point;
            var angle = Vector3.Angle(last_dir, new_dir);
            last_dir = new_dir;
            count += angle;
        }
        var dir = (transform.GetChild(0).position - point);
        var last_angle = Vector3.Angle(last_dir, dir);
        count += last_angle;
        return count >= 359;
    }

    public Vector3 GetClosestOnEdges(Vector3 point)
    {
        if (CheckInside(point))
            return point;
        Vector3 result = Vector3.zero;
        var distance = float.MaxValue;
        for (int i = 0; i < transform.childCount; i++)
        {
            var next = (i + 1 + transform.childCount) % transform.childCount;
            var edge = transform.GetChild(next).position - transform.GetChild(i).position;
            var projected = Vector3.Project(point, edge);
            var middle_pos = transform.GetChild(i).position + edge / 2;
            var diff = point - projected;

            var proj = Vector3.Project(point, edge);

            var length = ((middle_pos - projected).magnitude) * Mathf.Cos(Vector3.Angle((middle_pos - projected), (point - projected)) * Mathf.Deg2Rad);
            var final_projection = projected + (point - projected).normalized * length;

            final_projection = Limit(final_projection, transform.GetChild(i).position, edge);
            final_projection = Limit(final_projection, transform.GetChild(next).position, -edge);


            var new_distance = Vector3.Distance(final_projection, point);
            if (new_distance < distance)
            {
                result = final_projection;
                distance = new_distance;
            }
        }
        return result;
    }
    public Vector3 GetClosestOnEdges2(Vector3 point)
    {
        Vector3 closest_point = transform.GetChild(0).position;
        Vector3 second_closest_point = closest_point;
        var distance = float.MaxValue;

        for (int i = 0; i < transform.childCount; i++)
        {
            var new_d = Vector3.Distance(point, transform.GetChild(i).position);
            if (new_d < distance)
            {
                second_closest_point = closest_point;
                closest_point = transform.GetChild(i).position;
                distance = new_d;
            }
        }
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(closest_point, 0.5f);
        Gizmos.DrawSphere(second_closest_point, 0.5f);
        return closest_point;
    }
    public Vector3 Limit(Vector3 point, Vector3 limit, Vector3 axis)
    {
        var direction_sign_x = Mathf.Sign(axis.x);
        var x = Mathf.Max(point.x * direction_sign_x, limit.x * direction_sign_x) * direction_sign_x;

        var direction_sign_y = Mathf.Sign(axis.y);
        var y = Mathf.Max(point.y * direction_sign_y, limit.y * direction_sign_y) * direction_sign_y;

        var direction_sign_z = Mathf.Sign(axis.z);
        var z = Mathf.Max(point.z * direction_sign_z, limit.z * direction_sign_z) * direction_sign_z;

        return new Vector3(x, y, z);
    }
#if UNITY_EDITOR
    void OnDrawGizmos()
    {
        var points = new List<Vector3>();
        for (int i = 0; i < transform.childCount; i++)
        {
            var next = (i + 1 + transform.childCount) % transform.childCount;
            points.Add(transform.GetChild(i).position);
            var edge = transform.GetChild(i).position - transform.GetChild(next).position;
        }
        points.Add(transform.GetChild(0).position);
        Handles.color = Color.green;
        Handles.DrawAAPolyLine(1, points.ToArray());
    }
#endif
}
