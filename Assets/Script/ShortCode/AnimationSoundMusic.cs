using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AnimationSoundMusic : MonoBehaviour
{
    [SerializeField]
    private UnityEvent onEnableEvent; // Método chamado quando o script é habilitado 

    private void OnEnable()
    {

        if (onEnableEvent != null)
        {
            if (GameSounds.Instance.MusicOn == true)
                onEnableEvent.Invoke();
        }

    }
}
