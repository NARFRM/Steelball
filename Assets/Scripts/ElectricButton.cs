using UnityEngine;

public class ElectricParticlesController : MonoBehaviour
{
    public ParticleSystem electricParticles;
    public float repelStrength = 5f; // Fuerza de repulsión

    private void Update()
    {
        if (electricParticles == null) return;

        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = Camera.main.nearClipPlane;
        Vector3 worldMousePos = Camera.main.ScreenToWorldPoint(mousePosition);

        // Obtener las partículas
        ParticleSystem.Particle[] particles = new ParticleSystem.Particle[electricParticles.particleCount];
        electricParticles.GetParticles(particles);

        for (int i = 0; i < particles.Length; i++)
        {
            Vector3 direction = (particles[i].position - worldMousePos).normalized;
            particles[i].velocity += direction * repelStrength * Time.deltaTime;
        }

        electricParticles.SetParticles(particles, particles.Length);
    }
}
