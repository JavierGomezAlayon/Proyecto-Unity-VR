using UnityEngine;

// Script para abrir y cerrar la puerta con un trigger
public class PuertaTrigger : MonoBehaviour
{
    public Animator doorAnimator;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("OnTriggerEnter with: " + other.name);
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered trigger");
            doorAnimator.SetBool("IsOpen", true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("OnTriggerExit with: " + other.name);
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player exited trigger");
            doorAnimator.SetBool("IsOpen", false);
        }
    }
}
