// Our texture sampler
texture Texture;
sampler TextureSampler = sampler_state
{
	Texture = <Texture>;
};

int ImageHeight;
float Pulse;

float Contrast;
float Brightness;
float DesaturationAmount;

// This data comes from the sprite batch vertex shader
struct VertexShaderOutput
{
	float2 Position : TEXCOORD0;
	float4 Color : COLOR0;
	float2 TextureCordinate : TEXCOORD0;
};

float4 PassThrough(VertexShaderOutput input) : COLOR0
{
	return tex2D(TextureSampler, input.TextureCordinate);
}

float4 Sepia(VertexShaderOutput input) : COLOR0
{
	float4 color = tex2D(TextureSampler, input.TextureCordinate);
	
	float3x3 sepia ={0.393, 0.349, 0.272,
					0.769, 0.686, 0.534 ,
					0.189, 0.168, 0.131};

	float4 result;
	result.rgb = mul(color.rgb, sepia);
	result.a = 1.0f;

	return result;
}


float4 ScanLine(VertexShaderOutput input) : COLOR0
{
	float4 color = tex2D(TextureSampler, input.TextureCordinate);
	int a = saturate((input.Position.y * ImageHeight) % 4);
	int b = saturate((input.Position.y * ImageHeight+1) % 4);
	float m = min(a,b);

	color.rgb *= m;

	return color;
}


float4 Desaturate(VertexShaderOutput input) : COLOR0
{
	float4 color = tex2D(TextureSampler, input.TextureCordinate);

	// Desaturate
	float desaturatedColor = dot(color,float3(0.3, 0.59, 0.11));
	color.rgb = lerp(color, desaturatedColor, DesaturationAmount); // TODO Could take a pulse value
	
	return color;
}

float4 BrightnessContrast(VertexShaderOutput input) : COLOR0
{
	float4 color = tex2D(TextureSampler, input.TextureCordinate);

	 // Apply contrast.
	float factor = (1.1 * (Contrast + 1.0)) / (1.0 * (1.1 - Contrast));
	float4 outcolor = factor * (color - 0.5) + 0.5;
	outcolor += Brightness;

	outcolor.a = 1.0f;
	return saturate(outcolor);
}

float4 FadeToColor(VertexShaderOutput input) : COLOR0
{
	float4 color = tex2D(TextureSampler, input.TextureCordinate);

	float4 destColor = float4(0.0,0.0f,0.0f,1.0f); // Black

	color = lerp(color, destColor, Pulse);

	return color;
}


technique StandardTechnique
{
	pass Pass1
	{
		PixelShader = compile ps_2_0 PassThrough();
	}
}

technique DesaturateTechnique
{
	pass Pass1
	{
		PixelShader = compile ps_2_0 Desaturate();
	}
}



technique SepiaToneTechnique
{
	pass Pass1
	{
		PixelShader = compile ps_2_0 Sepia();
	}
}

technique ScanlineTechnique
{
	pass Pass1
	{
		PixelShader = compile ps_2_0 ScanLine();
	}
}

technique FadeToColorTechnique
{
	pass Pass1
	{
		PixelShader = compile ps_2_0 FadeToColor();
	}
}

technique BrightnessContrastTechnique
{
	pass Pass1
	{
		PixelShader = compile ps_2_0 BrightnessContrast();
	}
}