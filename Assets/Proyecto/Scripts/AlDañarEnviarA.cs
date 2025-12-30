using UnityEngine;

public class AlDañarEnviarA : MonoBehaviour
{
    public string SceneName;
    // La bala llamará a esta función al impactar
    public void TakeDamage(float amount)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(SceneName);
    }
}
