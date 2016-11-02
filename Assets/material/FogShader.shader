Shader "Custom/FogShader" {
	// Adapted from tutorials on Unity website
	Properties{
		_MainTex("Texture", 2D) = "white" {}
	}
		SubShader
	{
		Tags {"RenderType"="Opaque"}
		LOD 100
		Pass
	{
		CGPROGRAM
#pragma vertex vertexShader
#pragma fragment fragmentShader
#pragma multi_compile_fog

#include "UnityCG.cginc"

		struct vertexIn {
			float4 position : POSITION;
			float2 texcoord : TEXCOORD0;
		};

		struct vertexOut {
			float4 position : SV_POSITION;
			half2 texcoord : TEXCOORD0;
			UNITY_FOG_COORDS(1)
		};

		sampler2D _MainTex;
		float4 _MainTex_ST;

		vertexOut vertexShader(vertexIn v)
		{
			vertexOut o;
			o.position = mul(UNITY_MATRIX_MVP, v.position);
			o.texcoord = TRANSFORM_TEX(v.texcoord, _MainTex);
			UNITY_TRANSFER_FOG(o, o.position);
			return o;
		}

		float4 fragmentShader(vertexOut i) : SV_Target
		{
			float4 mainColor = tex2D(_MainTex, i.texcoord);

			UNITY_APPLY_FOG(i.fogCoord, mainColor);
			return mainColor;
		}

			ENDCG
		}
	}
}