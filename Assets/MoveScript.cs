using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class MoveScript : MonoBehaviour
{
    public float speed = 20;
    Vector3 midoint;
    float rotation = -1;
    float currentSpeed;

    public async Task RotateAroundCenter(Transform object1, Transform object2)
    {
        while (rotation != -1 && rotation < 180)
        {
            currentSpeed = speed * Time.deltaTime;
            object1.RotateAround(midoint, Vector3.up, currentSpeed);
            object2.RotateAround(midoint, Vector3.up, currentSpeed);
            rotation += currentSpeed;
            await Task.Yield(); //can do wait for seconds....
        }
        if (rotation >= 180)
        {
            rotation = -1;
        }
    }

    public async Task RoundSwapObjects(Transform obj1, Transform obj2)
    {
        midoint = Vector3.Lerp(obj1.position, obj2.position, 0.5f);
        rotation = 0;
        await RotateAroundCenter(obj1, obj2);
    }
}
