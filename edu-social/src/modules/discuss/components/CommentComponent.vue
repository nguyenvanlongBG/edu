<template>
  <div class="comment">
    <AvatarComponent />
    <div class="comment-info">
      <span class="own-answer-name">Nguyễn Văn A</span>
      <div class="answer" @click="seeSolution">Xem lời giải</div>
      <div class="rate-star-and-time">
        <q-rating
          v-model="rate"
          size="2em"
          color="yellow-5"
          icon="star_border"
          icon-selected="star"
          icon-half="star_half"
          readonly
        />
        <span>Tạo lúc: 29/8/2023</span>
      </div>
    </div>
  </div>
</template>
<script src="../ts/comment"></script>
<style lang="scss">
@import url(../css/post.scss);
</style>
