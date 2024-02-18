<template>
  <div class="post">
    <div class="own-post">
      <AvatarComponent :user="post.user" />
    </div>
    <EditorComponent :content="post.content" :readonly="true" />
    <div class="comments">
      <div class="create-comment" @click="showCreateComment">
        <AvatarComponent />
        <q-input
          :placeholder="$t('discuss.answer')"
          bg-color="grey"
          color="dark"
          rounded
          standout
          v-model="text"
          input-class="input-create-comment"
          readonly
        />
      </div>
      <CommentComponent v-for="comment in post.comments" :key="comment.id" />

      <span v-if="post.comments.length > 4" class="see-more">Xem thêm</span>
    </div>
  </div>
</template>
<script lang="ts" src="../ts/post.ts"></script>
<style>
@import url(../css/post.scss);
</style>
