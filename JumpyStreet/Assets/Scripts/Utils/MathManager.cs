using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MathUtils
{
    /// This script holds simplified math functions for use in this code.

    public static readonly float Epsilon = Mathf.Epsilon * 2f;

    public static float Round(float num, int roundTo)
    {
        //Debug.Log("Rounding " + num + " to " + roundTo + " decimal places.");
        float roundNum = System.Convert.ToSingle(System.Math.Round(num, roundTo, System.MidpointRounding.AwayFromZero));
        return roundNum;
    }
    public static Vector2 Round(Vector2 v2, int roundTo)
    {
        float newX = Round(v2.x, roundTo);
        float newY = Round(v2.y, roundTo);
        Vector2 newV2 = new Vector2(newX, newY);
        return newV2;
    }
    public static Vector3 Round(Vector3 v3, int roundTo)
    {
        float newX = Round(v3.x, roundTo);
        float newY = Round(v3.y, roundTo);
        float newZ = Round(v3.z, roundTo);
        Vector3 newV3 = new Vector3(newX, newY, newZ);
        return newV3;
    }

    public static float GetMidpointBetweenFloats(List<float> floatList)
    {
        floatList.Sort();
        return (floatList[0] + floatList[floatList.Count - 1]) / 2;
    }
    public static Vector2 GetMidpointBetweenPositions(List<Vector2> v2List)
    {
        if (v2List[0] != null)
        {
            List<float> xPosList = new List<float>();
            List<float> yPosList = new List<float>();
            for (int i = 0; i < v2List.Count; i++)
            {
                xPosList.Add(v2List[i].x);
                yPosList.Add(v2List[i].y);
            }
            xPosList.Sort();
            yPosList.Sort();
            return new Vector2(GetMidpointBetweenFloats(xPosList),GetMidpointBetweenFloats(yPosList));
        }
        return new Vector2(0,0);
    }
    public static Vector3 GetMidpointBetweenPositions(List<Vector3> v3List)
    {
        if (v3List[0] != null)
        {
            List<float> xPosList = new List<float>();
            List<float> yPosList = new List<float>();
            List<float> zPosList = new List<float>();
            for (int i = 0; i < v3List.Count; i++)
            {
                xPosList.Add(v3List[i].x);
                yPosList.Add(v3List[i].y);
                zPosList.Add(v3List[i].z);
            }
            xPosList.Sort();
            yPosList.Sort();
            zPosList.Sort();
            return new Vector3(GetMidpointBetweenFloats(xPosList), GetMidpointBetweenFloats(yPosList), GetMidpointBetweenFloats(zPosList));
        }
        return new Vector3(0, 0, 0);
    }
    public static Vector2 GetMidpointBetweenPositions(List<GameObject> GOList)
    {
        List<Vector2> vectorList = new List<Vector2>();
        for (int i = 0; i < vectorList.Count; i++)
        {
            vectorList.Add(GOList[i].transform.position);
            vectorList.Add(GOList[i].transform.position);
        }
        return GetMidpointBetweenPositions(vectorList);
    }
    public static Vector3 GetMidpointBetweenPositionsV3(List<GameObject> GOList)
    {
        List<Vector2> vectorList = new List<Vector2>();
        for (int i = 0; i < vectorList.Count; i++)
        {
            vectorList.Add(GOList[i].transform.position);
            vectorList.Add(GOList[i].transform.position);
            vectorList.Add(GOList[i].transform.position);
        }
        return GetMidpointBetweenPositions(vectorList);
    }

    public static string Get2DigitNumberString(int num)
    {
        switch (num.ToString().Length)
        {
            default:
                return "99";
            case 2:
                return num.ToString();
            case 1:
                return "0" + num.ToString();
        }
    }
    public static string Get3DigitNumberString(int num)
    {
        switch (num.ToString().Length)
        {
            default:
                return "999";
            case 3:
                return num.ToString();
            case 2:
                return "0" + num.ToString();
            case 1:
                return "00" + num.ToString();
        }
    }

    /// <summary>
    /// Returns the cosine of the angle
    /// </summary>
    /// <param name="degrees">the angle in degrees</param>
    public static float Cos(float degrees)
    {
        return Mathf.Cos(degrees * Mathf.Deg2Rad);
    }

    /// <summary>
    /// Returns the sine of the angle
    /// </summary>
    /// <param name="degrees">the angle in degrees</param>
    public static float Sin(float degrees)
    {
        return Mathf.Sin(degrees * Mathf.Deg2Rad);
    }

    /// <summary>
    /// Turns a euler angle, in degrees, into a Vector2.
    /// </summary>
    /// <param name="degrees"> The angle in degrees. </param>
    public static Vector2 EulerToVector(float degrees)
    {
        return new Vector2(Cos(degrees), Sin(degrees));
    }

    /// <summary>
    /// Returns the angle of the specified vector in degrees between 0 and 360.
    /// </summary>
    /// <param name="a">The vector.</param>
    public static float Angle(Vector2 a)
    {
        return (Mathf.Atan2(a.y, a.x) * Mathf.Rad2Deg) % 360f;
    }

    public static bool PointIsBetween(Vector2 p, Vector2 a, Vector2 b)
    {
        bool xIsBetween = (a.x <= p.x && p.x <= b.x) || (b.x <= p.x && p.x <= a.x);
        bool yIsBetween = (a.y <= p.y && p.y <= b.y) || (b.y <= p.y && p.y <= a.y);
        return xIsBetween && yIsBetween;
    }

    public static Quaternion ShortestRotation(Quaternion a, Quaternion b)
    {
        if (Quaternion.Dot(a, b) < 0)
        {
            return a * Quaternion.Inverse(Multiply(b, -1));
        }
        else return a * Quaternion.Inverse(b);
    }

    /// <summary>
    /// Returns the signed angle difference between two rays pointing in the specified direction, in degrees.
    /// 
    /// If the shortest arc from a to b is counterclockwise (increasing in angle),
    /// the sign is positive; otherwise the sign is negative.
    /// </summary>
    /// <returns>The angle in degrees between two rays pointing in the specified direction.</returns>
    /// <param name="a">The angle of the first ray, in degrees.</param>
    /// <param name="b">The angle of the second ray, in degrees.</param>
    public static float ShortestRotation(float a, float b)
    {
        return ((b - a + 180.0f) % 360.0f) - 180.0f;
    }

    public static Quaternion Multiply(Quaternion input, float scalar)
    {
        return new Quaternion(input.x * scalar, input.y * scalar, input.z * scalar, input.w * scalar);
    }

    public static Quaternion VectorToQuaternion(Vector2 v)
    {
        return Quaternion.FromToRotation(Vector2.right, v);
    }

    public static float FromToRotation(Vector2 from, Vector2 to)
    {
        return Quaternion.FromToRotation(from, to).eulerAngles.z;
    }

    public static Vector2 Project(Vector2 main, Vector2 on)
    {
        float magnitude = Vector2.Dot(main, on);
        return on.normalized * magnitude;
    }

    /// <summary>
    /// Returns the projection of vector main onto a vector pointing theta degrees.
    /// </summary>
    /// <param name="main">The vector main.</param>
    /// <param name="theta">The angle theta, in degrees.</param>
    public static Vector2 Project(Vector2 main, float theta)
    {
        return Project(main, EulerToVector(theta));
    }

    /// <summary>
    /// Returns the scalar projection of vector main (aka scalar component) onto a vector pointing theta degrees.
    /// </summary>
    /// <param name="main">The vector main.</param>
    /// <param name="theta">The angle theta, in degrees.</param>
    public static float ScalarAngleProjection(Vector2 main, float theta)
    {
        return Vector2.Dot(main, EulerToVector(theta));
    }

    /// <summary>
    /// Returns the specified angle in the range 0 to 360.
    /// </summary>
    /// <param name="angle">The specified angle in degrees.</param>
    /// <returns></returns>
    public static float PositiveAngle(float angle)
    {
        return angle % 360.0f;
    }

    /// <summary>
    /// Returns the positive length of the arc from a traveling counter-clockwise to b, in radians.
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    public static float PositiveArc(float a, float b)
    {
        var diff = PositiveAngle(b) - PositiveAngle(a);
        if (diff < 0.0f) return diff + 360f;
        return diff;
    }

    /// <summary>
    /// Returns whether the specified angle is inside the arc formed between the angle a
    /// traveling counter-clockwise (upwards) to b.
    /// </summary>
    /// <param name="angle">The specified angle in degrees.</param>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    public static bool AngleInRange(float angle, float a, float b)
    {
        return PositiveArc(a, angle) <= PositiveArc(a, b);
    }

    /// <summary>
    /// Rotates the point by the angle about (0, 0).
    /// </summary>
    /// <param name="point">The point.</param>
    /// <param name="angle">The angle in degrees.</param>
    public static Vector2 RotateBy(Vector2 point, float angle)
    {
        return RotateBy(point, angle, new Vector2(0.0f, 0.0f));
    }

    /// <summary>
    /// Rotates the point by the angle about the specified origin.
    /// </summary>
    /// <param name="point">The point.</param>
    /// <param name="angle">The angle in degrees.</param>
    /// <param name="origin">The origin.</param>
    public static Vector2 RotateBy(Vector2 point, float angle, Vector2 origin)
    {
        float s = Sin(angle);
        float c = Cos(angle);

        Vector2 npoint = point - origin;

        return new Vector2(npoint.x * c - npoint.y * s + origin.x, npoint.x * s + npoint.y * c + origin.y);
    }

    /// <summary>
    /// Returns the positive vertical distance between a and b if a is higher than b or the negative
    /// vertical distance if the opposite is true.
    /// </summary>
    /// <param name="a">The point a.</param>
    /// <param name="b">The point b.</param>
    public static float Highest(Vector2 a, Vector2 b)
    {
        return Highest(a, b, 90f);
    }

    /// <summary>
    /// Returns the positive distance between a and b projected onto the axis in the specifed direction.
    /// If a is higher than b on that axis, a positive value is returned. Otherwise a negative value.
    /// </summary>
    /// <param name="a">The point a.</param>
    /// <param name="b">The point b.</param>
    /// <param name="direction">The direction, in degrees, by which the farthest point in that
    /// direction is chosen. For example, if zero is specified, and point a is farther right
    /// than point b, a positive distance will be returned.</param>
    /// <returns>The positive distance between a and b if a is higher than b in the specified
    /// direction or the negative distance if the opposite is true.</returns>
    public static float Highest(Vector2 a, Vector2 b, float direction)
    {
        // Get easy angles out of the way
        if (direction == 0f) return a.x - b.x;
        if (direction == 90f) return a.y - b.y;
        if (direction == 180f) return b.x - a.x;
        if (direction == 270f) return b.y - a.y;

        Vector2 diff = Project(a, direction) - Project(b, direction);
        return (Mathf.Abs(Angle(diff) - direction) < 90f) ? diff.magnitude : -diff.magnitude;
    }

    /// <summary>
    /// Returns if a is equal to 0, plus or minus a small difference
    /// </summary>
    public static bool Equalsf(float a)
    {
        return Equalsf(a, 0f);
    }

    /// <summary>
    /// Returns if a and b are equal, plus or minus a small difference
    /// </summary>
    public static bool Equalsf(float a, float b)
    {
        return Mathf.Abs(a - b) <= Epsilon;
    }
}