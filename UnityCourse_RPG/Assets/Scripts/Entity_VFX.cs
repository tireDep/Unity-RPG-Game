using System.Collections;
using UnityEngine;

public class Entity_VFX : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    [Header("On Damage VFX")]
    [SerializeField] private Material onDamageMaterial;
    [SerializeField] private float onDamagedVfxDuration = 0.2f;
    private Material originMaterial;
    private Coroutine onDamageVfxCoroutine;

    private void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        originMaterial = spriteRenderer.material;
    }

    public void PlayOnDamageVfx()
    {
        if (onDamageVfxCoroutine != null)
            StopCoroutine(onDamageVfxCoroutine);

        onDamageVfxCoroutine = StartCoroutine(OnDamageVfxCorou());
    }

    private IEnumerator OnDamageVfxCorou()
    {
        spriteRenderer.material = onDamageMaterial;

        yield return new WaitForSeconds(onDamagedVfxDuration);
        spriteRenderer.material = originMaterial;
    }
}
