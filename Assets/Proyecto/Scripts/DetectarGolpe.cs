using UnityEngine;

public class DetectarGolpe : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip sonidoGolpe;

    // public GameObject panelRojo;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstaculo"))
        {
            hacerGolpe();
        }
    }

    void hacerGolpe()
    {
        if (audioSource != null && sonidoGolpe != null)
        {
            audioSource.PlayOneShot(sonidoGolpe);
        }
        // PlayerStats.Instance.RestarVida(10);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
