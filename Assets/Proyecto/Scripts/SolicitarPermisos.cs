using UnityEngine;
#if UNITY_ANDROID
using UnityEngine.Android; // Necesario para gestionar permisos de Android
#endif

// Gestión de permisos de micrófono
public class SolicitarPermisos : MonoBehaviour
{
    void Awake()
    {
        // Solo ejecutamos esto si estamos en un dispositivo Android (las gafas)
        #if UNITY_ANDROID
        
        if (!Permission.HasUserAuthorizedPermission(Permission.Microphone))
        {
            Permission.RequestUserPermission(Permission.Microphone);
        }
        
        #endif
    }
}