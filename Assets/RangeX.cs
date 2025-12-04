using JetBrains.Annotations;
using UnityEngine;

public class RangeX : MonoBehaviour
{
    internal int start = -1;
    internal int end = -1;

    //c dict k is y, ranex .addpoint foreach y
    public void AddPoint(int x)
    {
        if (start == -1)
        {
            start = x;
            return;
        }
        if (end == -1)
        {
            if (start <= x)
            {
                end = x;
            }
            else
            {
                end = start;
                start = x;
            }
            return;
        }

        if (x < start)
        {
            start = x;
        }
        if (x > end)
        {
            end = x;
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
