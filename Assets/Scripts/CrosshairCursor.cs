using UnityEngine;

public class CrosshairCursor : MonoBehaviour
{
    [Header("Cursor settings")]
    [Tooltip("The sprite for the cursor")]
    public Texture2D crosshairTexture;
    [Tooltip("If the image is 32x32 this would be 16x16")]
    public Vector2 hotspot = Vector2.zero;

    void Start()
    {
        Cursor.SetCursor(crosshairTexture, hotspot, CursorMode.Auto);
    }
}