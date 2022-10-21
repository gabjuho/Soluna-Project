Shader "GamePix/Cartoon"
{
    Properties
    {
        [Header(Texture)]
        [Space(10)]
        [HDR]_TintColor("Base Color",Color) = (1,1,1,1)
        [NoScaleOffset]  _Albedo ("Texture", 2D) = "white" {}

        [Header(Color Settings)]
        [Space(10)]
        _ShadowColor("Shadow Color",Color) = (1,1,1,1)
        _shadowInensitiy("Shadow Intensitiy",Range(0,50)) = 1
        _Smoothness("Smoothness float",Range(0,1)) = 0.1
        [Space(10)]
        _BBScolor("Base Bisde Shadow Color", Color) = (1,1,1,1)
        _BBSfloat("Base Bisde float",Range(0,1)) = 0.1

        [Header(OutLine Settings)]
        [Space(10)]
        _OutlineWidth("Outline Width", Range(0,0.1)) = 0.03
        _OutlineColor("Outline Color", Color) = (0,0,0,1)

        [Header(GI Term)]
        [Space(10)]
        [Enum(UnityEngine.Rendering.CullMode)] _Cull("Cull Mode", Float) = 1
    }
    SubShader
    {
       Tags
        {
            "RenderPipeline"="UniversalRenderPipeline"
            "RenderType"="Opaque"
            "Queue"="Geometry"
        }
        LOD 300
        Cull[_Cull]

        Pass
        {
            Name "FOWARD"

            HLSLPROGRAM

            #pragma prefer_hlslcc gles
            #pragma exclude_renderers d3d11_9x

            #pragma multi_compile_fog
        
	        #pragma vertex vert
	        #pragma fragment frag


            #pragma multi_compile _ _MAIN_LIGHT_SHADOWS
            #pragma multi_compile _ _MAIN_LIGHT_SHADOWS_CASCADE
            #pragma multi_compile _ _SHADOWS_SOFT
            #pragma shader_feature _ALPHATEST_ON
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"

            CBUFFER_START(UnityPerMaterial)
            half4 _ShadowColor, _TintColor, _BBScolor;
            float _Smoothness, _shadowInensitiy;

            float4 _Albedo_ST;
            float _BBSfloat;
            CBUFFER_END

            Texture2D _Albedo;
            SamplerState sampler_Albedo;

            struct VertexInput
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;                                             
                float2 uv : TEXCOORD1;
            };

            struct VertexOutput
            {
                float4 vertex : SV_POSITION;

                float3 normal       : NORMAL;
                float3 normalWS     : TEXCOORD0;
                float3 positionWS   : TEXCOORD3;
                float4 shadowCoord  : TEXCOORD2;

                float2 uv   : TEXCOORD1;
            };

            VertexOutput vert(VertexInput v)
            {
                VertexOutput o  = (VertexOutput)0; 

                o.vertex        = TransformObjectToHClip(v.vertex.xyz);
                o.normalWS      = TransformObjectToWorldNormal(v.normal.xyz);
                o.positionWS    = TransformObjectToWorld(v.vertex.xyz);

                o.uv            = v.uv.xy * _Albedo_ST.xy + _Albedo_ST.zw;


                o.normal        = normalize(mul(v.normal, (float3x3)UNITY_MATRIX_I_M));
               VertexPositionInputs vertexInput = GetVertexPositionInputs(v.vertex.xyz);
               o.shadowCoord   = GetShadowCoord(vertexInput);

                return o;
            }

            half4 frag(VertexOutput i) : SV_Target
            {
            i.shadowCoord   = TransformWorldToShadowCoord(i.positionWS);
            Light light     = GetMainLight(i.shadowCoord);

            float4 albedo = _Albedo.Sample(sampler_Albedo, i.uv);

            float NdotL1 = dot(light.direction, i.normalWS);
            float NdotL2 = saturate(dot(light.direction, i.normalWS)) - _BBSfloat;

            float ISmooth1 = smoothstep(0, _Smoothness, NdotL1);
            float ISmooth2 = smoothstep(0, _Smoothness, NdotL2);

            float4 ILerp;
            ILerp = lerp(_BBScolor, _TintColor, ISmooth2);
            ILerp = lerp(_ShadowColor, ILerp, ISmooth1);

            float3 ambient = SampleSH(i.normal);
            float3 NdotL = saturate(dot(normalize(_MainLightPosition.xyz), i.normal));
            half4 col = half4(albedo * ILerp * light.color.xyz, albedo.a);
            float ISmooth = smoothstep(0, _Smoothness, NdotL);
            ILerp = lerp(_ShadowColor,ILerp,ISmooth);

            col.rgb *= NdotL  * light.shadowAttenuation + (ambient * _shadowInensitiy); // * _MainLightColor.rgb

            return col; 
            }
            ENDHLSL
        }

        Pass
        {
            Name "ShadowCaster"
			Tags
            {
                "LightMode" = "ShadowCaster"
            }

			Cull Back

            HLSLPROGRAM
            
			#pragma prefer_hlslcc gles
			 #pragma exclude_renderers d3d11_9x
			#pragma target 2.0
 
			#pragma vertex ShadowPassVertex
			#pragma fragment ShadowPassFragment

			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"

			struct VertexInput
			{
				float4 positionOS : POSITION;
				float4 normalOS : NORMAL;	
			};

			struct VertexOutput
			{
				float4 vetex : SV_POSITION;
			};

            VertexOutput ShadowPassVertex(VertexInput v)
            {
                VertexOutput o;

				float3 positionWS = TransformObjectToWorld(v.positionOS.xyz);
				float3 normalWS = TransformObjectToWorldNormal(v.normalOS.xyz);

                float4 positionCS = TransformWorldToHClip(ApplyShadowBias(positionWS, normalWS, _MainLightPosition.xyz));

                o.vetex = positionCS;

                return o;
            }
 
            half4 ShadowPassFragment(VertexOutput i) : SV_TARGET
			{
				return 0;
			}

            ENDHLSL
        }

        pass
        {
           Name "DepthOnly"
            Tags{"LightMode" = "DepthOnly"}

            ZWrite On
            ColorMask 0

            Cull Back

            HLSLPROGRAM
           
            #pragma prefer_hlslcc gles
            #pragma exclude_renderers d3d11_9x
            #pragma target 2.0
   
            // GPU Instancing
            #pragma multi_compile_instancing

            #pragma vertex vert
            #pragma fragment frag
               
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
               
            CBUFFER_START(UnityPerMaterial)
            CBUFFER_END
               
            struct VertexInput
            {
                float4 vertex : POSITION;                   
                UNITY_VERTEX_INPUT_INSTANCE_ID
            };

                struct VertexOutput
                {           
                float4 vertex : SV_POSITION;
                 
                UNITY_VERTEX_INPUT_INSTANCE_ID           
                UNITY_VERTEX_OUTPUT_STEREO                 
                };

            VertexOutput vert(VertexInput v)
            {
                VertexOutput o;
                UNITY_SETUP_INSTANCE_ID(v);
                UNITY_TRANSFER_INSTANCE_ID(v, o);
                UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);

                o.vertex = TransformObjectToHClip(v.vertex.xyz);

                return o;
            }

            half4 frag(VertexOutput IN) : SV_TARGET
            {       
                return 0;
            }
            ENDHLSL
        }
    } 
}
