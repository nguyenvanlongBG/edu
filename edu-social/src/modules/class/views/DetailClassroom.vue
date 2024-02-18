<template>
  <div class="container-detail-class">
    <div class="detail">
      <div class="post">
        <div class="owner-post">
          <AvatarComponent></AvatarComponent>
        </div>
        <div class="description">
          <div>Đây là một bài post test</div>
          <div class="actions">
            <q-btn color="white" text-color="black" label="Làm đề" />
            <q-btn color="white" text-color="black" label="Lịch sử" />
          </div>
        </div>
      </div>
      <div class="post">
        <div class="owner-post">
          <AvatarComponent></AvatarComponent>
        </div>
        <div class="description">
          <div>Đây là một bài post test</div>
          <div class="actions">
            <q-btn color="white" text-color="black" label="Làm đề" />
            <q-btn color="white" text-color="black" label="Lịch sử" />
          </div>
        </div>
      </div>
    </div>
    <div class="toolbar-class">
      <div>
        <q-input rounded outlined v-model="text">
          <template v-slot:append> </template>
        </q-input>
      </div>
      <div class="item">
        <div>
          <img
            class="icon-32px"
            src="../../core/assets/pictures/file-and-folder.png"
            alt=""
          />
        </div>
        <span>Tài liệu</span>
      </div>
      <div class="item expand" @click="changeStatusExpand">
        <span>Thành viên</span>
        <span v-if="isExpandingMember"
          ><q-icon name="expand_less" size="32px"></q-icon
        ></span>
        <span v-if="!isExpandingMember"
          ><q-icon name="expand_more" size="32px"></q-icon
        ></span>
      </div>
      <div class="members" v-if="isExpandingMember">
        <div class="member">Nguyễn Văn A</div>
        <div class="member">Nguyễn Văn B</div>
        <div class="add-member">
          <q-icon name="add_circle_outline" size="20px"></q-icon>
          <span>Thêm thành viên</span>
        </div>
      </div>
    </div>
  </div>
</template>
<script>
import AvatarComponent from 'src/modules/core/components/avatar/AvatarComponent.vue';
import { ref } from 'vue';

export default {
  name: 'DetailClassroom',
  components: { AvatarComponent },
  setup() {
    const isExpandingMember = ref(false);
    function changeStatusExpand() {
      isExpandingMember.value = !isExpandingMember.value;
    }
    return {
      isExpandingMember,
      changeStatusExpand,
    };
  },
};
</script>
<style src="../css/index.scss" lang="scss" scoped></style>
