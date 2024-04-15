shader_type canvas_item;

//轮廓颜色
uniform vec4 outline_color : source_color = vec4(0.0, 0.0, 0.0, 1.0);

void fragment() {
	float a = COLOR.a;
	//显示轮廓
	if (a == 0.0) {
		vec2 size = TEXTURE_PIXEL_SIZE;
		float outline;
		outline = texture(TEXTURE, UV + vec2(-size.x, 0)).a;
		outline += texture(TEXTURE, UV + vec2(0, size.y)).a;
		outline += texture(TEXTURE, UV + vec2(size.x, 0)).a;
		outline += texture(TEXTURE, UV + vec2(0, -size.y)).a;
		outline = min(outline, 1.0);
		if (outline > 0.0) {
			COLOR = mix(COLOR, outline_color , 1);
			COLOR.a = outline_color.a;
		}
	}
}
