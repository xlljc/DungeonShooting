shader_type canvas_item;
render_mode blend_mix;

uniform vec4 color : source_color = vec4(1.0, 1.0, 1.0, 1.0);
uniform vec2 size = vec2(1280.0, 720.0);
uniform int line_width = 1;
uniform vec2 offset = vec2(0.0, 0.0);
uniform float grid_size = 16.0;

void fragment() {
	vec2 uv = ((offset - vec2(float(line_width)) * 0.5f) / size) + UV;
	vec2 r = mod(size * uv, vec2(grid_size));
	vec2 lines = step(1.0 - float(line_width) / grid_size, r / vec2(grid_size));
	float alpha = dot(lines, vec2(1.0, 1.0));
	COLOR = color;
	COLOR.a *= alpha;
}