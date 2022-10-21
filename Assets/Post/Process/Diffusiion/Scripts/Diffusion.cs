using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace GamePix.Rendering
{
    [SerializeField, VolumeComponentMenu("GamePix/Diffusion")]
    public class Diffusion : VolumeComponent
    {
        public FloatParameter contrast = new FloatParameter(1.0f);
        public ClampedFloatParameter intensity = new ClampedFloatParameter(0f, 0f, 1f);

        public bool IsActive => intensity.value > 0f;
    }
}