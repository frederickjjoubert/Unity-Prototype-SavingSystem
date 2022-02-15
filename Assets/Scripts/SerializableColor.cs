using UnityEngine;

[System.Serializable]
public class SerializableColor
{
    float r, g, b;

    public SerializableColor(Color color)
    {
        r = color.r;
        g = color.g;
        b = color.b;
    }

    public Color ToColor()
    {
        return new Color(r, g, b);
    }

}
