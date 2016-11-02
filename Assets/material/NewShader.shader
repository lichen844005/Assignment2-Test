Shader "Custom/NewShader" {
// Adapted from tutorials on Unity website
    Properties{
        _AmbientLightColor("Ambient Light Color", Color) = (1,1,1,1)
        _AmbientLighIntensity("Ambient Light Intensity", Range(0.0, 1.0)) = 1.0
 
        _DiffuseDirection("Diffuse Light Direction", Vector) = (0.1,0.1,0.1,1)
        _DiffuseColor("Diffuse Light Color", Color) = (0,0,0,1)
        _DiffuseIntensity("Diffuse Light Intensity", Range(0.0, 1.0)) = 1.0
		_MainTex("Texture", 2D) = "white" {}
    }
        SubShader
    {
        Pass
        {
            CGPROGRAM
            #pragma target 3.0
            #pragma vertex vertexShader
            #pragma fragment fragmentShader
 
            float4 _AmbientLightColor;
            float _AmbientLighIntensity;
            float3 _DiffuseDirection;
            float4 _DiffuseColor;
            float _DiffuseIntensity;
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
 
            float4 fragmentShader(vertexOut psIn) : SV_Target
            {
				float4 mainColor = tex2D(_MainTex, psIn.texcoord);
                float4 diffuse = saturate(dot(_DiffuseDirection, psIn.normal));
                return saturate(mainColor * (_AmbientLightColor * _AmbientLighIntensity) 
                     + (diffuse * _DiffuseColor * _DiffuseIntensity));
            }
 
            ENDCG
        }
    }
}