shader_type canvas_item;

void vertex() {
	VERTEX = VERTEX * vec2(4.0);
}

void fragment() {
    vec2 pixel_size = 1.0 / vec2(textureSize(TEXTURE, 0));
    vec4 color = vec4(0.0);
    for (int x = -1; x <= 1; x++) {
        for (int y = -1; y <= 1; y++) {
			color += texture(TEXTURE, UV + vec2(float(x), float(y)) * pixel_size);
			color += texture(TEXTURE, UV + vec2(float(x), float(y)) * pixel_size / 2.0);
        }
    }
    color /= 18.0;
    COLOR = color;
}