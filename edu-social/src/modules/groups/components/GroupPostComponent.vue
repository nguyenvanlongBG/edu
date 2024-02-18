<template>
  <div class="group-post">
    <div class="posts">
      <div class="post">
        <div class="mask">
          <q-btn
            rounded
            label="Sửa"
            size="16px"
            color="teal"
            @click="updateTest"
            class="action-post"
          />
          <q-btn
            rounded
            label="Làm bài"
            size="16px"
            color="teal"
            @click="practiceExam"
            class="action-post"
          />
        </div>
        <div class="toolbar">
          <div class="author">
            <AvatarComponent />
            <div class="created">
              <div class="author-name">Nguyễn Văn A</div>
              <div class="created-date">12/2/2023</div>
            </div>
          </div>
          <q-icon class="more-info" size="24px" name="more_horiz"></q-icon>
        </div>
        <div class="title-post">Đề thi Lim</div>
        <div class="line">
          <span class="label">Môn:</span>
          <span class="detail">Toán</span>
        </div>
        <div class="line">
          <span class="label">Chủ đề:</span>
          <span class="detail">Giới hạn</span>
        </div>
        <div class="line">
          <span class="label">Mô tả:</span>
          <div class="detail">Đề nâng cao về giới hạn</div>
        </div>
        <div class="picture-default">
          <img
            src="../../../modules/core/assets/pictures/exam-group-post-default.png"
          />
        </div>
        <div class="list-user-do">
          <span class="label">Những người đã làm:</span>
          <div class="users">
            <div class="user">
              <AvatarComponent />
            </div>
            <div class="user">
              <AvatarComponent />
            </div>
            <div class="user">
              <AvatarComponent />
            </div>
            <div class="user">
              <AvatarComponent />
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>
<script lang="ts">
import router from 'src/router';
import AvatarComponent from '../../core/components/avatar/AvatarComponent.vue';
export default {
  components: {
    AvatarComponent,
  },
  setup() {
    function updateTest() {
      router.push({
        name: 'exam',
        params: {
          id: 1,
        },
        query: {
          editMode: 2,
        },
      });
    }
    function practiceExam() {
      router.push({
        name: 'exam',
        params: {
          id: 1,
        },
        query: {
          editMode: 1,
        },
      });
    }
    return {
      updateTest,
      practiceExam,
    };
  },
};
</script>
<style src="../css/group-view.scss" lang="scss"></style>
