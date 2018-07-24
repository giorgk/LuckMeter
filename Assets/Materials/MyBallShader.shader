Shader "MyShaders/BallShader"
{
	Properties
	{
		_MainColor ("Main Color", Color) = (1, 0.5, 0.7, 1)
		_SecColor ("Secondary Color", Color) = (0, 0.5, 0.3, 1)
		_MainTex("Mask", 2D) = "white"{}
	}
	SubShader
	{
		Tags{"RenderType" = "Opaque"}

		CGPROGRAM
		#pragma surface surf Lambert

		sampler2D _MainTex;
		float4 _MainColor;
		float4 _SecColor;

		struct Input{
			float2 uv_MainTex;
			//float4 maincol : COLOR;
			//float4 seccol : COLOR;
		};

		void surf (Input IN, inout SurfaceOutput o){

		fixed4 maintex = tex2D(_MainTex, IN.uv_MainTex);

		o.Albedo  =maintex.rgb * _MainColor.rgb + (1-maintex)* _SecColor.rgb;

		}
		ENDCG
	}
	Fallback "Diffuse"
}
