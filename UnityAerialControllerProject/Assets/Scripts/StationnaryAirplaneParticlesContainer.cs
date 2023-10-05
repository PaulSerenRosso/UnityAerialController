using HelperPSR.MonoLoopFunctions;
using UnityEngine;

public class StationnaryAirplaneParticlesContainer : MonoBehaviour, IUpdatable
{
    public Transform pivotPoint;

    [SerializeField] private ParticleSystem particleSystem;
    [SerializeField] private float offset;

    private void Start()
    {
        UpdateManager.Register(this);
    }

    private void OnDisable()
    {
        UpdateManager.UnRegister(this);
    }

    public void ActivateParticle()
    {
        if (particleSystem.isPlaying) return; 
        particleSystem.Play();
    }

    public void DeactivateParticle()
    {
        if (!particleSystem.isPlaying) return;
        particleSystem.Stop();
    }

    public void OnUpdate()
    {
        particleSystem.transform.position = pivotPoint.position + offset * Vector3.down;
    }
}