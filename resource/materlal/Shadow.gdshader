shader_type canvas_item;

uniform vec4 shadowColor : hint_color = vec4(0.0, 0.0, 0.0, 0.8);

//将贴图渲染为阴影

void fragment() {
	float a = texture(TEXTURE, UV).a;
	
	COLOR = mix(vec4(0.0, 0.0, 0.0, 0.0), shadowColor, a);
}
