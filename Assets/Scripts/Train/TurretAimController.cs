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
        [SerializeField] private AudioSource _shootingSound;
        [SerializeField] private ParticleSystem _gunshotParticleSystem;
        private bool _canShoot = true;
        private bool _burstShot;

        public bool BurstShot
        {
            get => _burstShot;
            set => _burstShot = value;
        }

        public bool CanShoot
        {
            get => _canShoot;
            set => _canShoot = value;
        }

        public float BulletCooldown
        {
            get => _bulletCooldown;
            set => _bulletCooldown = value;
        }

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

            if (Input.GetKeyDown(KeyCode.Mouse0) && _timeToNextBullet <= 0f && _canShoot)
            {
                _shootingSound.Play();
                Instantiate(_bulletPrefab, _muzzle.position, _muzzle.rotation); //THIS WORKS :tm:
                if (_burstShot) ShootMultiple(angle);
                _timeToNextBullet = _bulletCooldown;
                _gunshotParticleSystem.Play();
            }

            _timeToNextBullet -= Time.deltaTime;
        }

        private void ShootMultiple(float baseAngle)
        {
            var angle = 5f;
            var muzzleRot = _muzzle.rotation;
            var leftRotation = Quaternion.Euler(0, 0, baseAngle - angle);
            var leftRot2 = Quaternion.Euler(0, 0, baseAngle - angle * 2);
            var rightRotation = Quaternion.Euler(0, 0, baseAngle + angle);
            var rightRot2 = Quaternion.Euler(0, 0, baseAngle + angle * 2);

            var position = _muzzle.position;
            var go = Instantiate(_bulletPrefab, position, leftRotation);
            Instantiate(_bulletPrefab, position, rightRotation);
            Instantiate(_bulletPrefab, position, leftRot2);
            Instantiate(_bulletPrefab, position, rightRot2);
            
        }
    }
}
