using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using ImportantScripts.Core.Singleton;

public class EffectsManager : Singleton<EffectsManager>
{
    public PostProcessVolume postProcessVolume;
    [SerializeField] private Vignette _vignette;

    public float duration = .2f;

    [NaughtyAttributes.Button]
    public void ChangeVignette()
    {
        StartCoroutine(FlashColorVignette());
    }

    IEnumerator FlashColorVignette()
    {
        Vignette tmp;

        if(postProcessVolume.profile.TryGetSettings<Vignette>(out tmp))
        {
            _vignette = tmp;
        }

        ColorParameter c = new ColorParameter();


        float time = 0;
        while(time < duration)
        {
            c.value = Color.Lerp(Color.white, Color.red, time / duration);
            time += Time.deltaTime;
            _vignette.color.Override(c);
            yield return new WaitForEndOfFrame();
        }

        time = 0;
        while(time < duration)
        {
            c.value = Color.Lerp(Color.red, Color.white, time / duration);
            time += Time.deltaTime;
            _vignette.color.Override(c);
            yield return new WaitForEndOfFrame();
        }


    }
}
