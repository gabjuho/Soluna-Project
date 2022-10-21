Shader "Unlit/GamePix/WaterDrop_Mask"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Size("Size",float) = 1
        _T("Time Scale",float) = 1
        _Distortion("Distortion",Range(-5,5)) = 1
        _Blur("Blur intensitiy", Range(0,1)) = 1
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #pragma multi_compile_fog

            #define S(a,b,t) smoothstep(a,b,t);

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float _Size, _T, _Distortion, _Blur;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            float N21(float2 p)                                          //Random Patren
            {
                p = frac(p*float2(123.34, 345.45));
                p += dot(p, p + 34.345);

                return frac(p.x * p.y);
;            }

            float3 Layer(float2 UV, float t)
            {
               float2 aspact = float2(2,1);                             //직사각형 uv
               float2  uv = UV * _Size * aspact;
               uv.y += t* 0.25;                                         //uv를 움직여서 sin값으로 올라오는 물방울 그대로 보이게 하기.
               float2 gv = frac(uv) - 0.5;                              //x의 소수점 이하 부분을 리턴한다.
               float2 id = floor(uv);

               float n = N21(id);                                       //노이즈
               t += n * 6.2831;                                         //시간에 노이즈를 더해서 랜덤으로 움직일수 있게 만들기
               
               float w = UV.y * 10;                                   //흔들리는 값
               float x = (n - 0.5)*0.8;
               x += (0.4 - abs(x)) * sin(3*w)*pow(sin(w),6) * 0.45;     //물방울 x pos
               float y = -sin(t + sin(t + sin(t) * 0.5)) * 0.45;        //물방울 y pos
               y -= (gv.x-x) * (gv.x-x);                                //물방울 모양

               float2 dropPos = (gv- float2(x,y))/aspact;               //물방울 위치값
               float drop = S(0.05, 0.03, length(dropPos));             //물방울 

               float2 trailPos = (gv- float2(x, t * 0.25))/aspact;      //trail 위치값 (작은 물방울)               
               trailPos.y = (frac(trailPos.y * 8) - 0.5) /  8;          //trail
               float trail = S(0.03, 0.01, length(trailPos));           //물방울 움직인곳에 trail
               float fogTrail = S(-0.05, 0.05, dropPos.y);              //물안개
               fogTrail *= S(0.5, y, gv.y);             
               trail *= fogTrail;
               fogTrail *= S(.05, .04, abs(dropPos.x));                 //물안개 효과

               //col += fogTrail * 0.5;
               //col += trail;
               //col += drop;

               //col *= 0; col.rg += dropPos;
               float2 offs = drop * dropPos + trail * trailPos;         //물방울 난반사도
               //if(gv.x > 0.48 || gv.y > 0.49) col = float4(1,0,0,1);    //debug box

               return float3(offs, fogTrail);
            }


            fixed4 frag (v2f i) : SV_Target
            {
               float t = fmod(_Time.y + _T,7200);                        //시간
               float4 col = 0;                                           //Color

               float3 drops = Layer(i.uv, t);
               drops += Layer(i.uv * 1.25 + 7.54, t);
               drops += Layer(i.uv * 1.35 + 1.54, t);
               drops += Layer(i.uv * 1.60 - 7.54, t);

               float blur = _Blur * 7 * (1 - drops.z);
               col = tex2Dlod(_MainTex,float4( i.uv + drops.xy * _Distortion,0, blur));//물방울 텍스쳐화
               col.a = 255;
               return col;
            }
            ENDHLSL
        }
    }
}
