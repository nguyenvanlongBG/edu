<template>
  <div
    :class="
      'container-icon' + (type != undefined && type == 1 ? ' pointer' : '')
    "
  >
    <slot name="icon"> </slot>
  </div>
</template>
<script>
export default {
  name: 'EIcon',
  // type=0 Not cusor: pointer, type=1 cusor: pointer
  props: {
    // Có hiển thị con trỏ khi trỏ vào không 1: Có, 0: Không
    type: {
      type: Int32Array,
      required: true,
    },
  },
};
</script>
<style>
.container-icon {
  width: 24px;
  height: 24px;
  padding: 2px;
  box-sizing: border-box;
  display: flex;
  justify-content: center;
  align-items: center;
}

.pointer {
  cursor: pointer;
}
</style>
