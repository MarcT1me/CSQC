using Engine.Graphics.OpenGL.Buffer;

namespace Engine.Graphics.DefaultMeshes;

public class CubeVbo : VertexBuffer<int>
{
    public CubeVbo()
    {
        Data = GetVertexData();
        TransferData();
    }

    protected sealed override int[] GetVertexData()
    {
        int[] data;

        int[] vertices =
        {
            -1, -1, 1,
            1, -1, 1,
            1, 1, 1,
            -1, 1, 1,
            -1, 1, -1,
            -1, -1, -1,
            1, -1, -1,
            1, 1, -1
        };
        int[] indices =
        {
            0, 2, 3,
            0, 1, 2,
            1, 7, 2,
            1, 6, 7,
            6, 5, 4,
            4, 7, 6,
            3, 4, 5,
            3, 5, 0,
            3, 7, 4,
            3, 2, 7,
            0, 6, 1,
            0, 5, 6
        };
        data = ConnectVertexData(vertices, indices);

        int[] texCoord =
        {
            0, 0, 0,
            1, 0, 0,
            1, 1, 0,
            0, 1, 0
        };
        int[] texCoordIndices =
        {
            0, 2, 3,
            0, 1, 2,
            0, 2, 3,
            0, 1, 2,
            0, 1, 2,
            2, 3, 0,
            2, 3, 0,
            2, 0, 1,
            0, 2, 3,
            0, 1, 2,
            3, 1, 2,
            3, 0, 1
        };
        int[] texCoordData = ConnectVertexData(texCoord, texCoordIndices);

        int[] normals =
        {
            0, 0, 1,
            1, 0, 0,
            0, 0, -1,
            -1, 0, 0,
            0, 1, 0,
            0, -1, 0
        };

        data = CombineData(normals, 3, data, 9);
        data = CombineData(texCoordData, 9, data, 12);

        return data.ToArray();
    }
}