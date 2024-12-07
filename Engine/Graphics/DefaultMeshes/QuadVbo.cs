using Engine.Graphics.OpenGL.Buffer;

namespace Engine.Graphics.DefaultMeshes;

public class QuadVbo : VertexBuffer
{
    public QuadVbo()
    {
        Data = GetVertexData();
        TransferData();
    }

    protected sealed override object[] GetVertexData()
    {
        object[] data;

        object[] vertices =
        [
            -1, -1, 0,
            1, -1, 0,
            1, 1, 0,
            -1, 1, 0
        ];
        int[] indices =
        [
            0, 2, 3,
            0, 1, 2
        ];
        var vertexData = ConnectVertexData(vertices, 3, indices);

        object[] normals =
        [
            0, 1, 0,
            0, -1, 0,
            1, 0, 0,
            0, 0, 1
        ];

        object[] texCoord =
        [
            -1, -1,
            1, -1,
            1, 1,
            -1, 1
        ];
        int[] texCoordIndices =
        [
            0, 2, 3,
            0, 1, 2
        ];
        var texCoordData = ConnectVertexData(texCoord, 2, texCoordIndices);

        data = CombineData(vertexData.ToArray(), 6, normals, 3);
        data = CombineData(data, 9, texCoordData.ToArray(), 6);

        indicesCount = indices.Length;
        return vertexData;
    }
}