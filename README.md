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

## 3. Configurar un mezclador de sonidos, aplica a uno de los grupo un filtro de echo y el resto de filtros libre. Configura cada grupo y masteriza el efecto final de los sonidos que estás mezclando. Explica los cambios que has logrado con tu mezclador.

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

## 5. Implementar un script en el que el jugador actuve un sonido al colisionar con la esfera.

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
