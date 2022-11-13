using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GamePlay
{
    public class FollowCube : MonoBehaviour
    {
        [SerializeField]
        private Renderer cubeRenderer;

        private Material cubeMat;

        // Start is called before the first frame update
        void Start()
        {
            this.cubeMat = this.cubeRenderer.material;
            this.cubeMat.color = new Color(0.0f, 1.0f, 0.0f, 0.0f);
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void SetAlpha(float alpha)
        {
            this.cubeMat.color = new Color(0.0f, 1.0f, 0.0f, alpha);
        }

        public void SetPosition(Vector3 pos)
        {
            this.transform.forward = Camera.main.transform.forward;
            this.transform.position = pos;
        }
    }
}