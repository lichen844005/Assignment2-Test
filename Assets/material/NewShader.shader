Shader "Custom/NewShader" {

	Properties{
		_AmbientLightColor("Ambient Light Color", Color) = (1,1,1,1)
		_AmbientLighIntensity("Ambient Light Intensity", Range(0.0, 1.0)) = 1.0

		_DiffuseDirection("Diffuse Light Direction", Vector) = (0.1,0.1,0.1,1)
		_DiffuseColor("Diffuse Light Color", Color) = (0,0,0,1)
		_DiffuseIntensity("Diffuse Light Intensity", Range(0.0, 1.0)) = 1.0

		_SpecularColor("Specular Light Color", Color) = (1,1,1,1)
		_SpecularIntensity("Specular Light Intensity", Range(0.0, 1.0)) = 1.0
		_SpecularShininess("Shininess", Float) = 10

		_MainTex("Texture", 2D) = "white" {}
    }
        SubShader
    {
        Pass
        {
			Tags{ "LightMode" = "ForwardBase" }
            CGPROGRAM
            #pragma target 3.0
            #pragma vertex vertexShader
            #pragma fragment fragmentShader
 
            float4 _AmbientLightColor;
            float _AmbientLighIntensity;
            float3 _DiffuseDirection;
            float4 _DiffuseColor;
            float _DiffuseIntensity;
			float4 _RimColor;
			float _RimPower;
			sampler2D _MainTex;
 
            struct vertexIn {
                float4 position : POSITION;
                float3 normal : NORMAL;
				float2 texcoord : TEXCOORD0;
            };
 
            struct vertexOut {
                float4 position : SV_POSITION;
                float3 normal : NORMAL;
				float2 texcoord : TEXCOORD0;
            };
 
            vertexOut vertexShader(vertexIn v)
            {
                vertexOut o;
                o.position = mul(UNITY_MATRIX_MVP, v.position);
				o.texcoord = v.texcoord;
                o.normal = v.normal;
                return o;
            }
 
            float4 fragmentShader(vertexOut i) : SV_Target
            {
				float4 mainColor = tex2D(_MainTex, i.texcoord);
                float4 diffuse = saturate(dot(_DiffuseDirection, i.normal));
                return saturate(mainColor * (_AmbientLightColor * _AmbientLighIntensity) 
                     + (diffuse * _DiffuseColor * _DiffuseIntensity));
            }
 
            ENDCG
        }
		Pass{
			Tags{ "LightMode" = "ForwardAdd" }
			Blend One One

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"

			float4 _Color;
			float4 _LightColor0;
			float _Shininess;
			float4 _SpecularColor;
			float _SpecularIntensity;
			float _SpecularShininess;

			struct vertexInput {
				float4 vertex : POSITION;
				float3 normal : NORMAL;
			};

			struct vertexOutput {
				float4 position : SV_POSITION;
				float3 normal : NORMAL;
				float4 posWorld : TEXCOORD0;
				float3 normalDir : TEXCOORD1;
			};

			vertexOutput vert(vertexInput v) {
				vertexOutput o;
				o.position = mul(UNITY_MATRIX_MVP, v.vertex);
				o.normal = v.normal;
				o.posWorld = mul(unity_ObjectToWorld, v.vertex);
				o.normalDir = normalize(mul(v.normal, unity_WorldToObject).xyz);
				return o;
			}

			fixed4 frag(vertexOutput i) : SV_Target
			{
				float3 specularReflection;
				float3 normalDirection = normalize(i.normalDir); //finds normal of vertex

				float3 viewDirection = normalize(_WorldSpaceCameraPos - i.posWorld.xyz); //gets view direction
				float3 lightDirection;
				float attenuation;
				if (0.0 == _WorldSpaceLightPos0.w) // directional light?
				{
					attenuation = 1.0; // no attenuation
					lightDirection = normalize(_WorldSpaceLightPos0.xyz); //normalizes the light position
				}
				else // point or spot light
				{
					float3 vertexToLightSource = _WorldSpaceLightPos0.xyz - i.posWorld.xyz; // find vertex to light pos to world position
					attenuation = 1.0 / length(vertexToLightSource); // attenuation drop-off power
					lightDirection = normalize(vertexToLightSource); // normalize vector between light and vertex
				}

				if (dot(normalDirection, lightDirection) < 0.0)
				{
					specularReflection = float3(0.0, 0.0, 0.0);
				}
				else
				{
					specularReflection = attenuation * _SpecularColor.rgb * pow(max(0.0, dot(
						reflect(-lightDirection, normalDirection),
						viewDirection)), _SpecularShininess);
				}

				return _LightColor0 * (_SpecularIntensity * float4(specularReflection , 1));
			}
			ENDCG
		}
    }
}
