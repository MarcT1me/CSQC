using SDL2;
using OpenTK.Graphics.OpenGL;

namespace Engine.Graphics.OpenGL;

public static class OpenGl
{
    public static void Initialise()
    {
        SDL.SDL_GL_SetAttribute(
            SDL.SDL_GLattr.SDL_GL_CONTEXT_MAJOR_VERSION, GlData.ApiVersions.X
        );
        SDL.SDL_GL_SetAttribute(
            SDL.SDL_GLattr.SDL_GL_CONTEXT_MINOR_VERSION, GlData.ApiVersions.Y
        );
        SDL.SDL_GL_SetAttribute(
            SDL.SDL_GLattr.SDL_GL_CONTEXT_PROFILE_MASK, (int)SDL.SDL_GLprofile.SDL_GL_CONTEXT_PROFILE_CORE
        );
        SDL.SDL_GL_SetAttribute(
            SDL.SDL_GLattr.SDL_GL_CONTEXT_FLAGS, (int)SDL.SDL_GLcontext.SDL_GL_CONTEXT_DEBUG_FLAG
        );
        SDL.SDL_GL_SetAttribute(
            SDL.SDL_GLattr.SDL_GL_MULTISAMPLEBUFFERS, 1
        );
        SDL.SDL_GL_SetAttribute(
            SDL.SDL_GLattr.SDL_GL_MULTISAMPLESAMPLES, GlData.NumberOfSamples
        );
        SDL.SDL_GL_SetAttribute(
            SDL.SDL_GLattr.SDL_GL_DEPTH_SIZE, GlData.DepthBits
        );
    }

    public static void SetGl()
    {
        GL.Enable(EnableCap.DepthTest | EnableCap.Blend | EnableCap.CullFace);
        GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);
        GL.DepthFunc(DepthFunction.Lequal);
        SetViewport();
        GL.ClearColor(0.08f, 0.16f, 0.18f, 1.0f);
    }

    public static void SetViewport()
    {
        GL.Viewport(GlData.Position.X, GlData.Position.Y, GlData.Resolution.X, GlData.Resolution.Y);
    }
}