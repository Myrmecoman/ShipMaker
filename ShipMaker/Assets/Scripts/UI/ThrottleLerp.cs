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
    private float Height;
    private float ZeroHeight;
    private float TargetValue = 0;


    private void Awake()
    {
        own = GetComponent<RectTransform>();
        current = Stop;
        Height = Full.anchoredPosition.y - FullBack.anchoredPosition.y;
        ZeroHeight = Stop.anchoredPosition.y;
    }


    void Update()
    {
        if (input != 0)
            ResolveNewPos(input);
        value = Mathf.MoveTowards(value, TargetValue, Time.deltaTime / 5f);
        //own.anchoredPosition = Vector2.MoveTowards(own.anchoredPosition, current.anchoredPosition, Time.deltaTime * 30);
    }


    private void ResolveNewPos(float val)
    {
        // forward
        if(input > 0)
        {
            if (current == FullBack)
            {
                current = Stop;
                TargetValue = 0;
            }
            else if (current == Stop)
            {
                current = Quart;
                TargetValue = 0.25f;
            }
            else if (current == Quart)
            {
                current = Half;
                TargetValue = 0.5f;
            }
            else if (current == Half)
            {
                current = AlmostFull;
                TargetValue = 0.75f;
            }
            else if (current == AlmostFull)
            {
                current = Full;
                TargetValue = 1;
            }
        }
        // backward
        else
        {
            if (current == Stop)
            {
                current = FullBack;
                TargetValue = -1;
            }
            else if (current == Quart)
            {
                current = Stop;
                TargetValue = 0;
            }
            else if (current == Half)
            {
                current = Quart;
                TargetValue = 0.25f;
            }
            else if (current == AlmostFull)
            {
                current = Half;
                TargetValue = 0.5f;
            }
            else if (current == Full)
            {
                current = AlmostFull;
                TargetValue = 0.75f;
            }
        }
        input = 0;
    }
}