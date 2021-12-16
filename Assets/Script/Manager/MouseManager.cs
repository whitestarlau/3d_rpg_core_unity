using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// [System.Serializable]
// public class EventVector3 : UnityEvent<Vector3>
// {

// }

public class MouseManager : Singleton<MouseManager>
{
    // public EventVector3 onMouseClick;
    public event Action<Vector3> onMouseClick;
    public event Action<GameObject> onEnemyClick;

    public Texture2D point, doorway, attack, target, arrow;

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
            switch (hitInfo.collider.gameObject.tag)
            {
                case "Ground":
                    Cursor.SetCursor(target, new Vector2(16, 16), CursorMode.Auto);
                    break;
                case "Enemy":
                    Cursor.SetCursor(attack, new Vector2(16, 16), CursorMode.Auto);
                    break;
                case "Doorway":
                    Cursor.SetCursor(doorway, new Vector2(16, 16), CursorMode.Auto);
                    break;
                default:
                    Cursor.SetCursor(target, new Vector2(16, 16), CursorMode.Auto);
                    break;
            }
        }
    }

    void mouseControl()
    {
        if (Input.GetMouseButtonDown(0) && hitInfo.collider != null)
        {
            switch (hitInfo.collider.gameObject.tag)
            {
                case "Ground":
                    onMouseClick?.Invoke(hitInfo.point);
                    break;
                case "Enemy":
                    onEnemyClick?.Invoke(hitInfo.collider.gameObject);
                    break;
            }
        }
    }
}
