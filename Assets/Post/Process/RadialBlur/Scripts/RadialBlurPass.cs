using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace GamePix.Rendering
{
    public class RadialBlurPass : CustomPostProcessingPass<RadialBlur>
    {
        private static readonly int BlurCenterId = UnityEngine.Shader.PropertyToID("_BlurCenterPos");
        private static readonly int BlurSizeId = UnityEngine.Shader.PropertyToID("_BlurSize");
        private static readonly int SampleId = UnityEngine.Shader.PropertyToID("_Samples");
        private static readonly int AmountId = UnityEngine.Shader.PropertyToID("_Amount");

        public RadialBlurPass(RenderPassEvent renderPassEvent, Shader shader) : base(renderPassEvent,shader)
        {
        }

        protected override string RenderTag => "RadialBlur";

        protected override void BeforeRender(CommandBuffer commandBuffer, ref RenderingData renderingData)
        {
            Material.SetVector(BlurCenterId, Component.blurCenterPos.value);
            Material.SetFloat(BlurSizeId, Component.blurSize.value * 0.1f);
            Material.SetInt(SampleId, Component.sampleCount.value);
            Material.SetFloat(AmountId, Component.amount.value);
        }

        protected override bool IsActive()
        {
            return Component.IsActive;
        }
    }
}
