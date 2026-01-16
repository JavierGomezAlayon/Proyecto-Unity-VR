Nosotros hemos realizado un juego de disparos en el que el personaje (jugador) va avanzando de manera progresiva por diferentes zonas en las que aparecen enemigos y tiene que ir disparando para eliminarlos y así, poder avanzar a las siguientes zonas.


### **Cuestiones importantes para el uso**


- El jugador en los niveles se mueve de forma automática hacía adelante.  
- El jugador dispone de 2 pistolas (una en cada mano) con las que tendrá que matar a los enemigos apuntando con ellas.  
- Las pistolas no tienen munición infinita, se dispone de 10 balas en cada pistola que se podrán recargar si el jugador apunta hacía abajo.  
- El jugador tendrá que soplar la boquilla de las pistolas cuando dispare una cierta cantidad de balas para enfriar el arma y que así pueda volver a disparar.
- El jugador dispondrá de un escudo que podrá usar para volverse invensible durante un periodo corto de tiempo, para activarlo debe tocar con la mano izquierda el reloj.
- Para conocer la vida del jugador este tendrá quie mirar el reloj situado en su muñeca derecha.


---


* **Hitos de programación logrados relacionándolos con los contenidos que se han impartido.**

Nosotros hemos hecho uso de las físicas que nos proporciona Unity para realizar este juego, aparte del uso de eventos para gestionar las diferentes mecanicas de manera eficiente y sencilla. También hemos utilizado varios elementos de la asset store para decorar y ambientar el juego junto con el uso de la herramienta de XR Interaction Toolkit para adaptar los controles de las gafas de meta al juego.


### **Aspectos que destacarías en la aplicación**


Nosotros destacaremos la estética de la aplicación la inmersión ya que todo está ambientado en el espacio, y se siente como si estuvieras en el mismo. A su vez el uso de casos reales de juegos de disparos que son el hecho de que te puedas llegar a quedar sin balas y que tengas que hacer la acción de cargar con los mandos.

Hemos relizado varios tipos de enemigos y diferentes niveles cada uno con mayor dificultad.

---


###     **Sensores utilizados**


* **Acelerómetro**


  Nosotros hemos utilizado este sensor para realizar la acción de recarga, a continuación se muestra el trozo de código donde se lleva a cabo su uso:



```c
public class RecargaGesto : MonoBehaviour
{
    public Disparo disparoScript;


    [Header("Configuración")]
    [Tooltip("¿Cuánto hay que inclinar? 1.0 es totalmente vertical, 0.5 es 45 grados.")]
    [Range(0.1f, 1.0f)]
    public float anguloNecesario = 0.7f;


    [Tooltip("Tiempo para evitar que recargue mil veces por segundo")]
    public float tiempoEntreRecargas = 1.0f;
   
    private float ultimoTiempoRecarga;


    void Update()
    {
        // 1. Averiguamos hacia dónde apunta la pistola
        // Vector3.down es el suelo del mundo (0, -1, 0)
        // transform.forward es la flecha azul de tu pistola
       
        // El "Producto Punto" (Dot) nos dice si dos direcciones coinciden.
        // Si es 1, miran igual. Si es -1, miran opuesto.
        float inclinacion = Vector3.Dot(transform.forward, Vector3.down);


        // 2. Comprobamos si miramos al suelo
        // Si 'inclinacion' es mayor que el umbral (ej: 0.7), es que estás apuntando abajo
        if (inclinacion > anguloNecesario)
        {
            // Verificamos el tiempo para no spamear
            if (Time.time > ultimoTiempoRecarga + tiempoEntreRecargas)
            {
                // Intentamos recargar
                // (El script de Disparo ya se encarga de no recargar si está lleno)
                disparoScript.Reload();
               
                ultimoTiempoRecarga = Time.time;
            }
        }
    }
}
```


* **Microfono**
 Nosotros hemos empleado el microfono para soplar el arma en caso de que esta se sobrecaliente si se llega a disparar una cantidad establecida de balas, si se supera dicho límite se debe soplar la boquilla del arma correspondiente, la lógica que hemos utilizado se define a continuación:
```c
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
```


### **Gif animado de ejecución.**

https://github.com/user-attachments/assets/3ed727f7-d5c9-4cd6-9303-15cf088ca543

---


### **Acta de los acuerdos del grupo**


* **Realización del core del juego** (creación de la estructura, escenarios, jugador) → Javier Gómez Alayon  
* **Decoración y diseño de las estructuras** → Paula Díaz Jorge junto con Franco Alla  
* **Diseño del Readme** → Franco Alla  
* **Fase de pruebas** del juego, detección de fallos y posibles mejoras → Todo el grupo.


