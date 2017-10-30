// Shader created with Shader Forge v1.38 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:True,hqlp:False,rprd:True,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:2,bsrc:3,bdst:7,dpts:2,wrdp:False,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:2865,x:32806,y:32695,varname:node_2865,prsc:2|diff-6343-OUT,spec-358-OUT,gloss-1813-OUT,normal-5964-RGB,emission-8756-OUT,alpha-6433-A;n:type:ShaderForge.SFN_Multiply,id:6343,x:32196,y:32302,varname:node_6343,prsc:2|A-7736-RGB,B-6665-RGB;n:type:ShaderForge.SFN_Color,id:6665,x:31823,y:32470,ptovrint:False,ptlb:Color,ptin:_Color,varname:_Color,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.5019608,c2:0.5019608,c3:0.5019608,c4:1;n:type:ShaderForge.SFN_Tex2d,id:7736,x:31823,y:32285,ptovrint:True,ptlb:Base Color,ptin:_MainTex,varname:_MainTex,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Tex2d,id:5964,x:32333,y:32137,ptovrint:True,ptlb:Normal Map,ptin:_BumpMap,varname:_BumpMap,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:3,isnm:True;n:type:ShaderForge.SFN_Slider,id:358,x:31953,y:32193,ptovrint:False,ptlb:Metallic,ptin:_Metallic,varname:node_358,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:1;n:type:ShaderForge.SFN_Slider,id:1813,x:31923,y:32103,ptovrint:False,ptlb:Gloss,ptin:_Gloss,varname:_Metallic_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:1;n:type:ShaderForge.SFN_Multiply,id:8756,x:32505,y:32842,varname:node_8756,prsc:2|A-3280-OUT,B-6433-RGB,C-4744-OUT;n:type:ShaderForge.SFN_Add,id:3280,x:32315,y:32831,varname:node_3280,prsc:2|A-718-OUT,B-9180-OUT;n:type:ShaderForge.SFN_Multiply,id:718,x:32107,y:32722,varname:node_718,prsc:2|A-1763-RGB,B-6433-RGB,C-4198-OUT;n:type:ShaderForge.SFN_Color,id:1763,x:31859,y:32652,ptovrint:False,ptlb:Screenlines,ptin:_Screenlines,varname:node_1763,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0,c2:0.6,c3:0.7,c4:1;n:type:ShaderForge.SFN_Tex2d,id:6433,x:31900,y:32861,ptovrint:False,ptlb:Lines,ptin:_Lines,varname:node_6433,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:278120791c685ae4bbc1a795b0117ebf,ntxv:0,isnm:False|UVIN-9411-UVOUT;n:type:ShaderForge.SFN_ValueProperty,id:4198,x:31740,y:32812,ptovrint:False,ptlb:LineBrightness,ptin:_LineBrightness,varname:node_4198,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:3;n:type:ShaderForge.SFN_Panner,id:9411,x:31574,y:32685,varname:node_9411,prsc:2,spu:1,spv:1|UVIN-586-UVOUT,DIST-3516-OUT;n:type:ShaderForge.SFN_ScreenPos,id:8617,x:30925,y:32672,varname:node_8617,prsc:2,sctp:2;n:type:ShaderForge.SFN_Multiply,id:3516,x:31481,y:32951,varname:node_3516,prsc:2|A-60-OUT,B-9775-T;n:type:ShaderForge.SFN_ValueProperty,id:60,x:31250,y:33070,ptovrint:False,ptlb:LineSpeD,ptin:_LineSpeD,varname:node_60,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0.05;n:type:ShaderForge.SFN_Time,id:9775,x:31195,y:33157,varname:node_9775,prsc:2;n:type:ShaderForge.SFN_Multiply,id:9180,x:32254,y:33088,varname:node_9180,prsc:2|A-3161-RGB,B-7265-RGB,C-9941-OUT;n:type:ShaderForge.SFN_Color,id:3161,x:32049,y:33025,ptovrint:False,ptlb:Scanlines,ptin:_Scanlines,varname:_node_1763_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0,c2:0.4,c3:0.5,c4:1;n:type:ShaderForge.SFN_Tex2d,id:7265,x:31957,y:33227,ptovrint:False,ptlb:Scanline,ptin:_Scanline,varname:node_7265,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:0fe94fa5a987b9f4a874adc26f07141d,ntxv:0,isnm:False|UVIN-3936-UVOUT;n:type:ShaderForge.SFN_ValueProperty,id:9941,x:32155,y:33330,ptovrint:False,ptlb:scanline Bright,ptin:_scanlineBright,varname:node_9941,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:20;n:type:ShaderForge.SFN_Panner,id:3936,x:31743,y:33126,varname:node_3936,prsc:2,spu:1,spv:1|UVIN-586-UVOUT,DIST-5572-OUT;n:type:ShaderForge.SFN_Multiply,id:5572,x:31558,y:33246,varname:node_5572,prsc:2|A-9920-OUT,B-9775-T;n:type:ShaderForge.SFN_ValueProperty,id:9920,x:31410,y:33143,ptovrint:False,ptlb:ScanSped,ptin:_ScanSped,varname:_LineSpec_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;n:type:ShaderForge.SFN_Fresnel,id:4744,x:32504,y:33246,varname:node_4744,prsc:2|EXP-7393-OUT;n:type:ShaderForge.SFN_ValueProperty,id:7393,x:32330,y:33330,ptovrint:False,ptlb:Fresnel,ptin:_Fresnel,varname:node_7393,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:3;n:type:ShaderForge.SFN_TexCoord,id:586,x:31117,y:32771,varname:node_586,prsc:2,uv:0,uaff:False;proporder:5964-6665-7736-358-1813-1763-6433-4198-60-3161-7265-9941-9920-7393;pass:END;sub:END;*/

