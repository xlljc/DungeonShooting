shader_type canvas_item;

uniform vec4 shadowColor : hint_color = vec4(0.0, 0.0, 0.0, 0.8);
uniform float schedule = 1.0;
//将贴图渲染为阴影

void fragment() {
	vec4 textureColor = texture(TEXTURE, UV);
	vec4 col = mix(textureColor, shadowColor, schedule);
	COLOR = mix(vec4(0.0, 0.0, 0.0, 0.0), col, textureColor.a);
}
