using Digger.Modules.Runtime.Sources;
using UnityEngine;

namespace Digger
{
    public class FlyCamera : MonoBehaviour
    {
        [Header("Speed parameters")]
        public float lookSpeed = 300f;
        public float moveSpeed = 20f;

        private float rotationX;
        private float rotationY;

        [SerializeField] private DiggerMasterRuntime dmr;

        private void Start()
        {
            rotationX = transform.localRotation.eulerAngles.y;
        }

        // Update is called once per frame
        private void Update()
        {
            rotationX += Input.GetAxis("Mouse X") * lookSpeed * Time.deltaTime;
            rotationY += Input.GetAxis("Mouse Y") * lookSpeed * Time.deltaTime;
            rotationY = Mathf.Clamp(rotationY, -90, 90);
            transform.localRotation = Quaternion.AngleAxis(rotationX, Vector3.up) * Quaternion.AngleAxis(rotationY, Vector3.left);
            transform.position += (transform.forward * Input.GetAxis("Vertical") + transform.right * Input.GetAxis("Horizontal")) * moveSpeed * Time.deltaTime;

            if (Input.GetMouseButtonDown(0))
            {
                if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, Mathf.Infinity))
                {
                    dmr.Modify(hit.point, Modules.Core.Sources.BrushType.Sphere, Modules.Core.Sources.ActionType.Dig, 0, 0.5f, 4f);
                }
            }
        }
    }
}