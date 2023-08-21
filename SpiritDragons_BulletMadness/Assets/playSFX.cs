using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playSFX : MonoBehaviour
{
    AudioSource aSource;
    [SerializeField] GameObject buttons;
    [SerializeField] GameObject panel;
    private void Start()
    {
        aSource = GetComponent<AudioSource>();
    }

    public void playSound()
    {
        StartCoroutine(play());
    }

    IEnumerator play()
    {
        aSource.Play();
        yield return new WaitForSeconds(.25f);
        panel.SetActive(true);
        buttons.SetActive(false);
    }
}
