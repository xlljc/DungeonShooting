shader_type canvas_item;

uniform vec4 blend : source_color = vec4(1.0, 1.0, 1.0, 1.0);
uniform float schedule = 0.0;
uniform float alpha = 1.0;

void fragment() {
	vec4 textureColor = texture(TEXTURE, UV);
	vec4 col = mix(textureColor, blend, schedule);
	col = mix(vec4(0.0, 0.0, 0.0, 0.0), col, textureColor.a);
	col.a *= alpha;
	COLOR = col;
}
