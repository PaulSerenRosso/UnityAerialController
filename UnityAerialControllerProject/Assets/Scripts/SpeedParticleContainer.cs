using UnityEngine;

public class SpeedParticleContainer : MonoBehaviour
{
    [SerializeField] private float maxParticleCount;

    [SerializeField] private float minParticleCount;

    [SerializeField] private float minParticleSpeed;

    [SerializeField] private float maxParticleSpeed;

    [SerializeField] private ParticleSystem particleSystem;

    private void Start()
    {
        var particleSystemColorBySpeed = particleSystem.colorBySpeed;
        particleSystemColorBySpeed.range = new Vector2(minParticleSpeed, maxParticleSpeed);
        DeactivateParticle();
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

    public void UpdateParticle(float speedNormalized)
    {
        var particleSystemEmission = particleSystem.emission;
        particleSystemEmission.rateOverTime =
            new ParticleSystem.MinMaxCurve(minParticleCount + speedNormalized * maxParticleCount);
        var particleSystemMain = particleSystem.main;
        particleSystemMain.startSpeed =
            new ParticleSystem.MinMaxCurve(minParticleSpeed + speedNormalized * maxParticleSpeed);
    }
}