using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridMovement : MonoBehaviour
{
    private Transform   m_GrabbedTransform;
    private Vector3     m_MouseWorldPosition;

    private int[]       m_IndexForPos = new int[2];
    private float       m_DistanceToCamera = 10;
    private int         m_GridSize = 30;
    private float       m_DragOffSet = 15;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            if (m_GrabbedTransform)
            {
                Drag();
            }
            else
            {
                Grab();
            }
        }
        else if (Input.GetMouseButtonUp(0) && m_GrabbedTransform)
        {
            m_GrabbedTransform = null;
        }
    }

    private void Grab()
    {
        if (m_GrabbedTransform)
        {
            m_GrabbedTransform = null;
        }
        else
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                m_GrabbedTransform = transform;
            }
        }
    }

    private void Drag()
    {
        m_MouseWorldPosition = Input.mousePosition;
        m_MouseWorldPosition.z = m_DistanceToCamera;
        m_MouseWorldPosition = Camera.main.ScreenToWorldPoint(m_MouseWorldPosition);

        m_IndexForPos = GetIndexForPosition(m_MouseWorldPosition.x - m_DragOffSet, m_MouseWorldPosition.y + m_DragOffSet);

        m_MouseWorldPosition = GetPositionForIndex(m_IndexForPos[0], m_IndexForPos[1]);

        m_GrabbedTransform.position = new Vector3(m_MouseWorldPosition.x, m_MouseWorldPosition.y, m_GrabbedTransform.position.z);
    }


    public int[] GetIndexForPosition(float _x, float _y)
    {
        int x;
        int y;

        int[] array = new int[2];

        int remainderX = (int)_x % m_GridSize;
        if (remainderX <= m_GridSize * 0.5f)
        {
            x = (int)_x - remainderX;
        }
        else
        {
            x = (int)_x + (m_GridSize - remainderX);
        }
        array[0] = (int)(x / m_GridSize);

        int remainderY = (int)_y % (int)(m_GridSize * 0.5f);
        if (remainderY <= m_GridSize * 0.5f)
        {
            y = (int)_y - remainderY;
        }
        else
        {
            y = (int)_y + (m_GridSize - remainderY);
        }

        array[1] = (int)(y / m_GridSize);

        return array;
    }

    public Vector2 GetPositionForIndex(int x, int y)
    {
        float posX = (float)(x * m_GridSize);
        float posY = (float)(y * m_GridSize);

        Vector2 position = new Vector2(posX, posY);

        return position;
    }
}
