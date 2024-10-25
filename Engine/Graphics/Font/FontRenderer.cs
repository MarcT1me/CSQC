using Engine.Data.Files.Localisation;
using Engine.Graphics.Shaders;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

// engine

#pragma warning disable CS8500

namespace Engine.Graphics.Font;

public class FontRenderer
{
    private readonly ShaderProgram _rendererShader;
    private Matrix4 _screenProjection;

    public FontRenderer(ShaderProgram shaderProgram, Vector2i screenSize)
    {
        _rendererShader = shaderProgram;
        OnResize(screenSize.X, screenSize.Y);
    }

    public void OnResize(int x, int y)
    {
        _screenProjection = Matrix4.CreateOrthographicOffCenter(0.0f, x, y, 0.0f, -1.0f, 1.0f);
    }

    public void Begin()
    {
        _rendererShader.Use(true);
        // screen projection
        _rendererShader.SetUniform(GL.UniformMatrix4, "projection", false, ref _screenProjection);
    }

    public void DrawPhrase(string name, LangData localisation, object[] parameters)
    {
        // ReSharper disable once NotAccessedVariable
        var data = Objects.Font.Font.List[name];

        var text = string.Format(localisation.phrase[name], parameters);

        data.Text = text;
        Objects.Font.Font.List[name] = data;

        _rendererShader.SetUniform(GL.Uniform4, "textColor", Objects.Font.Font.List[name].Color);
        Objects.Font.Font.Fonts[data.Font].Render(name);
    }

    public void Draw(string name, string text)
    {
        // ReSharper disable once NotAccessedVariable
        var data = Objects.Font.Font.List[name];

        data.Text = text;
        Objects.Font.Font.List[name] = data;

        _rendererShader.SetUniform(GL.Uniform4, "textColor", Objects.Font.Font.List[name].Color);
        Objects.Font.Font.Fonts[data.Font].Render(name);
    }

    public void Draw(string name)
    {
        var data = Objects.Font.Font.List[name];

        _rendererShader.SetUniform(GL.Uniform4, "textColor", Objects.Font.Font.List[name].Color);
        Objects.Font.Font.Fonts[data.Font].Render(name);
        _rendererShader.Use(true);
    }

    public void Finish()
    {
        _rendererShader.Use(false);
    }

    public void Dispose()
    {
        _rendererShader.Dispose();
    }
}