// Copyright 2017 Google Inc.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

Shader "Brush/DiffuseInstanced" {
	Properties{
	  _Color("Main Color", Color) = (1,1,1,1)
	  _MainTex("Base (RGB) Trans (A)", 2D) = "white" {}
	}

		SubShader{
Tags { "RenderType" = "Opaque" }
		  LOD 200
		  Cull Off

		CGPROGRAM
		#pragma surface surf Lambert
		#pragma multi_compile __ TBT_LINEAR_TARGET
		#include "../../../Shaders/Include/Brush.cginc"
		#pragma target 3.0

		sampler2D _MainTex;
		UNITY_INSTANCING_BUFFER_START(Props)
			UNITY_DEFINE_INSTANCED_PROP(fixed4, _Color)
		UNITY_INSTANCING_BUFFER_END(Props)

		struct Input {
		  float2 uv_MainTex;
		  float4 color : COLOR;
		  //fixed vface : VFACE;
		};

		//void vert(inout appdata_full v) {
		//  v.color = TbVertToNative(v.color);
		//}

		void surf(Input IN, inout SurfaceOutput o) {
		  o.Albedo = IN.color * UNITY_ACCESS_INSTANCED_PROP(Props, _Color);
		  o.Alpha = 1.0;
		}
		ENDCG
	}
		Fallback "Diffuse"
}
