using UnityEngine;

namespace KamiyoModPack.Kamiyo_Re21341
{
    public class AuraColorFragment : MonoBehaviour
    {
        private void Start()
        {
            var ps = GetComponentInChildren<ParticleSystem>();
            var col = ps.colorOverLifetime;
            col.enabled = true;
            col.color = new Color(1, 1, 1, 0.321f);
        }
    }
}