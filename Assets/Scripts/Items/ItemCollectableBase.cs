using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Items
{

    public class ItemCollectableBase : MonoBehaviour
    {

        public SFXType SFXType;
        public ItemType itemType;
        public string compareTag = "Player";
        public ParticleSystem coinParticleSystem;
        public float timeToHide = 1;
        public GameObject graphicItem;

        public Collider collider;

        [Header("Sounds")]
        public AudioSource audioSource;

        private void Awake() 
        {
            if(coinParticleSystem != null) coinParticleSystem.transform.SetParent(null);
        }

        private void OnTriggerEnter(Collider collision) 
        {
            if(collision.transform.CompareTag(compareTag)) 
            {
                Collect();
            }
        }

        private void PlaySFX()
        {
            SFXPool.Instance.Play(SFXType);
        }

        protected virtual void Collect() 
        {
            PlaySFX();
            if(collider != null) collider.enabled = false;
            if(graphicItem != null) graphicItem.SetActive(false);
            OnCollect();
            HideObject();
        }

        private void HideObject()
        {
        gameObject.SetActive(false);
        }

        protected virtual void OnCollect() 
        { 
            if(coinParticleSystem != null) coinParticleSystem.Play();
            if(audioSource != null) audioSource.Play();
            if(coinParticleSystem != null) coinParticleSystem.transform.parent = null;
            if(coinParticleSystem != null) Destroy(coinParticleSystem.gameObject, 2f);
            ItemManager.Instance.AddByType(itemType);
        }

    }
}
