Shader "Utj/OverdrawKun/Overdraw"
{
    SubShader
    {
        Tags { "RenderType" = "Opaque" }

        Blend One One
        ZClip False
		ZTest LEqual
		ZWrite On
		Cull Back
        //Fog { Color (0,0,0,0) }
		//Fog{ Mode Off }

        CGPROGRAM
		
        #pragma surface surf Lambert nofog
		

        struct Input {
            float2 uv_MainTex;
        };

        sampler2D _MainTex;

        void surf (Input IN, inout SurfaceOutput o)
        {
            half4 c = tex2D (_MainTex, IN.uv_MainTex);
            o.Albedo = half3(0, 0, 0);
            o.Alpha = c.a;
            o.Emission = half3(1.0/255.0, 0.125, 0.25);
        }

        ENDCG
    }

    Fallback "Diffuse"
}
