using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeManager : MonoBehaviour
{
    [SerializeField] private bool CanItReturn = true;

    public void LeftMoveFNC()
    {
        transform.Translate(Vector3.left, Space.World);
    }

    public void RightMoveFNC()
    {
        transform.Translate(Vector3.right, Space.World);
    }

    public void DownMoveFNC()
    {
        transform.Translate(Vector3.down, Space.World);
    }

    public void UpMoveFNC()
    {
        transform.Translate(Vector3.up, Space.World);
    }

    public void RotateLeftFNC()
    {
        if (CanItReturn)
            transform.Rotate(0f, 0f, -90f, Space.Self);
    }

    public void RotateRightFNC()
    {
        if (CanItReturn)
            transform.Rotate(0f, 0f, 90f, Space.Self);
    }

    public void IsTurnClockwise(bool isClockwise)
    {
        if (isClockwise) RotateRightFNC();
        else RotateLeftFNC();
    }
}
