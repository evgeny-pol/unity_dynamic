using UnityEngine;

public static class VectorUtils
{
    public static Vector3 GetRandomVector(Vector3 area)
    {
        return new Vector3(area.x * Random.value, area.y * Random.value, area.z * Random.value);
    }
}
