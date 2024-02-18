<template>
  <q-input
    class="default-input"
    :input-class="qClass"
    :placeholder="placeholder"
    rounded
    outlined
    v-model="value"
    :dense="false"
  ></q-input>
</template>
<script lang="ts">
import { ref } from 'vue';
export default {
  name: 'EInput',
  props: {
    qClass: {
      type: String,
      required: false,
      default: '',
    },
    placeholder: {
      type: String,
      required: false,
      default: 'Tìm kiếm',
    },
  },
  setup() {
    const value = ref('');
    return {
      value,
    };
  },
};
</script>
<style src="../../css/e-input.scss" lang="scss"></style>
