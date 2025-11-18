using UnityEngine;
using System.Collections;

public class CreaterEffects : MonoBehaviour
{
    [SerializeField] private GameObject effectPrefab;
    private GameObject effect;
    private Coroutine coroutine;

    public void Show()
    {
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
            
            if (effect != null)
                Destroy(effect);
        }

        effect = Instantiate(effectPrefab, transform.position, Quaternion.identity);
        coroutine = StartCoroutine(PlayAndDestroy(2.5f));
    }

    private IEnumerator PlayAndDestroy(float lifeTime)
    {
        yield return new WaitForSeconds(lifeTime);
        if (effect != null) Destroy(effect);
        coroutine = null;
    }
}