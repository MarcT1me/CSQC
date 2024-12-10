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

    public static void SetGl(GlData glData)
    {
        foreach (EnableCap cap in GlData.EnableCaps)
        {
            GL.Enable(EnableCap.CullFace);
            GlCheckError($"Error GL.Enable {Enum.GetName(cap)}");
        }
        
        GL.DepthFunc(DepthFunction.Lequal);
        GlCheckError("Error GL.DepthFunc");
        GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);
        GlCheckError("Error GL.BlendFunc");
        
        SetViewport(glData);
        GlCheckError("Error GL.Viewport");
        
        GL.ClearColor(glData.ClearColor.X, glData.ClearColor.Y, glData.ClearColor.Z, glData.ClearColor.W);
        GlCheckError("Error GL.ClearColor");
    }

    public static void SetViewport(GlData glData)
    {
        GL.Viewport(glData.Position.X, glData.Position.Y, glData.Resolution.X, glData.Resolution.Y);
    }
    
    public static void GlCheckError(string caption = "null")
    {
        ErrorCode error = GL.GetError();
        if (error != ErrorCode.NoError)
        {
            // Handle or log the error
            Console.WriteLine($"OpenGL Error: {caption}\n\t\t| {error}");
        }
    }
}