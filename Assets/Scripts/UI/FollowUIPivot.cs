using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class FollowUIPivot : MonoBehaviour
    {
        [SerializeField]
        private Transform followPivot;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            this.transform.position = new Vector3(
                Mathf.Lerp(this.transform.position.x, this.followPivot.position.x, 0.05f),
                Mathf.Lerp(this.transform.position.y, this.followPivot.position.y, 0.005f),
                Mathf.Lerp(this.transform.position.z, this.followPivot.position.z, 0.05f));

            this.transform.forward = Vector3.Cross(this.followPivot.right, Vector3.up);
        }
    }
}