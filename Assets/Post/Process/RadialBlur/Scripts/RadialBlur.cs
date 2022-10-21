using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace GamePix.Rendering
{
    [SerializeField, VolumeComponentMenu("GamePix/RadialBlur")]
    public class RadialBlur : VolumeComponent
    {
        public ClampedFloatParameter amount = new ClampedFloatParameter(0f, 0f, 1f);
        public FloatParameter blurSize = new FloatParameter(0.1f);
        public Vector2Parameter blurCenterPos = new Vector2Parameter(new Vector2(0.5f, 0.5f));
        public ClampedIntParameter sampleCount = new ClampedIntParameter(8, 1, 48);

        public bool IsActive => amount.value > 0f;
    }
}

