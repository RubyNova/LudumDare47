using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Train;

public class BGMController : MonoBehaviour
{
    [SerializeField] private AudioSource _bgmSource;
    [SerializeField] private TrainCarMover _trainCar;


    private void Start() => _trainCar.TrainCarCrashed += () =>
    {
        _bgmSource.Stop();
    };
}
