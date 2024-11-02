<template>
  <div class="mask-popup">
    <ChatComponent @close="async () => await closeChat()" isShowBack="true" />
  </div>
</template>
<script>
import ChatComponent from './ChatComponent.vue';
import { defineComponent } from 'vue';
import { useDialogPluginComponent } from 'quasar';
export default defineComponent({
  name: 'PopupChat',
  components: { ChatComponent },
  emits: [...useDialogPluginComponent.emits],
  setup(_props, _ctx) {
    const { dialogRef, onDialogHide, onDialogOK, onDialogCancel } =
      useDialogPluginComponent();
    async function closeChat() {
      onDialogHide();
    }
    function delay(ms) {
      return new Promise((resolve) => setTimeout(resolve, ms));
    }
    return {
      delay,
      dialogRef,
      onDialogHide,
      onDialogOK,
      closeChat,
      onDialogCancel,
    };
  },
});
</script>
<style lang="scss">
.mask-popup {
  position: fixed;
  top: 0;
  bottom: 0;
  left: 0;
  right: 0;
  display: flex;
  flex-direction: row;
  justify-content: center;
  background-color: #000000da;
  z-index: 100;
  align-items: center;
}
</style>
