using Engine.Graphics.OpenGL.Buffer;

namespace Engine.Graphics.DefaultMeshes;

public class QuadVbo : VertexBuffer<int>
{
    public QuadVbo()
    {
        Data = GetVertexData();
        TransferData();
    }

    protected sealed override int[] GetVertexData()
    {
        int[] vertices =
        [
            -1, -1, 1,
            1, -1, 1,
            1, 1, 1,
            -1, 1, 1
        ];
        int[] indices =
        [
            0, 2, 3,
            0, 1, 2
        ];
        indecesCount = indices.Length;
        return ConnectVertexData(vertices, indices);
    }
}