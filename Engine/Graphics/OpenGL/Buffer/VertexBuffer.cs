namespace Engine.Graphics.OpenGL.Buffer;


public abstract class VertexBuffer<T> : Buffer<T> where T : struct
{
    public int indecesCount = 0;
    
    protected T[] ConnectVertexData(T[] vertices, int[] indices)
    {
        List<T> data = new();
        foreach (int ind in indices)
        {
            data.Add(vertices[ind * 3]);
            data.Add(vertices[ind * 3 + 1]);
            data.Add(vertices[ind * 3 + 2]);
        }

        return data.ToArray();
    }

    protected T[] CombineData(T[] data1, int len1, T[] data2, int len2)
    {
        List<T> data = new();

        for (int i = 0; i < data1.Length / 9; i++)
        {
            // data1
            for (int j = 0; j < len1; j++)
                data.Add(data1[i * len1 + j]);

            // data2
            for (int j = 0; j < len2; j++)
                data.Add(data2[i * len2 + j]);
        }

        return data.ToArray();
    }

    protected abstract T[] GetVertexData();
}
