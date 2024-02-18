<template>
  <q-editor
    v-model="qeditor"
    :readonly="isReadonly"
    :dense="$q.screen.lt.md"
    :toolbar="toolbar"
    @update:model-value="updateContent"
    class="custom-editor"
  />
</template>
<script lang="ts">
import { ref, watch } from 'vue';
import { defineComponent } from 'vue';
export default defineComponent({
  props: {
    isReadonly: {
      type: Boolean,
      required: false,
      default: true,
    },
    content: {
      type: String,
      required: false,
      default: '',
    },
  },
  emits: ['update:content'],
  created() {
    this.qeditor = this.content;
    if (this.isReadonly) {
      this.toolbar = [];
    } else {
      this.toolbar = [
        ['bold', 'italic', 'strike', 'underline'],
        ['upload', 'save'],
      ];
    }
  },
  setup(props, ctx) {
    const toolbar = ref([] as any);
    const qeditor = ref('OK');
    watch(
      () => props.isReadonly,
      (newValue, oldValue) => {
        if (newValue) {
          toolbar.value = [];
          console.log(oldValue);
        } else {
          toolbar.value = [
            ['bold', 'italic', 'strike', 'underline'],
            ['upload', 'save'],
          ];
        }
      }
    );
    function updateContent(data: string) {
      ctx.emit('update:content', data);
    }
    return {
      toolbar,
      qeditor,
      updateContent,
    };
  },
});
</script>
<style lang="scss">
.custom-editor {
}
</style>
