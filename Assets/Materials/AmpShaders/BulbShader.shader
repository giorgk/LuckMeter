// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "MyShaders/BulbShader"
{
	Properties
	{
		[HideInInspector] __dirty( "", Int ) = 1
		_Emit_color("Emit_color", Color) = (1,0,0,0)
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" "IsEmissive" = "true"  }
		Cull Back
		CGPROGRAM
		#pragma target 3.0
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows 
		struct Input
		{
			fixed filler;
		};

		uniform float4 _Emit_color;

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			o.Albedo = _Emit_color.rgb;
			float3 temp_cast_1 = (0.4).xxx;
			o.Emission = temp_cast_1;
			o.Metallic = 1.0;
			o.Smoothness = 0.0;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=10001
-1347;29;1298;824;833.0639;386.414;1;True;True
Node;AmplifyShaderEditor.RangedFloatNode;32;-332.0983,120.1538;Float;False;Constant;_Float1;Float 1;1;0;1;0;0;0;1;FLOAT
Node;AmplifyShaderEditor.ColorNode;52;-681.264,-240.5137;Float;False;Property;_Emit_color;Emit_color;0;0;1,0,0,0;0;5;COLOR;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.RangedFloatNode;12;-375.3008,38.80093;Float;False;Constant;_Float4;Float 4;1;0;0.4;0;0;0;1;FLOAT
Node;AmplifyShaderEditor.ConditionalIfNode;49;-1817.025,-342.9404;Float;False;False;5;0;FLOAT;0.0;False;1;FLOAT;0.0;False;2;FLOAT;0.0;False;3;FLOAT;0.0;False;4;FLOAT;0.0;False;1;FLOAT
Node;AmplifyShaderEditor.RangedFloatNode;3;-342.3,214.9999;Float;False;Constant;_Float0;Float 0;1;0;0;0;0;0;1;FLOAT
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;0,0;Float;False;True;2;Float;ASEMaterialInspector;0;Standard;MyShaders/BulbShader;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;0;False;0;0;Opaque;0.5;True;True;0;False;Opaque;Geometry;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;False;0;255;255;0;0;0;0;False;0;4;10;25;False;0.5;True;0;Zero;Zero;0;Zero;Zero;Add;Add;0;False;0;0,0,0,0;VertexOffset;False;Cylindrical;Relative;0;;-1;-1;-1;-1;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0.0;False;4;FLOAT;0.0;False;5;FLOAT;0.0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0.0;False;9;FLOAT;0.0;False;10;OBJECT;0.0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;13;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;0;0;52;0
WireConnection;0;2;12;0
WireConnection;0;3;32;0
WireConnection;0;4;3;0
ASEEND*/
//CHKSM=76F5C8C16F097B9C30C203C7F557F6BD9D2FBC80