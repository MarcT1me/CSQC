#version 330 core

/* indev */
//in vec3 normal;
//in vec2 texCoord;

uniform ivec2 u_resolution;

void main() {
    vec2 uv = gl_FragCoord.xy / u_resolution;
    gl_FragColor = vec4(vec3(uv.xy, 0.0), 1.0);
}
