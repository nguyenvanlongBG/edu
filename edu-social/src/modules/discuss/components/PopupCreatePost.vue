<template>
  <q-dialog ref="dialogRef" @hide="onDialogHide">
    <div class="container-create-post">
      <div class="header">Tạo câu hỏi</div>
      <div class="body">
        <EditorComponent v-model:content="contentNewPost" :readonly="false" />
      </div>
      <div class="footer">
        <EButton :label="$t('button.save')" @click="savePost" />
        <EButton :label="$t('button.cancel')" @click="cancel" />
      </div>
    </div>
  </q-dialog>
</template>
<script lang="ts">
import { defineComponent, ref } from 'vue';
import { useDialogPluginComponent } from 'quasar';
import EditorComponent from 'src/modules/core/components/rich-editor/EditorComponent.vue';
import EButton from 'src/modules/core/components/button/EButton.vue';
import { Post } from '../models/post';

export default defineComponent({
  name: 'PopupCreatePost',
  emits: [...useDialogPluginComponent.emits],
  setup() {
    const contentNewPost = ref('');
    const { dialogRef, onDialogHide, onDialogOK, onDialogCancel } =
      useDialogPluginComponent();
    function cancel() {
      onDialogHide();
    }
    function savePost() {
      const post: Post = {
        id: '3',
        content: contentNewPost.value,
        evaluate: 5,
        comments: [],
        user: {
          id: '',
          name: '',
          avatarUrl:
            'https://haycafe.vn/wp-content/uploads/2021/11/hinh-anh-hoat-hinh-de-thuong-cute-dep-nhat-600x600.jpg',
        },
      };
      onDialogOK(post);
    }
    return {
      contentNewPost,
      cancel,
      savePost,
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
