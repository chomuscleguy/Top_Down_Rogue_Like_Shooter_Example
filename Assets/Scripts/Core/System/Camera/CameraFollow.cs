using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform target;

    public void Init(Transform target)
    {
        this.target = target;
    }

    void LateUpdate()
    {
        if (target == null)
            return;

        transform.position = new Vector3(target.position.x, target.position.y, -10f);
    }
}
