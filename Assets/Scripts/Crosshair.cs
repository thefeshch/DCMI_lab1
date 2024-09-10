using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crosshair : MonoBehaviour
{
    public Texture2D crosshairTexture;
    public float crosshairSize = 25f;

    void OnGUI()
    {
        float xMin = (Screen.width / 2) - (crosshairSize);
        float yMin = (Screen.height / 2 - 150) - (crosshairSize);
        GUI.DrawTexture(new Rect(xMin, yMin, crosshairSize, crosshairSize), crosshairTexture);
    }
}
