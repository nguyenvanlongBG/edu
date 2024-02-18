<template>
  <div class="group__menu">
    <div class="header">
      <span class="title-group-study">Nhóm</span>
      <div class="find-and-add">
        <EInput />
        <q-icon name="control_point" size="24px" class="icon-add-group" />
      </div>
    </div>
    <template v-for="group in groups" :key="group">
      <input
        type="radio"
        :id="'group_' + group"
        :value="group"
        name="groups"
        v-model="focusGroup"
        class="hidden-input-radio"
      />
      <lebel
        class="group"
        :for="'group_' + group"
        @click="changeGroup('' + group)"
      >
        <AvatarComponent />
        <div class="status-active">
          <span class="group-name"> Nhóm 1 </span>
          <span class="last-active"> Nguyễn Văn A: Đề tổng hợp </span>
        </div>
      </lebel>
    </template>
  </div>
</template>
<script lang="ts">
import EInput from '../input/EInput.vue';
import AvatarComponent from '../../../core/components/avatar/AvatarComponent.vue';
import { ref } from 'vue';
export default {
  components: {
    EInput,
    AvatarComponent,
  },
  setup() {
    const groups = ref([1, 2, 3, 4]);
    const focusGroup = ref('1');
    function changeGroup(groupID = '') {
      focusGroup.value = groupID;
    }
    return { groups, focusGroup, changeGroup };
  },
};
</script>
<style src="../../css/the-menu.scss" lang="scss"></style>
