using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class EventVector3 : UnityEvent<Vector3>
{

}

public class MouseManager : MonoBehaviour
{
    public EventVector3 onMouseClick;

    RaycastHit hitInfo;

    // Update is called once per frame
    void Update()
    {
        setCursorTexture();
        mouseControl();
    }

    //获取点击目标，同时改变鼠标颜色和状态
    void setCursorTexture()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hitInfo))
        {

        }
    }

    void mouseControl()
    {
        if (Input.GetMouseButtonDown(0) && hitInfo.collider != null)
        {
            if (hitInfo.collider.gameObject.CompareTag("Ground"))
            {
                onMouseClick?.Invoke(hitInfo.point);
            }
        }
    }
}
