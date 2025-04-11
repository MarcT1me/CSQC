# QuantumCore Engine

###### Powered by C# | Ported from Python - PyQC

<img alt="main" height="128" src="Engine\Data\Assets\QuantumCore.png" width="128"/>

<p style="color:rgba(255,200,0,0.5); font-weight:bold;">⚠️ WARNING: QuantumCore Engine is currently under development!</p>

---
> > > > ### list of changes
>>>> - Change the Window class
>>>> - Switching to a mix of SDL and Opengl.Graphics
>>>> - Create a OpenGL namespace

## Overview

The engine was migrated from PyQCv1 to .NET. This will increase the speed and use modern APIs for working with graphics.

So far, very little has been implemented (only the window and shaders), but unlike Python, buffers and part of the GL
module have been made.

## How to start

1) clone [GitHub](https://github.com/MarcT1me/CSQC.git)
2) install deps
    ```bash
   dotnet restore
    ```
3) build project
    ```bash
   dotnet build
    ```
4) run project
    ```bash
   dotnet run --project TestApp/TestApp.csproj
    ```