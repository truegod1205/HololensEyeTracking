using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GamePlay
{
    public class TeleportCube : MonoBehaviour
    {
        [SerializeField]
        private Renderer cubeRenderer;

        private Material cubeMat;

        private float fadeoutTime = 2;

        // Start is called before the first frame update
        void Start()
        {
            this.cubeMat = this.cubeRenderer.material;
            this.cubeMat.color = new Color(0.0f, 1.0f, 1.0f, 1.0f);
            this.StartCoroutine(this.Fadeout());
        }

        // Update is called once per frame
        void Update()
        {

        }

        private IEnumerator Fadeout()
        {
            float spawnTime = Time.time;
            while (true)
            {
                float passTime = Time.time - spawnTime;
                if (passTime > fadeoutTime)
                {
                    this.cubeMat.color = new Color(0.0f, 1.0f, 1.0f, 0.0f);
                    break;
                }

                this.cubeMat.color = new Color(0.0f, 1.0f, 1.0f, 1.0f - (passTime / fadeoutTime));
                yield return null;
            }

            GameObject.Destroy(this.gameObject);
        }
    }
}