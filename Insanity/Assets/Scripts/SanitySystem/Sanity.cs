using UnityEngine;

namespace SanitySystem
{
    public class Sanity
    {
        public int sanity { get; set; }

        float sanityNormalized { get; set; }

        public void CalculateValues()
        {
            this.sanity = Mathf.Max(10, Mathf.Min(90, this.sanity));

            if (sanity < 50)
            {
                this.sanityNormalized = (50f - sanity) * 2f / 100f;
            }
            else
            {
                this.sanityNormalized = (sanity - 50f) * 2f / 100f;
            }
        }

        public float GetHealthModifier()
        {
            if (sanity < 50f)
            {
                return 1f - sanityNormalized;
            }
            else
            {
                return 1f + sanityNormalized;
            }
        }

        public float GetStrengthModifier()
        {
            return 2f - GetHealthModifier();
        }
    }
}
