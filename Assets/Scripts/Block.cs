using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField] public int idx;
    [SerializeField] public Transform[] wayPoint;
    [SerializeField] public Block prevBlock;
    [SerializeField] public Block nextBlock;
    [SerializeField] public Transform[] intersectPointPrev;
    [SerializeField] public Transform[] intersectPointNext;

    public static float offsetDistance = 25.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Projection을 통해 가장 가까운 line과 point를 구함
    public Vector3 getNearestPoint(Vector3 hitPos)
    {
        Vector3 lineStartPos, lineEndPos;
        Vector3 line;
        Vector3 hitLine;
        Vector3 projLine;
        Vector3 orthoLine;
        float distance;
        float shortestDistance = 1000;
        Vector3 nearestPoint = Vector3.zero;
       
        for (int i = 0; i < wayPoint.Length - 1; i++)
        {
            lineStartPos = wayPoint[i].position;
            lineEndPos = wayPoint[i + 1].position;
            line = lineEndPos - lineStartPos;
            hitLine = hitPos - lineStartPos;
            projLine = Vector3.Project(hitLine, line);
            orthoLine = hitLine - projLine;
            distance = orthoLine.magnitude;
            if ((line + projLine).magnitude < line.magnitude)   // hitpoint가 startpoint보다 앞에 있음
            {
                distance = (hitPos - lineStartPos).magnitude;
            }
            else if (projLine.magnitude > line.magnitude)     // hitpoint가 endpoint보다 뒤에 있음
            {
                distance = (hitPos - lineEndPos).magnitude;
            }
            if (distance < shortestDistance)
            {
                shortestDistance = distance;
                nearestPoint = lineStartPos + projLine;
                if ((line + projLine).magnitude < line.magnitude)   // hitpoint가 startpoint보다 앞에 있음
                {
                    nearestPoint = lineStartPos;
                } else if (projLine.magnitude > line.magnitude)     // hitpoint가 endpoint보다 뒤에 있음
                {
                    nearestPoint = lineEndPos;
                }
            }
        }
        return nearestPoint + Vector3.up;
    }

    // 정방향 기준, 해당 지점에서 가장 가까운 라인의 이전 인덱스를 반환
    public int getNearestWayPointIdx(Vector3 pos)
    {
        Vector3 lineStartPos, lineEndPos;
        Vector3 line;
        Vector3 hitLine;
        Vector3 projLine;
        Vector3 orthoLine;
        float distance;
        float shortestDistance = 1000;
        int wayPointIdx = 0;

        for (int i = 0; i < wayPoint.Length - 1; i++)
        {
            lineStartPos = wayPoint[i].position;
            lineEndPos = wayPoint[i + 1].position;
            line = lineEndPos - lineStartPos;
            hitLine = pos - lineStartPos;
            projLine = Vector3.Project(hitLine, line);
            orthoLine = hitLine - projLine;
            distance = orthoLine.magnitude;
            if ((line + projLine).magnitude < line.magnitude)   // hitpoint가 startpoint보다 앞에 있음
            {
                distance = (pos - lineStartPos).magnitude;
            }
            else if (projLine.magnitude > line.magnitude)     // hitpoint가 endpoint보다 뒤에 있음
            {
                distance = (pos - lineEndPos).magnitude;
            }
            if (distance < shortestDistance)
            {
                shortestDistance = distance;
                wayPointIdx = i;
            }
        }

        return wayPointIdx;
    }

    public bool intersectPrevBlock()
    {
        Vector3 thisScreen0 = Camera.main.WorldToScreenPoint(intersectPointPrev[0].position);
        Vector3 thisScreen1 = Camera.main.WorldToScreenPoint(intersectPointPrev[1].position);
        Vector3 prevScreen0 = Camera.main.WorldToScreenPoint(prevBlock.intersectPointNext[0].position);
        Vector3 prevScreen1 = Camera.main.WorldToScreenPoint(prevBlock.intersectPointNext[1].position);
        float distance0 = (thisScreen0 - prevScreen0).magnitude;
        float distance1 = (thisScreen1 - prevScreen1).magnitude;
        //Debug.Log("distance0: " + distance0 + "distance1: " + distance1);
        return (distance0 < offsetDistance && distance1 < offsetDistance) ? true : false;
    }
    public bool intersectNextBlock()
    {
        Vector3 thisScreen0 = Camera.main.WorldToScreenPoint(intersectPointNext[0].position);
        Vector3 thisScreen1 = Camera.main.WorldToScreenPoint(intersectPointNext[1].position);
        Vector3 nextScreen0 = Camera.main.WorldToScreenPoint(nextBlock.intersectPointPrev[0].position);
        Vector3 nextScreen1 = Camera.main.WorldToScreenPoint(nextBlock.intersectPointPrev[1].position);
        float distance0 = (thisScreen0 - nextScreen0).magnitude;
        float distance1 = (thisScreen1 - nextScreen1).magnitude;
        //Debug.Log("distance0: " + distance0 + "distance1: " + distance1);
        return (distance0 < offsetDistance && distance1 < offsetDistance) ? true : false;
    }
}