---


### **Checklist de Validación de Diseño UX en RV**


Esta lista de verificación recopila las buenas prácticas de diseño para aplicaciones de Realidad Virtual, enfocándose en el confort, la usabilidad y la seguridad del usuario.


**Estado:**
- [x] **Contempla**: Implementado correctamente.
- [ ] **No Contempla**: Pendiente o no realizado.
- [~] **No Aplica**: No es relevante para este proyecto.


---


#### 1. Confort y Prevención del Mareo (Motion Sickness)
> El objetivo es minimizar la discrepancia entre la percepción visual y la vestibular.


- [x] **Velocidad Constante**: El usuario se mueve a velocidad constante; se evitan aceleraciones y frenazos bruscos que el cuerpo no siente.
- [x] **Control del Usuario**: El usuario tiene el control del movimiento (anticipación). Si es un movimiento forzado (ej. montaña rusa), el usuario inicia la acción, no arranca sola.
- [ ] **Referencias Estáticas (Cockpit)**: Si el usuario se mueve virtualmente pero está sentado, se usan cabinas, rejillas o sillas visibles como anclaje visual.
- [ ] **Head Tracking Robusto**: Se utiliza seguimiento de 3 grados de libertad (o superior) para reducir el mareo.
- [~] **Gestión de Pérdida de Tracking**: Si falla el seguimiento de la cabeza, la pantalla se desvanece a negro (fade out) en lugar de congelar la imagen.
- [x] **Transiciones de Luz Suaves**: Se evitan cambios bruscos de entornos oscuros a muy brillantes para no causar incomodidad visual.


#### 2. Interfaz de Usuario (UI) e Interacción
> Adaptación al "Canvas infinito" y limitaciones de interacción.


- [x] **Inicio por Confirmación**: La experiencia comienza solo cuando el usuario confirma que está listo (click en pantalla inicial), nunca automáticamente al cargar.
- [x] **Posición de UI**: Los controles aparecen dentro del campo de visión inicial y se reorientan si el usuario se mueve.
- [x] **Uso de Retícula (Reticle)**: Se usa una retícula para ayudar a apuntar a objetivos pequeños o lejanos.
- [~] **Retícula Contextual**: La retícula solo es visible cuando es necesaria (al acercarse a un objetivo o mediante hover), no siempre.
- [~] **Feedback en Gaze**: Si se usa la mirada como botón (dwell time), hay feedback visual claro (ej. cuenta atrás) y la latencia es baja.
- [x] **Separación de Elementos**: Los elementos interactivos están suficientemente separados para evitar selecciones erróneas.


#### 3. Inmersión y Presencia
> Coherencia entre expectativas y estímulos sensoriales.


- [x] **Representación de Manos**: Se usan manos estilizadas (cartoon/robóticas) en lugar de realistas para evitar la disonancia con las manos reales.
- [x] **Propiocepción**: No se representan brazos ni codos, solo las manos, para evitar romper la inmersión por fallos en la estimación de la postura.
- [~] **Coherencia de Control**: Si se ven manos virtuales, la interacción es agarrar; si se ven mandos, la interacción es usar botones/gatillos.
- [ ] **Affordance Clara**: Los objetos que parecen interactivos lo son realmente. Si no se pueden usar, tienen otra apariencia o están fuera del alcance.
- [x] **Audio Espacial**: Se utiliza sonido 3D para que los objetos tengan una localización auditiva coherente en el espacio.
- [x] **Latencia Baja**: La respuesta a las acciones es inmediata (idealmente < 20 ms).


#### 4. Ergonomía y Seguridad
> Respeto a las zonas de confort físico y visual.


- [x] **Escala Correcta**: El tamaño de los objetos es coherente con la escala del mundo real y las expectativas del usuario.
- [~] **Área de Juego Segura**: Si se requieren movimientos físicos (caminar, agacharse), se delimita el área para evitar golpes con el mundo real.
- [x] **Zona de Confort Vertical**: El contenido principal evita obligar al usuario a mirar hacia arriba o abajo más de 60° (evitar dolor de cuello).
- [x] **Zona de Confort Horizontal**: El contenido principal se mantiene en la zona central (visión cómoda aprox. 30° - 55°).
- [x] **Distancia de Visualización**: Los elementos de interés se sitúan en la zona confortable (evitar poner cosas muy cerca de los ojos o excesivamente lejos).


