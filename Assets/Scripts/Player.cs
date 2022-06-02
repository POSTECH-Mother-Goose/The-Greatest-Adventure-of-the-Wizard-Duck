using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class Player : MonoBehaviour
{
    [SerializeField] public Block block;
    [SerializeField] float speed = 5f;
    Vector3[] destinations;
    int num = -1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (num >= 0)
        {
            if (transform.position.y != destinations[num].y) // �ٸ� ����� waypoint�� �� y���� �ٸ� �ɷ� ����
            {
                transform.position = destinations[num];
                //Thread.Sleep((int)(0.5 / speed * 1000));    // �Ÿ� / �ӵ� * ms/s
                num++;
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, destinations[num], speed * Time.deltaTime);
                if (transform.position == destinations[num])
                {
                    num++;
                }
            }

            if (destinations[num] == Vector3.down)  // ������ destination�̶�� ��ȣ
            {
                num = -1;
                return;
            }
        }
    }

    public void moveSet(Vector3[] _destinations)
    {
        destinations = _destinations;
        num = 0;
    }
}
