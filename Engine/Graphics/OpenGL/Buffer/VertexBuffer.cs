namespace Engine.Graphics.OpenGL.Buffer;

public abstract class VertexBuffer : Buffer
{
    public int indicesCount = 0;

    protected object[] ConnectVertexData(object[] vertices, int size, int[] indices)
    {
        List<object> data = new();
        foreach (int ind in indices)
        {
            for (int j = 0; j < size; j++)
                data.Add(vertices[ind * size + j]);
        }

        return data.ToArray();
    }

    protected object[] CombineData(object[] data1, int len1, object[] data2, int len2)
    {
        List<object> data = new List<object>();

        int index1 = 0;
        int index2 = 0;

        while (index1 < data1.Length || index2 < data2.Length)
        {
            for (int j = 0; j < len1 && index1 < data1.Length; j++)
            {
                data.Add(data1[index1++]);
            }

            for (int j = 0; j < len2 && index2 < data2.Length; j++)
            {
                data.Add(data2[index2++]);
            }
        }

        return data.ToArray();
    }

    protected abstract object[] GetVertexData();
}