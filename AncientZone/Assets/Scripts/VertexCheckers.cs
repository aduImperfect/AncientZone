using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VertexCheckers
{
    public static bool IsVertexInside(Mesh mesh, Vector3 point)
    {
        Vector3[] verts = mesh.vertices;
        int[] tris = mesh.triangles;

        int triangleCount = tris.Length / 3;

        for(int i = 0; i < triangleCount; ++i)
        {
            Vector3 v1 = verts[tris[i * 3]];
            Vector3 v2 = verts[tris[i * 3 + 1]];
            Vector3 v3 = verts[tris[i * 3 + 2]];

            Plane pl = new Plane(v1, v2, v3);
            if(pl.GetSide(point))
            {
                return false;
            }
        }

        return true;
    }

    public static List<RaycastHit> SweepAround(Transform currTrans)
    {
        List<RaycastHit> rayCastinfoList = new List<RaycastHit>();

        float angle1InRad = 1.0f * Mathf.Deg2Rad;

        for (int i = 0; i < 360; ++i)
        {
            RaycastHit hitInfo;

            Physics.Raycast(currTrans.position, currTrans.forward, out hitInfo);
            rayCastinfoList.Add(hitInfo);

            currTrans.Rotate(Vector3.up, angle1InRad);
        }

        return rayCastinfoList;
    }
    public static List<Vector3> GetCastHitPoints(List<RaycastHit> castHitList)
    {
        List<Vector3> castHitPointsList = new List<Vector3>();

        for (int i = 0; i < castHitList.Count; ++i)
        {
            castHitPointsList.Add(castHitList[i].point);
        }

        return castHitPointsList;
    }

    public static RaycastHit RayCollision(Vector3 pos, Vector3 dir)
    {
        RaycastHit rayCastinfo;
        Physics.Raycast(pos, dir, out rayCastinfo);
        return rayCastinfo;
    }
}
