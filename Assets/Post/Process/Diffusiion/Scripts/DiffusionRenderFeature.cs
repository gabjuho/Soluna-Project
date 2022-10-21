using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

//Foward Render Data
namespace GamePix.Rendering
{
    public class DiffusionRenderFeature : ScriptableRendererFeature
    {
        [System.Serializable]
        public class Settings
        {
            public RenderPassEvent renderPassEvent = RenderPassEvent.BeforeRenderingPostProcessing;
            public Shader shader;
        }

        //Foward Render Data에 settings 스트럭트 표시
        public Settings settings = new Settings();

        //쉐이더 pass
        private DiffusionPass _pass;

        public override void Create()
        {
            //Foward Render Data에 표시할 이름
            this.name = "Diffusion";
            //쉐이더 pass 설정
            _pass = new DiffusionPass(settings.renderPassEvent, settings.shader);
        }

        public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
        {
            _pass.Setup(renderer.cameraColorTarget);
            renderer.EnqueuePass(_pass);
        }
    }
}