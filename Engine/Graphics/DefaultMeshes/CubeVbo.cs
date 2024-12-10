// using Engine.Graphics.OpenGL.Buffer;
//
// namespace Engine.Graphics.DefaultMeshes;
//
// public class CubeVbo : VertexBuffer
// {
//     public CubeVbo()
//     {
//         Data = GetVertexData();
//         TransferData();
//     }
//
//     protected sealed override object[] GetVertexData()
//     {
//         object[] data;
//         
//         object[] vertices =
//         [
//             -1, -1, 1,
//             1, -1, 1,
//             1, 1, 1,
//             -1, 1, 1,
//             -1, 1, -1,
//             -1, -1, -1,
//             1, -1, -1,
//             1, 1, -1
//         ];
//         int[] indices =
//         [
//             0, 2, 3,
//             0, 1, 2,
//             1, 7, 2,
//             1, 6, 7,
//             6, 5, 4,
//             4, 7, 6,
//             3, 4, 5,
//             3, 5, 0,
//             3, 7, 4,
//             3, 2, 7,
//             0, 6, 1,
//             0, 5, 6
//         ];
//        var vertexData = ConnectVertexData(vertices, 3, indices);
//
//         object[] normals =
//         [
//             0, 0, 1,
//             1, 0, 0,
//             0, 0, -1,
//             -1, 0, 0,
//             0, 1, 0,
//             0, -1, 0
//         ];
//
//         object[] texCoord =
//         [
//             -1, -1,
//             1, -1,
//             1, 1,
//             -1, 1
//         ];
//         int[] texCoordIndices =
//         [
//             0, 2, 3,
//             0, 1, 2
//         ];
//         var texCoordData = ConnectVertexData(texCoord, 2, texCoordIndices);
//
//         data = CombineData(vertexData.ToArray(), 9, normals, 3);
//         data = CombineData(data, 12, texCoordData.ToArray(), 6);
//
//         return vertexData;
//     }
// }