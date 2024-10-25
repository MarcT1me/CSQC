using OpenTK.Mathematics;
using StbImageSharp;

namespace Engine.Data.Files.Image;

public class ImgData
{
    public readonly byte[] Data;
    public Vector2i Size;

    public ImgData(Stream stream)
    {
        ImageResult imageData = ImageResult.FromStream(stream, ColorComponents.RedGreenBlueAlpha);
        this.Data = imageData.Data;
        Size = new(imageData.Width, imageData.Height);
    }
}