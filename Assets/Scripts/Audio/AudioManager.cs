using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Audio
{
    [Serializable]
    public struct AudioData
    {
        public string audioName;
        public AudioClip clip;
    }

    public class AudioManager : MonoBehaviour
    {
        [SerializeField]
        private List<AudioData> audioDatas = new List<AudioData>();

        [SerializeField]
        private AudioSource player;

        private static AudioManager instance = null;
        public static AudioManager Instance
        {
            get
            {
                return instance;
            }
        }

        void Awake()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(this);
            }
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void PlayAudio(string clipName)
        {
            var clipIndex = audioDatas.FindIndex(x => x.audioName == clipName);
            if (clipIndex >= 0)
            {
                this.player.PlayOneShot(audioDatas[clipIndex].clip);
            }
        }
    }
}