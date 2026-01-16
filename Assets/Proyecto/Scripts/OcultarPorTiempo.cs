using UnityEngine;
using System.Collections;

public class OcultarPorTiempo : MonoBehaviour
{
    [Header("¿Cuánto tiempo esperas?")]
    public float segundos = 3.0f;

    private void OnEnable()
    {
        StartCoroutine(CuentaAtras());
    }

    IEnumerator CuentaAtras()
    {
        yield return new WaitForSeconds(segundos);
        gameObject.SetActive(false);
    }
}