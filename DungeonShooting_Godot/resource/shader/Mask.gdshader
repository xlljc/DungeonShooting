shader_type canvas_item;

uniform sampler2D mask_texture;

void fragment() {
	vec2 size = TEXTURE_PIXEL_SIZE;
	COLOR.a = 1.0 - texture(mask_texture, UV).a;
}

