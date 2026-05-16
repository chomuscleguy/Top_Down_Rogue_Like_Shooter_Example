using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour
{
    public IEnumerator Shake(float duration,float magnitude)
    {
        Vector3 originPos = transform.localPosition;

        float timer = 0f;
        
        while(timer < duration)
        {
            float x = Random.Range(-1.0f, 1.0f) * magnitude;
            float y = Random.Range(-1.0f, 1.0f) * magnitude;

            transform.localPosition = originPos + new Vector3(x, y, 0);

            timer += Time.unscaledDeltaTime;
         
            yield return null;
        }

        transform.localPosition = originPos;
    }
}
