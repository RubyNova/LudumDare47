using UnityEngine;

namespace Train
{
    public class TurretAimController : MonoBehaviour
    {
        [Header("Dependencies")]
        [SerializeField] private Camera _camera;
        [SerializeField] private Transform _muzzle;
        [SerializeField] private GameObject _bulletPrefab;
        [SerializeField] private float _bulletCooldown;

        private float _timeToNextBullet = 0f;
    
        // Start is called before the first frame update
        void Start()
        {
            _timeToNextBullet = _bulletCooldown;
        }

        // Update is called once per frame
        void Update()
        {
            var mouseInput = _camera.ScreenToWorldPoint(Input.mousePosition);
            var mouseDirection = mouseInput.normalized;
            var diff = mouseInput - transform.position;
            float angle = (Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg) - 90;
            transform.rotation = Quaternion.Euler(0, 0, angle);

            Debug.DrawRay(_muzzle.transform.position, _muzzle.transform.up, new Color(1, 1, 1, 1));

            if (Input.GetKeyDown(KeyCode.Mouse0) && _timeToNextBullet <= 0f)
            {
                var go = Instantiate(_bulletPrefab, _muzzle.position, _muzzle.rotation); //THIS WORKS :tm:
                _timeToNextBullet = _bulletCooldown;
            }

            _timeToNextBullet -= Time.deltaTime;
        }
    }
}
