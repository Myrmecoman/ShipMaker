using UnityEngine;


public class SteerLerp : MonoBehaviour
{
    public RectTransform FullL;
    public RectTransform HalfL;
    public RectTransform Zero;
    public RectTransform HalfR;
    public RectTransform FullR;
    [HideInInspector]public float value = 0;
    [HideInInspector]public float input = 0;

    private RectTransform own;
    private RectTransform current;


    private void Awake()
    {
        own = GetComponent<RectTransform>();
        current = Zero;
    }


    void Update()
    {
        if (input != 0)
            ResolveNewPos(input);
        own.anchoredPosition = Vector2.MoveTowards(own.anchoredPosition, current.anchoredPosition, Time.deltaTime * 10);
    }


    private void ResolveNewPos(float val)
    {
        // right
        if (input > 0)
        {
            if (current == FullL)
                current = HalfL;
            if (current == HalfL)
                current = Zero;
            if (current == Zero)
                current = HalfR;
            if (current == HalfR)
                current = FullR;
        }
        // left
        else
        {
            if (current == HalfL)
                current = FullL;
            if (current == Zero)
                current = HalfL;
            if (current == HalfR)
                current = Zero;
            if (current == FullR)
                current = HalfR;
        }
        input = 0;
    }
}