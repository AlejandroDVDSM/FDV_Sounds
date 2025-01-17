# FDV_Sounds

## 1. Añadir un audio que se debe reproducir en cuanto se carga la escena y en bucle.

Añadimos un objeto con el componente `AudioSource` a la escena y e inicalizamos `AudioClip` con el audio que deseemos reproducir. Para que se reproduzca cuando se carga la escena y en bucle deberemos marcar las opciones `Play On Awake` y `Loop`.

![image](https://github.com/user-attachments/assets/ab3a7113-1c26-4526-8675-3985908923c9)


https://github.com/user-attachments/assets/5355637f-44a6-4171-bac1-f8b230b1b2d7


## 2. Crea un objeto con una fuente de audio a la que le configures el efecto Doppler elevado y que se mueva a al pulsar la tecla m a una velocidad alta.

Añadimos una esfera en la escena y le añadimos un `AudioSource` que tendrá la propiedad `Spatial Blend` a 1 para que Unity lo trate como un audio 3D. Luego, realizamos los siguientes cambios:

1. Incrementamos el valor de `Doppler Level` para alternar el pitch en función de la velocidad del GameObject.
2. Incrementamos el valor de `Spread` para regular el audio estéreo.
3. Incrementamos los valores de `Min Distance` y `Max Distance`.
4. Cambiamos la propiedad `Volume Roloff` a `Linear Roloff`. Al cambiarlo a este modo, entre más lejos estemos de la fuente que reproduce el sonido, menos escucharemos.

_Resultado con `Volume Roloff` a `Logarithmic Rolloff`:_

![image](https://github.com/user-attachments/assets/38838be1-4e48-4d2c-b023-1154d5e38810)

https://github.com/user-attachments/assets/4b8da8ea-f192-4812-8bd1-9640d4d31465


_Resultado con `Volume Roloff` a `Linear Roloff`:_

![image](https://github.com/user-attachments/assets/2b54649f-55e8-43fe-b1e9-220107f985f1)

https://github.com/user-attachments/assets/0c06a3e2-379a-45d4-b126-2cd5ff8d2337

## 3. Configurar un mezclador de sonidos, aplica a uno de los grupo un filtro de eco y el resto de filtros libre. Configura cada grupo y masteriza el efecto final de los sonidos que estás mezclando. Explica los cambios que has logrado con tu mezclador.

Creamos un `AudioMixer` haciendo click derecho desde la pestaña _Project_ y seleccionando la opción `Create > Audio Mixer`

![image](https://github.com/user-attachments/assets/b9d04895-c04d-4194-b5ad-681bd3b28b26)

Ahora, creamos dos grupos distintos: _Music_ y _Echo_.

![image](https://github.com/user-attachments/assets/8c4df56a-bde0-442d-95cf-475defab6518)

Añadimos el efecto `Echo` al grupo _Echo_ y cambiamos los valores de `Decay` y `Drymix` a 50% para que el sonido del eco decaiga a lo largo del tiempo. El grupo _Music_ no tendrá efecto alguno.

![image](https://github.com/user-attachments/assets/eab2a8a5-4853-40ac-9c0a-aca70bd4f451)

Por último, añadimos los grupos a sus `AudioSource` respectivos.

![image](https://github.com/user-attachments/assets/c8c5bafa-4703-4485-975a-8925dcb6d3af)

_Sin añadir mixer:_

https://github.com/user-attachments/assets/8ecff50f-ee79-4b33-8e37-5c10a0859fd0

_Con el mixer activo:_

https://github.com/user-attachments/assets/c4ed7427-32df-4360-9852-6c16a3cdf669

## 4. Implementar un script que al pulsar la tecla `P` accione el movimiento de una esfera en la escena y reproduzca un sonido en bucle hasta que se pulse la tecla `S`.

```c#
using UnityEngine;

public class MovingAudio : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 10.0f;

    [SerializeField] private AudioSource _audioSource;

    private bool _isMovementEnabled;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            _isMovementEnabled = !_isMovementEnabled;

            if (!_audioSource.isPlaying)
            {
                Debug.Log($"Key <P> pressed. Playing audio: {_audioSource.clip.name}");
                _audioSource.Play();
            }
        }

        if (Input.GetKeyUp(KeyCode.S))
        {
            if (_audioSource.isPlaying)
            {
                Debug.Log($"Key <S> pressed. Stopping audio: {_audioSource.clip.name}");
                _audioSource.Stop();
            }
        }

        if (_isMovementEnabled)
            transform.Translate(_moveSpeed * Time.deltaTime, 0, 0);
    }
}
```

https://github.com/user-attachments/assets/911127cc-853e-4c95-8c54-af1b7dad9a5d

## 5. Implementar un script en el que el jugador active un sonido al colisionar con la esfera.

Se añade el siguiente script a la esfera, que contendra un `AudioSource` y un `SphereCollider`:

```c#
using UnityEngine;

public class PlayOnCollision : MonoBehaviour
{
    private AudioSource _audioSource;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (!_audioSource.isPlaying)
                _audioSource.Play();
        }
    }
}
```

https://github.com/user-attachments/assets/84560a6e-4fd0-414c-8805-f15d8e73d185


## 6. Modificar el script anterior para que según la velocidad a la que se impacte, el cubo lance un sonido más fuerte o más débil.

```cs
using UnityEngine;

public class AudioSpeed : MonoBehaviour
{
    private AudioSource _audioSource;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (!_audioSource.isPlaying)
            {
                _audioSource.volume = other.gameObject.GetComponent<PlayerMovement>().MoveSpeed / 100;
                _audioSource.Play();
            }
        }
    }
}
```

https://github.com/user-attachments/assets/1ac686cf-0b73-4a93-a4d7-c7d45f4c3470

## 7. Agregar un sonido de fondo a la escena que se esté reproduciendo continuamente desde que esta se carga. Usar un mezclador para los sonidos.

Esto apartado es exactamente lo mismo que el apartado 3, donde se empleó un mezclador con dos grupos distintos, uno para la música de fondo y otro para los sonidos.

## 8. Crear un script para simular el sonido que hace el jugador cuando está en movimiento (mecánica para reproducir sonidos de pasos).

Añadimos un `AudioSource` al personaje y desmarcamos las opciones `Play On Awake` y `Loop`. También es importante dejar la referencia de `AudioClip` sin asignar, ya que la asignaremos directamente desde el código.

![image](https://github.com/user-attachments/assets/b9fe867c-b551-442e-a836-b78dbe5afc27)

A continuación, en nuestro script creamos un array de `AudioClip` que serán reproducidos mientras el personaje se mueve.

```cs
using UnityEngine;
using Random = System.Random;

public class PlayerMovementAudio : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 1.0f;

    [SerializeField] private AudioClip[] _audioClips;
    
    private AudioSource _audioSource;
    private Random _random;
    
    public float MoveSpeed => _moveSpeed;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _random = new Random();
    }

    void Update()
    {
        var horizontalMovement = Input.GetAxis("Horizontal");
        var verticalMovement = Input.GetAxis("Vertical");

        if (horizontalMovement != 0 || verticalMovement != 0)
        {
            if (!_audioSource.isPlaying)
            {
                _audioSource.clip = _audioClips[_random.Next(0, _audioClips.Length - 1)];
                _audioSource.Play();
            }
            transform.Translate(Input.GetAxis("Horizontal") * _moveSpeed * Time.deltaTime, 0, Input.GetAxis("Vertical") * _moveSpeed * Time.deltaTime);
        }
        
    }
}
```

![image](https://github.com/user-attachments/assets/daf3b8d9-d9a6-4539-baa6-4c5ac5493040)

https://github.com/user-attachments/assets/aa453ba3-da58-4341-89e9-0da36ead1924

## 9. En la escena de tus ejercicios 2D incorpora efectos de sonido ajustados a los siguientes requisitos:

1. Crea un grupo SFX en el AudioMixer para eventos:
    * Movimiento del personaje: Crea sonidos específicos para saltos y aterrizajes.
    * Interacción y recolección de objetos: Diseña sonido para la recolección de objetos.
    * Indicadores de salud/vida: Diseña un sonido breve y distintivo para cada cambio en el estado de salud (por ejemplo, ganar o perder vida).
* Crea un grupo Ambiente:
    * Crea un sonido de fondo acorde con el ambiente
    * Agrega una zona específica del juego en que el ambiente cambie de sonido
* Crea un grupo para música:
    * Crea un loop de música de fondo acorde al tono del juego

![image](https://github.com/user-attachments/assets/b72d6671-5ee7-40d0-b575-546a708cc18d)



https://github.com/user-attachments/assets/67ef8155-173c-4f9e-9bdd-d16c116ae90a





## Créditos

* Música de fondo por alkakrab: https://alkakrab.itch.io/fantasy-rpg-soundtrack-music.
* Sonidos de disparos por SnakeF8: https://f8studios.itch.io/snakes-authentic-gun-sounds.
* Sonidos de pasos por Pelatho: https://thowsenmedia.itch.io/video-game-footstep-sound-pack.
* Sonido de recompensa por Cyberwave Orchestra: https://cyberwave-orchestra.itch.io/hints-stars-points-rewards-pack.
* Sonido de curación por Leohpaz: https://leohpaz.itch.io/50-rpg-healing-buffs-sfx.
* Sonido de daño por VoiceBosch: https://voicebosch.itch.io/taking-damage-sounds-male-grunts-audio-pack.
* Sonido ambiental de pájaros por Ham: https://zombieham.itch.io/bird-sounds-mini-pack.
* Sonido ambiental de cafetería por Diablo Luna ど苛ッ: https://pudretediablo.itch.io/butterfly.
