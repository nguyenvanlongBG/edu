<template>
  <div class="toolbar-forum">
    <div class="title">Trao đổi gần đây</div>
    <div class="categories-message">
      <input
        name="category-message"
        class="hidden-input-type-message"
        type="radio"
        id="message-question"
        value="question"
      />
      <label class="category-message" for="message-question"> Câu hỏi </label>
      <input
        name="category-message"
        type="radio"
        class="hidden-input-type-message"
        id="message-answer"
        value="answer"
      />
      <label class="category-message" for="message-answer">Trả lời</label>
    </div>
    <div class="body-toolbar-forum">
      <div class="search-message">
        <EInput qClass="q-class-custom" :placeholder="$t('discuss.findTest')" />
      </div>
      <div class="messages">
        <div class="message" @click="showMessage">
          <AvatarComponent />
          <div class="general-message">
            <span class="receiver-name">Bạn tôi</span>
            <span class="last-message">Bạn: Thân yêu</span>
          </div>
        </div>
        <div class="message" @click="showMessage">
          <AvatarComponent />
          <div class="general-message">
            <span class="receiver-name">Bạn tôi</span>
            <span class="last-message">Bạn: Thân yêu</span>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>
<script src="../ts/toolbar.ts" lang="ts"></script>
<style lang="scss">
@import url(../css/toolbar.scss);
</style>
