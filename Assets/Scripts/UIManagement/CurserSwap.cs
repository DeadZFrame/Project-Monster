using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurserSwap : MonoBehaviour
{
    public Texture2D curserTexture;

    private void Start()
    {
        Cursor.SetCursor(curserTexture, Vector2.zero, CursorMode.ForceSoftware);
    }
}
