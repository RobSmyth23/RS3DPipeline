using System.Data.Common;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.SocialPlatforms;

public class Outcode
{

    internal bool up;
    internal bool down;
    internal bool left;
    internal bool right;

    public Outcode(Vector2 v)
    {
        //UDLR - up down left right ---- up and down is Y axis, left and right is x axis
        //0000 in viewport, and 1's indicate not in viewport
        up = v.y > 1;
        down = v.y < -1;
        left = v.x < -1;
        right = v.x > 1;
    }
    public Outcode()
    {

    }
    public Outcode(Outcode oc)
    {
        up = oc.up;
        down = oc.down;
        left = oc.left;
        right = oc.right;
    }
    public Outcode(bool up, bool down, bool left, bool right)
    {
        this.up = up;
        this.down = down;
        this.left = left;
        this.right = right;
    }

    public static Outcode operator + (Outcode a, Outcode b)
    {
        return new Outcode(
            a.up || b.up,
            a.down || b.down,
            a.left || b.left,
            a.right || b.right
        );
    }

    public static Outcode operator * (Outcode a, Outcode b)
    {
        return new Outcode(
            a.up && b.up,
            a.down && b.down,
            a.left && b.left,
            a.right && b.right
        );
    }

    public static bool operator == (Outcode a, Outcode b)
    {
       return (a.up == b.up) && (a.down == b.down) && (a.left == b.left) && (a.right == b.right);
    }

    public static bool operator != (Outcode a, Outcode b)
    {
        return !(a == b);
    }

    //public string Display()
    //{
    //    return "Outcode = up: " + up + ", down: " + down + ", left: " + left + ", right: " + right;
    //}

    public override string ToString()
    {
        return (up ? "1" : "0") + (down ? "1" : "0") + (left ? "1" : "0") + (right ? "1" : "0");
    }

}