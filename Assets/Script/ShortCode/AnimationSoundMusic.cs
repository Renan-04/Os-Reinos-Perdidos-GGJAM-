using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AnimationSoundMusic : MonoBehaviour
{
    [SerializeField]
    private UnityEvent onEnableEvent; // M�todo chamado quando o script � habilitado 

    private void OnEnable()
    {

        if (onEnableEvent != null)
        {
            if (GameSounds.Instance.MusicOn == true)
                onEnableEvent.Invoke();
        }

    }
}
