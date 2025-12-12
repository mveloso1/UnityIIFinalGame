using UnityEngine;

public class OneShotAudio : MonoBehaviour
{
    public static void PlayClip(AudioClip clip, Vector3 position, float volume = 1f)
    {
        GameObject tempGO = new GameObject("OneShotAudio");
        tempGO.transform.position = position;

        AudioSource aSrc = tempGO.AddComponent<AudioSource>();
        aSrc.clip = clip;
        aSrc.volume = volume;
        aSrc.spatialBlend = 1f; // 3D sound (optional)
        aSrc.Play();

        Object.Destroy(tempGO, clip.length);
    }
}