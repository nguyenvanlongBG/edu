<template>
  <div class="avatar-container">
    <img :src="user.avatarUrl" class="avatar-size" />
  </div>
</template>
<script>
import { defineComponent } from 'vue';
import { User } from '../../../core/models/index';
export default defineComponent({
  props: {
    user: {
      type: User,
      required: false,
      default: {
        id: 1,
        name: 'Test',
        avatarUrl:
          'https://haycafe.vn/wp-content/uploads/2021/11/hinh-anh-hoat-hinh-de-thuong-cute-dep-nhat-600x600.jpg',
      },
    },
  },
});
</script>
<style lang="scss">
@import url(../../css/avatar.scss);
</style>
