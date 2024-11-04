#version 330

uniform sampler2D texture0;
in vec2 v_texcoord;
out vec4 fragColor;

void main() {
    fragColor = texture(texture0, v_texcoord);
}