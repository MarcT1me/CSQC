using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using SharpFont;

namespace Engine.Graphics.Fonts
{
    namespace FreeType
    {
        internal struct Character
        {
            // the Font symbol
            public int TextureId { get; init; }
            public Vector2 Size { get; init; }
            public Vector2 Bearing { get; init; }
            public int Advance { get; init; }
        }

        public class FreeTypeFont
        {
            // array of all characters of the selected Font
            private readonly Dictionary<uint, Character> _characters = new();
            public readonly uint FontSize;
            private readonly int _vao;
            private readonly int _vbo;

            protected FreeTypeFont(
                uint fontSize, string fontPath, Vector2i localisationRange
            )
            {
                Library lib = new Library();

                if (!File.Exists(fontPath))
                {
                    throw new FileNotFoundException("Font file not found: " + fontPath);
                }

                byte[] fontData = File.ReadAllBytes(fontPath);

                Face face = new Face(lib, fontData, 0); // Передаем массив байтов и индекс шрифта (0 для одного шрифта)

                face.SetPixelSizes(0, fontSize);
                FontSize = fontSize;

                GL.PixelStore(PixelStoreParameter.UnpackAlignment, 1);

                GL.ActiveTexture(TextureUnit.Texture0);
                
                for (uint c = 0; c < 128; c++)
                    LoadChar(face, c);
                for (int c = localisationRange.X; c < localisationRange.Y+1; c++)
                    LoadChar(face, (uint)c);

                GL.BindTexture(TextureTarget.Texture2D, 0);

                GL.PixelStore(PixelStoreParameter.UnpackAlignment, 4);

                float[] quadVbo =
                {
                    // x    y    u     v    
                    0.0f, 1.0f, 0.0f, 1.0f,
                    0.0f, 0.0f, 0.0f, 0.0f,
                    1.0f, 0.0f, 1.0f, 0.0f,
                    0.0f, 1.0f, 0.0f, 1.0f,
                    1.0f, 0.0f, 1.0f, 0.0f,
                    1.0f, 1.0f, 1.0f, 1.0f
                };

                _vbo = GL.GenBuffer();
                GL.BindBuffer(BufferTarget.ArrayBuffer, _vbo);
                GL.BufferData(BufferTarget.ArrayBuffer, 4 * 6 * 4, quadVbo, BufferUsageHint.StaticDraw);

                _vao = GL.GenVertexArray();
                GL.BindVertexArray(_vao);

                GL.EnableVertexAttribArray(0);
                GL.VertexAttribPointer(0, 2, VertexAttribPointerType.Float, false, 4 * 4, 0);
                GL.EnableVertexAttribArray(1);
                GL.VertexAttribPointer(1, 2, VertexAttribPointerType.Float, false, 4 * 4, 2 * 4);

                GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
                GL.BindVertexArray(0);
            }

            void LoadChar(Face face, uint c)
            {
                try
                {
                    face.LoadChar(c, LoadFlags.Render, LoadTarget.Normal);
                    GlyphSlot glyph = face.Glyph;
                    FTBitmap bitmap = glyph.Bitmap;

                    int texObj = GL.GenTexture();
                    GL.BindTexture(TextureTarget.Texture2D, texObj);
                    GL.TexImage2D(TextureTarget.Texture2D, 0,
                        PixelInternalFormat.R8, bitmap.Width, bitmap.Rows, 0,
                        PixelFormat.Red, PixelType.UnsignedByte, bitmap.Buffer);

                    GL.TextureParameter(texObj, TextureParameterName.TextureMinFilter,
                        (int)TextureMinFilter.Linear);
                    GL.TextureParameter(texObj, TextureParameterName.TextureMagFilter,
                        (int)TextureMagFilter.Linear);
                    GL.TextureParameter(texObj, TextureParameterName.TextureWrapS,
                        (int)TextureWrapMode.ClampToEdge);
                    GL.TextureParameter(texObj, TextureParameterName.TextureWrapT,
                        (int)TextureWrapMode.ClampToEdge);

                    // add character
                    Character ch = new Character
                    {
                        TextureId = texObj,
                        Size = new Vector2(bitmap.Width, bitmap.Rows),
                        Bearing = new Vector2(glyph.BitmapLeft, glyph.BitmapTop),
                        Advance = glyph.Advance.X.Value
                    };
                    _characters.Add(c, ch);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }

            protected void RenderText(string text, Vector2 pos, Vector2 dir, bool renderBox = false)
            {
                GL.ActiveTexture(TextureUnit.Texture0);
                GL.BindVertexArray(_vao);

                float angleRad = (float)Math.Atan2(-dir.Y, dir.X);
                Matrix4 rotateM = Matrix4.CreateRotationZ(angleRad);
                Matrix4 transOriginM = Matrix4.CreateTranslation(new Vector3(pos.X, pos.Y, 0f));

                // Iterate through all characters
                float charX = 0.0f;
                foreach (var c in text)
                {
                    Character ch = _characters.TryGetValue(c, out var character) ? character : _characters['\r'];


                    float xRel = charX + ch.Bearing.X;
                    float yRel = ch.Size.Y - ch.Bearing.Y;

                    // Advance
                    charX += ch.Advance >> 6;

                    // Calculate main scale factor
                    Matrix4 scaleM = Matrix4.CreateScale(new Vector3(ch.Size.X, ch.Size.Y, 1.0f));
                    // I bring the size to one standard symbol
                    Matrix4 fieldOffsetM = Matrix4.CreateTranslation(new Vector3(0)
                        {
                            Y = FontSize - ch.Size.Y - (c
                                is '_' or '-' or '|' or '@'
                                or '{' or '}' or '[' or ']' or '(' or ')'
                                ? 5
                                : 0)
                        }
                    );
                    // offset during rotation
                    Matrix4 rotationOffsetM = Matrix4.CreateTranslation(new Vector3(xRel, yRel, 0.0f));

                    // model uniform matrix 
                    Matrix4 modelM = scaleM * fieldOffsetM * // scale
                                     rotationOffsetM * rotateM * // rotate
                                     transOriginM; // translate
                    GL.UniformMatrix4(0, false, ref modelM);

                    GL.BindTexture(TextureTarget.Texture2D, ch.TextureId);

                    // Render glyph on the screen
                    GL.Uniform1(10, 0);
                    GL.DrawArrays(PrimitiveType.Triangles, 0, 6);

                    // Render bounding box
                    if (renderBox)
                    {
                        GL.Uniform1(10, 1);
                        GL.DrawArrays(PrimitiveType.Lines, 0, 6);
                    }
                }

                GL.BindVertexArray(0);
                GL.BindTexture(TextureTarget.Texture2D, 0);
            }

            protected void Dispose()
            {
                foreach (var ch in _characters.Values)
                {
                    GL.DeleteTexture(ch.TextureId);
                }

                GL.DeleteBuffer(_vbo);
                GL.DeleteVertexArray(_vao);
            }
        }
    };
}