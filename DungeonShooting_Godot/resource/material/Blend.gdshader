shader_type canvas_item;

//混合颜色
uniform vec4 blend : source_color = vec4(1.0, 1.0, 1.0, 1.0);
//混合度
uniform float schedule : hint_range(0.0, 1.0) = 0.0;
//
uniform vec4 modulate : source_color = vec4(1.0, 1.0, 1.0, 1.0);

//------------------ 轮廓相关 --------------
uniform bool show_outline = true;
//轮廓颜色
uniform vec4 outline_color : source_color = vec4(0.0, 0.0, 0.0, 1.0);
//是否是彩虹轮廓
uniform bool outline_rainbow = false;
//轮廓是否使用 blend
uniform bool outline_use_blend = true;

//彩虹轮廓变化周期
const float frequency = 0.25;
const float light_offset = 0.5;

void fragment() {
	float a = COLOR.a;
	//显示轮廓
	if (show_outline && a == 0.0) {
		vec2 size = TEXTURE_PIXEL_SIZE;
		float outline;
		outline = texture(TEXTURE, UV + vec2(-size.x, 0)).a;
		outline += texture(TEXTURE, UV + vec2(0, size.y)).a;
		outline += texture(TEXTURE, UV + vec2(size.x, 0)).a;
		outline += texture(TEXTURE, UV + vec2(0, -size.y)).a;
		outline = min(outline, 1.0);
		if (outline > 0.0) {
			if (outline_rainbow){
				vec4 animated_line_color = vec4(
					light_offset + sin(2.0 * 3.14 * frequency * TIME),
					light_offset + sin(2.0 * 3.14 * frequency * TIME + radians(120.0)),
					light_offset + sin(2.0 * 3.14 * frequency * TIME + radians(240.0)),
					1.0
				);
				COLOR = mix(COLOR, animated_line_color, 1);
				a = animated_line_color.a;
			} else {
				COLOR = mix(COLOR, outline_color , 1);
				a = outline_color.a;
			}
		}
		if (outline_use_blend) {
			COLOR = mix(COLOR, blend, schedule);
		}
	} else { //非轮廓
		COLOR = mix(COLOR, blend, schedule);
	}
	
	COLOR.a *= a;
	COLOR *= modulate;
}
