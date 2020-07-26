using UnityEngine;


public class SteerLerp : MonoBehaviour
{
    public RectTransform ThrottleIndicator;
    public RectTransform FullL;
    public RectTransform HalfL;
    public RectTransform Zero;
    public RectTransform HalfR;
    public RectTransform FullR;
    [HideInInspector]public float value = 0;
    [HideInInspector]public float input = 0;

    private RectTransform own;
    private RectTransform current;
    private float Width;
    private float TargetValue = 0;


    private void Awake()
    {
        own = GetComponent<RectTransform>();
        current = Zero;
        Width = (FullR.anchoredPosition.x - FullL.anchoredPosition.x)/2;
    }


    void Update()
    {
        if (input != 0)
            ResolveNewPos(input);
        value = Mathf.MoveTowards(value, TargetValue, Time.deltaTime / 3f);
        own.anchoredPosition = new Vector2(-Width * value, own.anchoredPosition.y);
        ThrottleIndicator.anchoredPosition = new Vector2(-Width * TargetValue, ThrottleIndicator.anchoredPosition.y);
    }


    private void ResolveNewPos(float val)
    {
        // right
        if (input > 0)
        {
            if (current == FullL)
            {
                current = HalfL;
                TargetValue = 0.5f;
            }
            else if (current == HalfL)
            {
                current = Zero;
                TargetValue = 0;
            }
            else if (current == Zero)
            {
                current = HalfR;
                TargetValue = -0.5f;
            }
            else if (current == HalfR)
            {
                current = FullR;
                TargetValue = -1;
            }
        }
        // left
        else
        {
            if (current == HalfL)
            {
                current = FullL;
                TargetValue = 1;
            }
            else if (current == Zero)
            {
                current = HalfL;
                TargetValue = 0.5f;
            }
            else if (current == HalfR)
            {
                current = Zero;
                TargetValue = 0;
            }
            else if (current == FullR)
            {
                current = HalfR;
                TargetValue = -0.5f;
            }
        }
        input = 0;
    }
}