#version 330 core

layout(location=0) in vec3 in_position;
//layout(location=1) in vec3 in_normal;
//layout(location=2) in vec2 in_texCoord;

//out vec3 normal;
//out vec2 texCoord;

//uniform mat4 u_viewMatrix;
//uniform mat4 u_projectionMatrix ;

void main() {
    gl_Position = vec4(in_position, 1.0);  // u_projectionMatrix * u_viewMatrix * 
//    normal = in_normal;
//    texCoord = in_texCoord;
}
