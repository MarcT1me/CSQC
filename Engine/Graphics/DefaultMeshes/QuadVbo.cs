using Engine.Graphics.OpenGL.Buffer;

namespace Engine.Graphics.DefaultMeshes;

public class QuadVbo : VertexBuffer<int>
{
    public QuadVbo()
    {
        _data = GetVertexData();
        TransferData();
    }

    protected sealed override int[] GetVertexData()
    {
        int[] data;

        data = ConnectVertexData(
            vertices: new []
            {
                -1, -1, 1,
                1, -1, 1,
                1, 1, 1,
                -1, 1, 1,
            },
            indices: new []
            {
                0, 2, 3,
                0, 1, 2
            }
        );

        return data;
    }
}