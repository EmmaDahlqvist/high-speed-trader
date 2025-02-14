Shader "Unlit/LocalizedFogShader"
{
    Properties
    {
        _FogColor ("Fog Color", Color) = (1, 1, 1, 1)
        _FogStart ("Fog Start", Float) = 10
        _FogEnd ("Fog End", Float) = 50
        _FogCenter ("Fog Center", Vector) = (0, 0, 0)
        _FogFalloff ("Fog Falloff", Float) = 1.0
    }
    SubShader
    {
        Tags { "Queue"="Overlay" }
        Pass
        {
            Tags { "LightMode"="ForwardBase" }
            Blend SrcAlpha OneMinusSrcAlpha
            ZWrite Off
            Cull Front

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };

            struct v2f
            {
                float4 pos : POSITION;
                float3 worldPos : TEXCOORD0;
            };

            uniform float _FogStart;
            uniform float _FogEnd;
            uniform float _FogFalloff;
            uniform float3 _FogCenter;
            uniform float4 _FogColor;

            v2f vert(appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
                return o;
            }

            half4 frag(v2f i) : COLOR
            {
                // Ber�kna avst�ndet till fogens centrum
                float dist = distance(i.worldPos, _FogCenter);
                
                // Om vi vill att dimman ska vara mer intensiv vid kanterna, m�ste vi modifiera avst�ndet
                // H�r g�r vi att avst�ndet till kanterna blir viktigare
                float fadeFactor = 1.0 - saturate((dist - _FogStart) / (_FogEnd - _FogStart));
                
                // Nu anv�nder vi ett exponentiellt falloff f�r att mjukt tona ut dimman fr�n kanterna
                fadeFactor = pow(fadeFactor, _FogFalloff);
                
                // �terv�nd den interpolerade f�rgen baserat p� dimmans intensitet
                return lerp(float4(1, 1, 1, 0), _FogColor, fadeFactor);
            }
            ENDCG
        }
    }
}
