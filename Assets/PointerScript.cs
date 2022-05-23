using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class PointerScript : MonoBehaviour
{
    float smoothing = 3f;

    public async Task MoveTo(Transform target)
    {
        while (Vector3.Distance(transform.position, Above(target, 1)) > 0.05f)
        {
            transform.position = Vector3.Lerp(transform.position, Above(target, 1), smoothing / 200);//* Time.deltaTime);
            await Task.Yield();
        }
    }

    public Vector3 Above(Transform obj, float h)
    {
        Vector3 pos = obj.position;
        return new Vector3(pos.x, pos.y + h + obj.localScale.y / 2, pos.z);
    }
}
