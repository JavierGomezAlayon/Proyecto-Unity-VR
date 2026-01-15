using UnityEngine;

public class DetectorRuido : MonoBehaviour
{
    public static DetectorRuido instancia;

    [Header("Configuración")]
    [Tooltip("Multiplicador para ajustar la sensibilidad")]
    public float sensibilidad = 50; 
    public float volumenActual { get; private set; } // Los enemigos leerán esto

    private AudioClip clipMicrofono;
    private string microfonoNombre;
    private int sampleWindow = 128;

    private void Awake()
    {
        if (instancia == null) instancia = this;
    }

    private void Start()
    {
        if (Microphone.devices.Length > 0)
        {
            microfonoNombre = Microphone.devices[0];
            clipMicrofono = Microphone.Start(microfonoNombre, true, 1, 44100);
        }
        else
        {
            Debug.LogError("¡No hay micrófono detectado!");
        }
    }

    private void Update()
    {
        volumenActual = ObtenerVolumen() * sensibilidad;
    }

    float ObtenerVolumen()
    {
        if (clipMicrofono == null) return 0f;

        int pos = Microphone.GetPosition(microfonoNombre) - (sampleWindow + 1);
        if (pos < 0) return 0f;

        float[] data = new float[sampleWindow];
        clipMicrofono.GetData(data, pos);

        float suma = 0;
        for (int i = 0; i < sampleWindow; i++)
        {
            suma += data[i] * data[i];
        }
        return Mathf.Sqrt(suma / sampleWindow);
    }
}