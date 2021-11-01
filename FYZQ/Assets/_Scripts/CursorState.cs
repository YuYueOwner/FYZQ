using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CursorEnum
{
    MouseRightDownUI,
    MouseRightDown,
    TakeGoods,
    MouseDown,
    MouseIdel
}

public class CursorState : MonoBehaviour
{
    public static CursorState _instance;
    public List<Texture2D> cursor;
    // private CursorEnum cursorEnum = CursorEnum.MouseIdel;

    private void Awake()
    {
        _instance = this;
    }

    private void Update()
    {
        if (Input.GetMouseButton(1) || Input.GetMouseButton(0))
        {
            ChangeCursorState(CursorEnum.MouseRightDown);
        }

        if (Input.GetMouseButtonUp(1) || Input.GetMouseButtonUp(0))
        {
            ChangeCursorState(CursorEnum.MouseIdel);
        }
    }

    public void ChangeCursorState(CursorEnum cursorEnum)
    {
        switch (cursorEnum)
        {
            case CursorEnum.MouseRightDownUI:
                Cursor.visible = true;
                Cursor.SetCursor(cursor[0], Vector2.zero, CursorMode.Auto);
                break;
            case CursorEnum.MouseRightDown:
                Cursor.visible = false;
                break;
            case CursorEnum.TakeGoods:
                Cursor.visible = true;
                Cursor.SetCursor(cursor[1], Vector2.zero, CursorMode.Auto);
                break;
            case CursorEnum.MouseDown:
                Cursor.visible = true;
                Cursor.SetCursor(cursor[2], Vector2.zero, CursorMode.Auto);
                break;
            case CursorEnum.MouseIdel:
                Cursor.visible = true;
                Cursor.SetCursor(cursor[3], Vector2.zero, CursorMode.Auto);
                break;
            default:
                break;
        }
    }
}
