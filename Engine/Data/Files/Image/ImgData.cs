using OpenTK.Mathematics;
using StbImageSharp;

namespace Engine.Data.Files.Image;

public class ImgData
{
    public readonly byte[] Data;
    public Vector2i Size;

    public ImgData(Stream stream)
    {
        var imageData = ImageResult.FromStream(stream, ColorComponents.RedGreenBlueAlpha);
        Data = imageData.Data;
        Size = new Vector2i(imageData.Width, imageData.Height);
    }
}