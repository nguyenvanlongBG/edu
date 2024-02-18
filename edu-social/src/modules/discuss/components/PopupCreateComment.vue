<template>
  <q-dialog ref="dialogRef" @hide="onDialogHide">
    <div class="container-create-post">
      <div class="body">
        <div>
          <div class="header">Câu hỏi</div>
          <EditorComponent :content="post.content" :readonly="true" />
        </div>
        <div>
          <div class="header">Câu trả lời của bạn</div>
          <EditorComponent :readonly="false" />
        </div>
      </div>
      <div class="footer">
        <EButton :label="$t('button.save')" @click="saveComment" />
        <EButton :label="$t('button.cancel')" @click="cancel" />
      </div>
    </div>
  </q-dialog>
</template>
<script lang="ts">
import { defineComponent } from 'vue';
import { useDialogPluginComponent } from 'quasar';
import EditorComponent from 'src/modules/core/components/rich-editor/EditorComponent.vue';
import EButton from 'src/modules/core/components/button/EButton.vue';
import { Comment } from '../models/comment';
export default defineComponent({
  name: 'PopupCreateComment',
  props: ['post'],
  emits: [...useDialogPluginComponent.emits],
  setup() {
    const { dialogRef, onDialogHide, onDialogOK, onDialogCancel } =
      useDialogPluginComponent();
    function cancel() {
      onDialogHide();
    }
    function saveComment() {
      const comment: Comment = {
        id: '1',
        content: '2',
      };
      onDialogOK(comment);
    }
    return {
      cancel,
      saveComment,
      dialogRef,
      onDialogHide,
      onDialogOK,
      onDialogCancel,
    };
  },
  components: { EditorComponent, EButton },
});
</script>
<style scoped lang="scss">
.container-create-post {
  min-width: 800px;
  .header {
    display: flex;
    justify-content: start;
    font-size: 24px;
    font-weight: 600;
    color: $primary-orange-color;
    padding-top: 8px;
    padding-bottom: 8px;
    padding-left: 8px;
    background-color: #131212be;
    .icon--close {
      border-radius: 50%;
      border: 1px solid $primary-orange-color;
      color: white;
      margin-right: -36px;
      cursor: pointer;
    }
    .icon--close:hover {
      color: $primary-orange-color;
    }
  }
  .body {
    background-color: white;
  }
  .footer {
    background-color: white;
    display: flex;
    justify-content: end;
    column-gap: 8px;
    padding: 8px 8px 8px 0px;
  }
}
</style>
<style lang="scss">
@import url(../../core/css/common.scss);
</style>
