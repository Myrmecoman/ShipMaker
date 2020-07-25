using UnityEngine;


public class ThrottleLerp : MonoBehaviour
{
    public RectTransform FullBack;
    public RectTransform Stop;
    public RectTransform Quart;
    public RectTransform Half;
    public RectTransform AlmostFull;
    public RectTransform Full;
    [HideInInspector]public float value = 0;
    [HideInInspector]public float input = 0;

    private RectTransform own;
    private RectTransform current;


    private void Awake()
    {
        own = GetComponent<RectTransform>();
        current = Stop;
    }


    void Update()
    {
        if (input != 0)
            ResolveNewPos(input);
        own.anchoredPosition = Vector2.MoveTowards(own.anchoredPosition, current.anchoredPosition, Time.deltaTime * 10);
    }


    private void ResolveNewPos(float val)
    {
        // forward
        if(input > 0)
        {
            if (current == FullBack)
                current = Stop;
            if (current == Stop)
                current = Quart;
            if (current == Quart)
                current = Half;
            if (current == Half)
                current = AlmostFull;
            if (current == AlmostFull)
                current = Full;
        }
        // backward
        else
        {
            if (current == Stop)
                current = FullBack;
            if (current == Quart)
                current = Stop;
            if (current == Half)
                current = Quart;
            if (current == AlmostFull)
                current = Half;
            if (current == Full)
                current = AlmostFull;
        }
        input = 0;
    }
}