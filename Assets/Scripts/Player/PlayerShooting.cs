using System;
using Enemy;
using UnityEngine;

namespace Player
{
    /// <inheritdoc />
    /// <summary>
    /// Class the controls the player's shooting.
    /// </summary>
    public class PlayerShooting : MonoBehaviour
    {
        public int damagePerShot = 20;
        public float timeBetweenBullets = 0.15f;
        public float range = 100f;

        private float timer;
        private Ray shootRay;
        private RaycastHit shootHit;
        private int shootableMask;
        private ParticleSystem gunParticles;
        private LineRenderer gunLine;
        private AudioSource gunAudio;
        private Light gunLight;
        private float effectsDisplayTime = 0.2f;

        /// <summary>
        /// Disable shooting effects.
        /// </summary>
        public void DisableEffects()
        {
            gunLine.enabled = false;
            gunLight.enabled = false;
        }

        private void Awake()
        {
            shootableMask = LayerMask.GetMask("Shootable");
            gunParticles = GetComponent<ParticleSystem>();
            gunLine = GetComponent<LineRenderer>();
            gunAudio = GetComponent<AudioSource>();
            gunLight = GetComponent<Light>();
        }

        private void Update()
        {
            timer += Time.deltaTime;
            if (Input.GetButton("Fire1") && timer >= timeBetweenBullets && Math.Abs(Time.timeScale) > 0)
            {
                Shoot();
            }

            if (timer >= timeBetweenBullets * effectsDisplayTime)
            {
                DisableEffects();
            }
        }

        private void Shoot()
        {
            timer = 0f;
            gunAudio.Play();
            gunLight.enabled = true;
            gunParticles.Stop();
            gunParticles.Play();
            gunLine.enabled = true;
            var position = transform.position;
            gunLine.SetPosition(0, position);
            shootRay.origin = position;
            shootRay.direction = transform.forward;
            if (Physics.Raycast(shootRay, out shootHit, range, shootableMask))
            {
                shootHit.collider.GetComponent<EnemyHealth>()?.TakeDamage(damagePerShot, shootHit.point);
                gunLine.SetPosition(1, shootHit.point);
            }
            else
            {
                gunLine.SetPosition(1, shootRay.origin + shootRay.direction * range);
            }
        }
    }
}