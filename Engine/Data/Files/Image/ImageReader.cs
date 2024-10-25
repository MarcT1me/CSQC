namespace Engine.Data.Files.Image;

public class ImageReader : Reader
{
    public ImgData ImageData;

    public ImageReader(string imagePath) : base(EngineData.RootDirectory)
    {
        using (var stream = ReadStream(imagePath)) ImageData = new(stream);
    }
}