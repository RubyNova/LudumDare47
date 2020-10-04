using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Train
{
    public class Exploder : MonoBehaviour
    {
        [SerializeField] private TrainCarMover _mover;
        [SerializeField] private ParticleSystem _fireParticles;
        // Start is called before the first frame update
        void Start() => _mover.TrainCarCrashed += () => _fireParticles.Play();

        // Update is called once per frame
        void Update()
        {

        }
    }
}