#version 330 core

uniform vec2 u_resolution = vec2(800, 600);

void main() {
    vec2 uv = gl_FragCoord.xy / u_resolution;
    gl_FragColor = vec4(uv.xy, 0.0, 1.0);
}
