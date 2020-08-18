using UnityEngine;


public class SteerLerp : MonoBehaviour
{
    public RectTransform ThrottleIndicator;
    public RectTransform FullL;
    public RectTransform Zero;
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
        ResolveNewPos(input);
        value = Mathf.MoveTowards(value, TargetValue, Time.deltaTime / 2f);
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
                current = Zero;
                TargetValue = 0;
            }
            else if (current == Zero)
            {
                current = FullR;
                TargetValue = -1;
            }
            return;
        }

        // left
        if(input < 0)
        {
            if (current == Zero)
            {
                current = FullL;
                TargetValue = 1;
            }
            else if (current == FullR)
            {
                current = Zero;
                TargetValue = 0;
            }
            return;
        }

        // No input
        current = Zero;
        TargetValue = 0;
    }
}