Shader "Shader Forge/Scanner" {
    Properties {
        _BumpMap ("Normal Map", 2D) = "bump" {}
        _Color ("Color", Color) = (0.5019608,0.5019608,0.5019608,1)
        _MainTex ("Base Color", 2D) = "white" {}
        _Metallic ("Metallic", Range(0, 1)) = 0
        _Gloss ("Gloss", Range(0, 1)) = 0
        _Screenlines ("Screenlines", Color) = (0,0.6,0.7,1)
        _Lines ("Lines", 2D) = "white" {}
        _LineBrightness ("LineBrightness", Float ) = 3
        _LineSpeD ("LineSpeD", Float ) = 0.05
        _Scanlines ("Scanlines", Color) = (0,0.4,0.5,1)
        _Scanline ("Scanline", 2D) = "white" {}
        _scanlineBright ("scanline Bright", Float ) = 20
        _ScanSped ("ScanSped", Float ) = 1
        _Fresnel ("Fresnel", Float ) = 3
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent"
            "RenderType"="Transparent"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            Blend SrcAlpha OneMinusSrcAlpha
            Cull Off
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #define _GLOSSYENV 1
            #include "UnityCG.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #pragma multi_compile_fwdbase
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform float4 _Color;
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
            uniform sampler2D _BumpMap; uniform float4 _BumpMap_ST;
            uniform float _Metallic;
            uniform float _Gloss;
            uniform float4 _Screenlines;
            uniform sampler2D _Lines; uniform float4 _Lines_ST;
            uniform float _LineBrightness;
            uniform float _LineSpeD;
            uniform float4 _Scanlines;
            uniform sampler2D _Scanline; uniform float4 _Scanline_ST;
            uniform float _scanlineBright;
            uniform float _ScanSped;
            uniform float _Fresnel;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                float3 tangentDir : TEXCOORD3;
                float3 bitangentDir : TEXCOORD4;
                UNITY_FOG_COORDS(5)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.tangentDir = normalize( mul( unity_ObjectToWorld, float4( v.tangent.xyz, 0.0 ) ).xyz );
                o.bitangentDir = normalize(cross(o.normalDir, o.tangentDir) * v.tangent.w);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = UnityObjectToClipPos( v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                return o;
            }
            float4 frag(VertexOutput i, float facing : VFACE) : COLOR {
                float isFrontFace = ( facing >= 0 ? 1 : 0 );
                float faceSign = ( facing >= 0 ? 1 : -1 );
                i.normalDir = normalize(i.normalDir);
                i.normalDir *= faceSign;
                float3x3 tangentTransform = float3x3( i.tangentDir, i.bitangentDir, i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 _BumpMap_var = UnpackNormal(tex2D(_BumpMap,TRANSFORM_TEX(i.uv0, _BumpMap)));
                float3 normalLocal = _BumpMap_var.rgb;
                float3 normalDirection = normalize(mul( normalLocal, tangentTransform )); // Perturbed normals
////// Lighting:
////// Emissive:
                float4 node_9775 = _Time;
                float2 node_9411 = (i.uv0+(_LineSpeD*node_9775.g)*float2(1,1));
                float4 _Lines_var = tex2D(_Lines,TRANSFORM_TEX(node_9411, _Lines));
                float2 node_3936 = (i.uv0+(_ScanSped*node_9775.g)*float2(1,1));
                float4 _Scanline_var = tex2D(_Scanline,TRANSFORM_TEX(node_3936, _Scanline));
                float3 emissive = (((_Screenlines.rgb*_Lines_var.rgb*_LineBrightness)+(_Scanlines.rgb*_Scanline_var.rgb*_scanlineBright))*_Lines_var.rgb*pow(1.0-max(0,dot(normalDirection, viewDirection)),_Fresnel));
                float3 finalColor = emissive;
                fixed4 finalRGBA = fixed4(finalColor,_Lines_var.a);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
        Pass {
            Name "ShadowCaster"
            Tags {
                "LightMode"="ShadowCaster"
            }
            Offset 1, 1
            Cull Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_SHADOWCASTER
            #define _GLOSSYENV 1
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            struct VertexInput {
                float4 vertex : POSITION;
            };
            struct VertexOutput {
                V2F_SHADOW_CASTER;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.pos = UnityObjectToClipPos( v.vertex );
                TRANSFER_SHADOW_CASTER(o)
                return o;
            }
            float4 frag(VertexOutput i, float facing : VFACE) : COLOR {
                float isFrontFace = ( facing >= 0 ? 1 : 0 );
                float faceSign = ( facing >= 0 ? 1 : -1 );
                SHADOW_CASTER_FRAGMENT(i)
            }
            ENDCG
        }
        Pass {
            Name "Meta"
            Tags {
                "LightMode"="Meta"
            }
            Cull Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_META 1
            #define _GLOSSYENV 1
            #include "UnityCG.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #include "UnityMetaPass.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform float4 _Color;
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
            uniform float _Metallic;
            uniform float _Gloss;
            uniform float4 _Screenlines;
            uniform sampler2D _Lines; uniform float4 _Lines_ST;
            uniform float _LineBrightness;
            uniform float _LineSpeD;
            uniform float4 _Scanlines;
            uniform sampler2D _Scanline; uniform float4 _Scanline_ST;
            uniform float _scanlineBright;
            uniform float _ScanSped;
            uniform float _Fresnel;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
                float2 texcoord1 : TEXCOORD1;
                float2 texcoord2 : TEXCOORD2;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = UnityMetaVertexPosition(v.vertex, v.texcoord1.xy, v.texcoord2.xy, unity_LightmapST, unity_DynamicLightmapST );
                return o;
            }
            float4 frag(VertexOutput i, float facing : VFACE) : SV_Target {
                float isFrontFace = ( facing >= 0 ? 1 : 0 );
                float faceSign = ( facing >= 0 ? 1 : -1 );
                i.normalDir = normalize(i.normalDir);
                i.normalDir *= faceSign;
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                UnityMetaInput o;
                UNITY_INITIALIZE_OUTPUT( UnityMetaInput, o );
                
                float4 node_9775 = _Time;
                float2 node_9411 = (i.uv0+(_LineSpeD*node_9775.g)*float2(1,1));
                float4 _Lines_var = tex2D(_Lines,TRANSFORM_TEX(node_9411, _Lines));
                float2 node_3936 = (i.uv0+(_ScanSped*node_9775.g)*float2(1,1));
                float4 _Scanline_var = tex2D(_Scanline,TRANSFORM_TEX(node_3936, _Scanline));
                o.Emission = (((_Screenlines.rgb*_Lines_var.rgb*_LineBrightness)+(_Scanlines.rgb*_Scanline_var.rgb*_scanlineBright))*_Lines_var.rgb*pow(1.0-max(0,dot(normalDirection, viewDirection)),_Fresnel));
                
                float3 diffColor = float3(0,0,0);
                o.Albedo = diffColor;
                
                return UnityMetaFragment( o );
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
