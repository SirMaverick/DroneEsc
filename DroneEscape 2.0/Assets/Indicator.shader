// Shader created with Shader Forge v1.38 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:3,bdst:7,dpts:2,wrdp:False,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:False,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:3138,x:32853,y:32696,varname:node_3138,prsc:2|emission-1102-OUT,alpha-9050-A;n:type:ShaderForge.SFN_Color,id:7241,x:32260,y:32414,ptovrint:False,ptlb:Color,ptin:_Color,varname:node_7241,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.07843138,c2:0.3921569,c3:0.7843137,c4:1;n:type:ShaderForge.SFN_Tex2d,id:9050,x:32352,y:32913,ptovrint:False,ptlb:node_9050,ptin:_node_9050,varname:node_9050,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:8d20c846c240a0c46b80fa859196c5f5,ntxv:0,isnm:False|UVIN-4104-OUT;n:type:ShaderForge.SFN_Multiply,id:1102,x:32594,y:32799,varname:node_1102,prsc:2|A-7295-OUT,B-9050-RGB;n:type:ShaderForge.SFN_Add,id:4104,x:32080,y:32800,varname:node_4104,prsc:2|A-6586-UVOUT,B-8444-OUT;n:type:ShaderForge.SFN_TexCoord,id:6586,x:31966,y:32660,varname:node_6586,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Multiply,id:8444,x:31894,y:32898,varname:node_8444,prsc:2|A-3114-OUT,B-4001-T;n:type:ShaderForge.SFN_Append,id:3114,x:31710,y:32773,varname:node_3114,prsc:2|A-6844-OUT,B-1175-OUT;n:type:ShaderForge.SFN_ValueProperty,id:6844,x:31522,y:32668,ptovrint:False,ptlb:U_speed,ptin:_U_speed,varname:node_6844,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;n:type:ShaderForge.SFN_ValueProperty,id:1175,x:31498,y:32884,ptovrint:False,ptlb:V_speed,ptin:_V_speed,varname:_U_speed_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0;n:type:ShaderForge.SFN_Time,id:4001,x:31656,y:33001,varname:node_4001,prsc:2;n:type:ShaderForge.SFN_Multiply,id:21,x:32421,y:32378,varname:node_21,prsc:2|A-7241-RGB,B-9497-OUT;n:type:ShaderForge.SFN_Multiply,id:8165,x:32306,y:32628,varname:node_8165,prsc:2|A-8259-OUT,B-966-TTR;n:type:ShaderForge.SFN_Time,id:966,x:32123,y:32639,varname:node_966,prsc:2;n:type:ShaderForge.SFN_ValueProperty,id:8259,x:32123,y:32568,ptovrint:False,ptlb:TimeSpeed,ptin:_TimeSpeed,varname:node_8259,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;n:type:ShaderForge.SFN_Sin,id:9497,x:32446,y:32572,varname:node_9497,prsc:2|IN-8165-OUT;n:type:ShaderForge.SFN_Add,id:7295,x:32669,y:32474,varname:node_7295,prsc:2|A-7241-RGB,B-4837-OUT;n:type:ShaderForge.SFN_ConstantClamp,id:4837,x:32554,y:32583,varname:node_4837,prsc:2,min:0,max:0.2|IN-9497-OUT;proporder:7241-9050-6844-1175-8259;pass:END;sub:END;*/

Shader "Shader Forge/Indicator" {
    Properties {
        _Color ("Color", Color) = (0.07843138,0.3921569,0.7843137,1)
        _node_9050 ("node_9050", 2D) = "white" {}
        _U_speed ("U_speed", Float ) = 1
        _V_speed ("V_speed", Float ) = 0
        _TimeSpeed ("TimeSpeed", Float ) = 1
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
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform float4 _Color;
            uniform sampler2D _node_9050; uniform float4 _node_9050_ST;
            uniform float _U_speed;
            uniform float _V_speed;
            uniform float _TimeSpeed;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.pos = UnityObjectToClipPos( v.vertex );
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
////// Lighting:
////// Emissive:
                float4 node_966 = _Time;
                float node_9497 = sin((_TimeSpeed*node_966.a));
                float4 node_4001 = _Time;
                float2 node_4104 = (i.uv0+(float2(_U_speed,_V_speed)*node_4001.g));
                float4 _node_9050_var = tex2D(_node_9050,TRANSFORM_TEX(node_4104, _node_9050));
                float3 emissive = ((_Color.rgb+clamp(node_9497,0,0.2))*_node_9050_var.rgb);
                float3 finalColor = emissive;
                return fixed4(finalColor,_node_9050_var.a);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
