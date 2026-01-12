using UnityEngine;
#if UNITY_ANDROID
using UnityEngine.Android; // Necesario para gestionar permisos de Android
#endif

public class SolicitarPermisos : MonoBehaviour
{
    void Awake()
    {
        // Solo ejecutamos esto si estamos en un dispositivo Android (las gafas)
        #if UNITY_ANDROID
        
        // Si NO tenemos permiso del micrófono todavía...
        if (!Permission.HasUserAuthorizedPermission(Permission.Microphone))
        {
            // ...lo pedimos ahora mismo.
            Permission.RequestUserPermission(Permission.Microphone);
        }
        
        #endif
    }
}