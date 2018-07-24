// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "MyShaders/BallShader"
{
	Properties
	{
		[HideInInspector] __dirty( "", Int ) = 1
		_BckGrndColor("BckGrndColor", Color) = (0.9264706,0.456423,0.456423,0)
		_CircleColor("CircleColor", Color) = (0,0.006896496,1,0)
		_Mask("Mask", 2D) = "white" {}
		_EmmitValue("EmmitValue", Range( 0 , 1)) = 0
		_TextureSample0("Texture Sample 0", 2D) = "white" {}
		_TextColor("TextColor", Color) = (0,0,0,0)
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" "IsEmissive" = "true"  }
		Cull Back
		CGPROGRAM
		#pragma target 3.0
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows exclude_path:deferred 
		struct Input
		{
			float2 uv_texcoord;
		};

		uniform float4 _TextColor;
		uniform float4 _BckGrndColor;
		uniform float4 _CircleColor;
		uniform sampler2D _Mask;
		uniform float4 _Mask_ST;
		uniform sampler2D _TextureSample0;
		uniform float4 _TextureSample0_ST;
		uniform float _EmmitValue;

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 uv_Mask = i.uv_texcoord * _Mask_ST.xy + _Mask_ST.zw;
			float4 temp_output_9_0 = ( 1.0 - tex2D( _Mask, uv_Mask ) );
			float2 uv_TextureSample0 = i.uv_texcoord * _TextureSample0_ST.xy + _TextureSample0_ST.zw;
			float4 tex2DNode15 = tex2D( _TextureSample0, uv_TextureSample0 );
			float4 temp_output_18_0 = ( temp_output_9_0 + tex2DNode15 );
			o.Albedo = lerp( _TextColor , lerp( _BckGrndColor , _CircleColor , temp_output_9_0.x ) , temp_output_18_0.x ).rgb;
			o.Emission = ( ( 1.0 - temp_output_18_0 ) * _EmmitValue ).xyz;
			o.Metallic = float4(0,0,0,1).r;
			o.Smoothness = float4(0.6691177,0.6691177,0.6691177,1).r;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=10001
376;217;1634;824;1738.796;315.1522;1.3;True;False
Node;AmplifyShaderEditor.SamplerNode;3;-1120.4,326.2;Float;True;Property;_Mask;Mask;2;0;Assets/Resources/ball_mask_1.png;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;0.0;False;5;FLOAT4;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.OneMinusNode;9;-767.3999,235.7998;Float;True;1;0;FLOAT4;0.0;False;1;FLOAT4
Node;AmplifyShaderEditor.SamplerNode;15;-1139.294,939.9467;Float;True;Property;_TextureSample0;Texture Sample 0;4;0;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;0.0;False;5;FLOAT4;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.ColorNode;2;-1076.199,26.39999;Float;False;Property;_CircleColor;CircleColor;1;0;0,0.006896496,1,0;0;5;COLOR;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.ColorNode;1;-907.6005,-209.1999;Float;False;Property;_BckGrndColor;BckGrndColor;0;0;0.9264706,0.456423,0.456423,0;0;5;COLOR;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.SimpleAddOpNode;18;-491.2931,513.9475;Float;True;2;0;FLOAT4;0,0,0,0;False;1;FLOAT4;0.0;False;1;FLOAT4
Node;AmplifyShaderEditor.RangedFloatNode;14;-1171.7,570.7002;Float;False;Property;_EmmitValue;EmmitValue;3;0;0;0;1;0;1;FLOAT
Node;AmplifyShaderEditor.LerpOp;4;-482,-3.199987;Float;True;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT4;0.0;False;1;COLOR
Node;AmplifyShaderEditor.OneMinusNode;23;-189.7943,497.2483;Float;True;1;0;FLOAT4;0.0;False;1;FLOAT4
Node;AmplifyShaderEditor.ColorNode;22;-497.9958,-331.7534;Float;False;Property;_TextColor;TextColor;5;0;0,0,0,0;0;5;COLOR;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.ColorNode;11;168.9995,594.1992;Float;False;Constant;_Roughness;Roughness;3;0;0.6691177,0.6691177,0.6691177,1;0;5;COLOR;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.OneMinusNode;16;-757.6941,750.948;Float;True;1;0;FLOAT4;0.0;False;1;FLOAT4
Node;AmplifyShaderEditor.LerpOp;21;-8.395708,-195.7533;Float;True;3;0;COLOR;0.0;False;1;COLOR;0,0,0,0;False;2;FLOAT4;0.0;False;1;COLOR
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;13;-35.50018,229.4002;Float;False;2;0;FLOAT4;0.0;False;1;FLOAT;0,0,0,0;False;1;FLOAT4
Node;AmplifyShaderEditor.ColorNode;10;-1141.9,707.6;Float;False;Constant;_Metallic;Metallic;3;0;0,0,0,1;0;5;COLOR;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;629.1002,-31;Float;False;True;2;Float;ASEMaterialInspector;0;Standard;MyShaders/BallShader;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;0;False;0;0;Opaque;0.5;True;True;0;False;Opaque;Geometry;ForwardOnly;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;False;0;255;255;0;0;0;0;False;0;4;10;25;False;0.5;True;0;Zero;Zero;0;Zero;Zero;Add;Add;0;False;0;0,0,0,0;VertexOffset;False;Cylindrical;Relative;0;;-1;-1;-1;-1;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0.0;False;4;FLOAT;0.0;False;5;FLOAT;0.0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0.0;False;9;FLOAT;0.0;False;10;OBJECT;0.0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;13;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;9;0;3;0
WireConnection;18;0;9;0
WireConnection;18;1;15;0
WireConnection;4;0;1;0
WireConnection;4;1;2;0
WireConnection;4;2;9;0
WireConnection;23;0;18;0
WireConnection;16;0;15;0
WireConnection;21;0;22;0
WireConnection;21;1;4;0
WireConnection;21;2;18;0
WireConnection;13;0;23;0
WireConnection;13;1;14;0
WireConnection;0;0;21;0
WireConnection;0;2;13;0
WireConnection;0;3;10;0
WireConnection;0;4;11;0
ASEEND*/
//CHKSM=3F4C4DD53CAD0CC61D5C2E8E5DB3EF5FC3D02A00