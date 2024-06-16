<template>
  <div class="forum-container">
    <div class="main-content">
      <EditorComponent />
      <div class="posts" v-if="showPostsView">
        <div class="create-post">
          <AvatarComponent />
          <q-input
            :placeholder="$t('discuss.createPostPlaceholder')"
            bg-color="grey"
            color="dark"
            rounded
            standout
            v-model="text"
            input-class="input-create-post"
            @click="openPopupCreatePost"
            readonly
          />
        </div>
        <div class="list-post">
          <PostComponent :post="post" v-for="post in getPosts" :key="post.id" />
        </div>
      </div>
      <div class="message" v-else>
        <ChatComponent />
      </div>
    </div>
    <ToolBar @showMessage="changeMode" />
  </div>
</template>
<script lang="ts" src="../ts/forums.ts"></script>
<style>
@import url(../css/container-forum.scss);
</style>
