<template>
  <div class="group-toolbar">
    <div class="group-avatar">
      <AvatarComponent />
      <div class="group-name">Nhóm 1</div>
    </div>
    <div>
      <e-input />
    </div>
    <div class="item-toolbar">Thảo luận</div>
    <div class="item-toolbar">Tạo đề</div>
    <div class="item-toolbar">Danh sách thành viên</div>
    <div class="item-toolbar">Ngân hàng câu hỏi</div>
    <div class="item-toolbar">Ôn tập</div>
  </div>
</template>
<script>
import AvatarComponent from '../../core/components/avatar/AvatarComponent.vue';
import EInput from '../../core/components/input/EInput.vue';

export default {
  components: {
    AvatarComponent,
    EInput,
  },
};
</script>
<style src="../css/group-view.scss" lang="scss"></style>
