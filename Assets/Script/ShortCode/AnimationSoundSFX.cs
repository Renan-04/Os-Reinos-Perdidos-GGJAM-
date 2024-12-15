using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AnimationSoundSFX: MonoBehaviour
{
    [SerializeField]
    private UnityEvent onEnableEvent; // Método chamado quando o script é habilitado 

    private void OnEnable()
    {

        if (onEnableEvent != null)
        {
            if (GameSounds.Instance.SFXOn == true)
                onEnableEvent.Invoke();
        }

    }
}
