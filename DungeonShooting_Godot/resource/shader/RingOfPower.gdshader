shader_type canvas_item;
render_mode blend_add;

uniform float radius : hint_range(0.0, 1.0, 0.01) = .7;
uniform float thickness : hint_range(0.0, 1.0, 0.01) = .2;
uniform vec4 color : source_color = vec4(0.9, 0.4, 0.1, 1.0);
uniform float brightness : hint_range(0.0, 15.0, 0.01) = 5.0;
uniform float angular_speed : hint_range(-5.0, 5.0, 0.01) = 2.5;
uniform float radial_speed : hint_range(-5.0, 5.0, 0.01) = 1.4;
uniform float alpha : hint_range(0.0, 1.0, 0.01) = .5;
uniform sampler2D noise;

void fragment() {
	vec2 v = vec2(.5) - UV;
	float d = length(v) * 2.;
	float angle = atan(v.y, v.x) + (TIME * angular_speed);
	float thick_ratio = 1. - (abs(d - max(0., radius)) / max(.0001, thickness));
	vec2 polar = fract(vec2(angle / 6.28, d + (TIME * radial_speed)));
	vec4 col = thick_ratio * brightness * color;
	vec3 tex = texture(noise, polar).rgb;
	col.a = (alpha * (tex.r + tex.g + tex.b) * clamp(thick_ratio, 0., 1.)) / 3.;
	COLOR = col;
}