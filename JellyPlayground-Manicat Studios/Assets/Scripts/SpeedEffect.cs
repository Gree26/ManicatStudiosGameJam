using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpeedEffect : MonoBehaviour
{
    [SerializeField]
    private GameObject _speedEffectObject;
    private ParticleSystem _speedEffectSystem;
    private Camera _cam;
    private float _baseFov;
    [SerializeField]
    private float _fovIncrease = 10f;

    private void Start()
    {
        _cam = this.GetComponent<Camera>();
        _baseFov = _cam.fieldOfView;
        _speedEffectSystem = _speedEffectObject.GetComponent<ParticleSystem>();
    }

    private void OnEnable()
    {
        GameDataManager.isSpeedChanged += makeVisable;
    }

    private void OnDisable()
    {
        GameDataManager.isSpeedChanged -= makeVisable;
    }

    private void makeVisable(bool isItVisable)
    {

        _speedEffectObject.SetActive(isItVisable);

        if (isItVisable)
        {
            StartCoroutine(ZoomFov());
        }
        
    }

    private IEnumerator ZoomFov()
    {
        _cam.fieldOfView = _baseFov + _fovIncrease;
        while (_cam.fieldOfView != _baseFov)
        {
            _cam.fieldOfView = Mathf.Clamp(_cam.fieldOfView-.01f, _baseFov, _baseFov + _fovIncrease);
            yield return new WaitForEndOfFrame();
        }
    }
}
