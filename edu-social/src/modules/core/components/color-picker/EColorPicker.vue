<template src="./e-color-picker.html"></template>

<script>
export default {
  data() {
    return {
      hue: 0,
      colorValue: '#DB8585',
      isShowSelectColor: false
    };
  },
  methods: {
    getColorAtPosition(x, _y) {
      const gradientWidth = this.$refs.colorGradient.clientWidth;
      const percentage = x / gradientWidth;

      // Sử dụng HSL để lấy màu
      const h = this.hue;
      const s = percentage < 0.5 ? 100 : 50; // Ví dụ: điều chỉnh độ bão hòa
      const l = 50; // Giữ độ sáng ở mức trung bình

      return this.hslToHex(h, s, l);
    },

    updateColorPicker(event) {
      const rect = this.$refs.colorGradient.getBoundingClientRect();

      const offsetX = event.clientX - rect.left;
      const offsetY = event.clientY - rect.top;

      // Đặt vị trí của vòng tròn màu theo tọa độ click
      this.$refs.colorCircle.style.left = `${offsetX}px`;
      this.$refs.colorCircle.style.top = `${offsetY}px`;

      // Tính toán độ bão hòa và độ sáng dựa trên vị trí click
      const gradientWidth = this.$refs.colorGradient.clientWidth;
      const gradientHeight = this.$refs.colorGradient.clientHeight;

      const saturation = (offsetX / gradientWidth) * 100; // Độ bão hòa từ 0 đến 100
      const lightness = 100 - (offsetY / gradientHeight) * 100; // Độ sáng từ 0 đến 100

      // Chuyển đổi HSL sang mã màu hex và cập nhật colorValue
      this.colorValue = this.hslToHex(this.hue, saturation, lightness);
    },

    updateHue(event) {
      const rect = this.$refs.colorHue.getBoundingClientRect();
      const offsetX = event.clientX - rect.left;
      const huePercentage = offsetX / rect.width;

      // Cập nhật giá trị hue
      this.hue = Math.floor(huePercentage * 360);
      this.$refs.hueSlider.style.left = `${offsetX}px`;

      // Cập nhật gradient màu của color picker dựa trên hue mới
      this.$refs.colorGradient.style.background = `linear-gradient(to right, hsl(${this.hue}, 100%, 50%), hsl(${this.hue}, 0%, 50%))`;

      // Cập nhật lại mã màu dựa trên hue, saturation và lightness hiện tại
      const rectGradient = this.$refs.colorGradient.getBoundingClientRect();
      const circleOffsetX = this.$refs.colorCircle.offsetLeft;
      const circleOffsetY = this.$refs.colorCircle.offsetTop;

      const gradientWidth = this.$refs.colorGradient.clientWidth;
      const gradientHeight = this.$refs.colorGradient.clientHeight;

      const saturation = (circleOffsetX / gradientWidth) * 100; // Lấy độ bão hòa hiện tại
      const lightness = 100 - (circleOffsetY / gradientHeight) * 100; // Lấy độ sáng hiện tại

      // Chuyển đổi HSL sang mã màu hex và cập nhật colorValue
      this.colorValue = this.hslToHex(this.hue, saturation, lightness);
    },

    updateInputColor() {
      // Chuyển đổi mã hex thành HSL
      const { h, s, l } = this.hexToHSL(this.colorValue);

      // Cập nhật hue dựa trên giá trị HSL đã chuyển đổi
      this.hue = h;

      // Cập nhật vị trí slider hue
      const huePosition = (this.hue / 360) * this.$refs.colorHue.clientWidth;
      this.$refs.hueSlider.style.left = `${huePosition}px`;

      // Cập nhật vị trí vòng tròn màu (colorCircle) dựa trên độ bão hòa và độ sáng
      const gradientWidth = this.$refs.colorGradient.clientWidth;
      const gradientHeight = this.$refs.colorGradient.clientHeight;

      const offsetX = (s / 100) * gradientWidth;
      const offsetY = gradientHeight - (l / 100) * gradientHeight;

      this.$refs.colorCircle.style.left = `${offsetX}px`;
      this.$refs.colorCircle.style.top = `${offsetY}px`;

      // Cập nhật màu nền của ô xem trước
      this.$refs.colorPreview.style.backgroundColor = this.colorValue;
    },

    // Hàm chuyển đổi hex sang HSL
    hexToHSL(H) {
      let r = 0, g = 0, b = 0;
      if (H.length == 4) {
        r = '0x' + H[1] + H[1];
        g = '0x' + H[2] + H[2];
        b = '0x' + H[3] + H[3];
      } else if (H.length == 7) {
        r = '0x' + H[1] + H[2];
        g = '0x' + H[3] + H[4];
        b = '0x' + H[5] + H[6];
      }
      r /= 255;
      g /= 255;
      b /= 255;
      let cmin = Math.min(r, g, b),
        cmax = Math.max(r, g, b),
        delta = cmax - cmin,
        h = 0,
        s = 0,
        l = 0;

      if (delta == 0) h = 0;
      else if (cmax == r) h = ((g - b) / delta) % 6;
      else if (cmax == g) h = (b - r) / delta + 2;
      else h = (r - g) / delta + 4;

      h = Math.round(h * 60);
      if (h < 0) h += 360;

      l = (cmax + cmin) / 2;
      s = delta == 0 ? 0 : delta / (1 - Math.abs(2 * l - 1));
      s = +(s * 100).toFixed(1);
      l = +(l * 100).toFixed(1);

      return { h, s, l };
    },


    async pickColor() {
      if (window.EyeDropper) {
        const eyeDropper = new EyeDropper();
        try {
          const result = await eyeDropper.open();
          this.colorValue = result.sRGBHex; // Sử dụng mã hex sẵn có
        } catch (error) {
          console.error('Pipette error:', error);
        }
      } else {
        alert('Trình duyệt của bạn không hỗ trợ tính năng chọn màu pipette.');
      }
    },

    hslToHex(h, s, l) {
      s /= 100;
      l /= 100;
      const a = s * Math.min(l, 1 - l);
      const f = (n) => {
        const k = (n + h / 30) % 12;
        const color = l - a * Math.max(-1, Math.min(k - 3, 9 - k, 1));
        return Math.round(255 * color).toString(16).padStart(2, '0');
      };
      return `#${f(0)}${f(8)}${f(4)}`;
    },
    onOpenSelectColor() {
      this.isShowSelectColor = true
    }
  },
};
</script>

<style src="./e-color-picker.scss" lang="scss" scoped></style>
