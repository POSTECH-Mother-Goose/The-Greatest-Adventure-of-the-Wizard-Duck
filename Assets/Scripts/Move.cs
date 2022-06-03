using PlayerInputs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    [SerializeField] Block[] blockList;
    public Camera cam;
    public PlayerObject player;

    // Start is called before the first frame update
    void Start()
    {
        //Vector3 pos = cam.WorldToScreenPoint(cube.transform.position);        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonUp(0) && !player.isMoving)
        {
            RaycastHit hit;
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject.transform.parent == null) return;
                GameObject hitObj = hit.collider.gameObject.transform.parent.gameObject;
                Block hitBlock = hitObj.GetComponent<Block>();
                if (hitBlock == null) return;
                Vector3 hitPos = hit.transform.position;

                if (!checkCanGo(player.block.idx, hitBlock.idx)) return;
                Vector3 dest = hitBlock.getNearestPoint(hitPos);
                Vector3[] destinations = playerWayPointSet(hitBlock.idx, dest);
                player.moveSet(destinations);
            }
        }
    }

    bool checkCanGo(int idx1, int idx2)
    {
        if (idx1 < idx2)
        {
            for (int i = idx1; i < idx2; i++)
            {
                if (!blockList[idx1].intersectNextBlock()) return false;
            }
        }
        else
        {
            for (int i = idx1; i > idx2; i--)
            {
                if (!blockList[idx1].intersectPrevBlock()) return false;
            }
        }
        Debug.Log("can go");
        return true;
    }
    Vector3[] playerWayPointSet(int destBlockIdx, Vector3 dest)
    {
        Vector3[] destinations = new Vector3[100];
        int num = 0;

        if (player.block.idx == destBlockIdx)
        {
            int nowIdx = player.block.getNearestWayPointIdx(player.transform.position);
            int nextIdx = player.block.getNearestWayPointIdx(dest);
            if (nowIdx == nextIdx)
            {
                destinations[num++] = dest;
            }
            else if (nowIdx < nextIdx)
            {
                for (int i = nowIdx + 1; i <= nextIdx; i++)
                {
                    destinations[num++] = blockList[player.block.idx].wayPoint[i].position + Vector3.up;
                }
                destinations[num++] = dest;
            }
            else
            {
                for (int i = nowIdx; i > nextIdx; i--)
                {
                    destinations[num++] = blockList[player.block.idx].wayPoint[i].position + Vector3.up;
                }
                destinations[num++] = dest;
            }
        }
        else if (player.block.idx < destBlockIdx)
        {
            // 현재 블록
            int wayPointIdx = player.block.getNearestWayPointIdx(player.transform.position);
            for (int i = wayPointIdx + 1; i < blockList[player.block.idx].wayPoint.Length; i++)
            {
                destinations[num++] = blockList[player.block.idx].wayPoint[i].position + Vector3.up;
            }

            // 중간 블록들
            for (int i = player.block.idx + 1; i < destBlockIdx; i++)
            {
                for (int j = 0; j < blockList[i].wayPoint.Length; j++)
                {
                    destinations[num++] = blockList[i].wayPoint[j].position + Vector3.up;
                }
            }

            // 끝 블록
            wayPointIdx = blockList[destBlockIdx].getNearestWayPointIdx(dest);
            for (int i = 0; i <= wayPointIdx; i++)
            {
                destinations[num++] = blockList[destBlockIdx].wayPoint[i].position + Vector3.up;
            }
            destinations[num++] = dest;
        }
        else  // 역방향
        {
            // 현재 블록
            int wayPointIdx = player.block.getNearestWayPointIdx(player.transform.position);
            for (int i = wayPointIdx; i >= 0; i--)
            {
                destinations[num++] = blockList[player.block.idx].wayPoint[i].position + Vector3.up;
            }

            // 중간 블록들
            for (int i = player.block.idx - 1; i > destBlockIdx; i--)
            {
                for (int j = blockList[i].wayPoint.Length - 1; j >= 0; j--)
                {
                    destinations[num++] = blockList[i].wayPoint[j].position + Vector3.up;
                }
            }

            // 끝 블록
            wayPointIdx = blockList[destBlockIdx].getNearestWayPointIdx(dest);
            for (int i = blockList[destBlockIdx].wayPoint.Length - 1; i > wayPointIdx; i--)
            {
                destinations[num++] = blockList[destBlockIdx].wayPoint[i].position + Vector3.up;
            }
            destinations[num++] = dest;
        }
        destinations[num++] = Vector3.down;
        player.block = blockList[destBlockIdx];
        return destinations;
    }
}
