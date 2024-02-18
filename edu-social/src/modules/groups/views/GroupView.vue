<template>
  <div class="group-container">
    <div class="main">
      <GroupPostComponent />
    </div>
    <GroupToolBar />
  </div>
</template>
<script>
import GroupPostComponent from '../components/GroupPostComponent.vue';
import GroupToolBar from '../components/GroupToolBar.vue';

export default {
  components: {
    GroupPostComponent,
    GroupToolBar,
  },
};
</script>
<style src="../css/group-view.scss" lang="scss"></style>
