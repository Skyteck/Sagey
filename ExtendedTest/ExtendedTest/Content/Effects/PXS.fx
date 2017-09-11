sampler BaseTexture : register(s0);
sampler MaskTexture : register(s1) {
    addressU = Clamp;
    addressV = Clamp;
};

//All of these variables are pixel values
//Feel free to replace with float2 variables
float MaskLocationX;
float MaskLocationY;
float MaskWidth;
float MaskHeight;
float BaseTextureLocationX;  //This is where your texture is to be drawn
float BaseTextureLocationY;  //texCoord is different, it is the current pixel
float BaseTextureWidth;
float BaseTextureHeight;

float4 main(float2 texCoord : TEXCOORD0) : COLOR0
{
    //We need to calculate where in terms of percentage to sample from the MaskTexture
    float maskPixelX = texCoord.x * BaseTextureWidth + BaseTextureLocationX;
    float maskPixelY = texCoord.y * BaseTextureHeight + BaseTextureLocationY;
    float2 maskCoord = float2((maskPixelX - MaskLocationX) / MaskWidth, (maskPixelY - MaskLocationY) / MaskHeight);
    float4 bitMask = tex2D(MaskTexture, maskCoord);

    float4 tex = tex2D(BaseTexture, texCoord);

    //It is a good idea to avoid conditional statements in a pixel shader if you can use math instead.
    return tex * (bitMask.a);
    //Alternate calculation to invert the mask, you could make this a parameter too if you wanted
    //return tex * (1.0 - bitMask.a);
}
technique Technique1
{
    pass Pass1
    {
        PixelShader = compile ps_5_0 main();
    }
}