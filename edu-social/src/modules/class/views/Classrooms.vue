<template>
  <div class="container">
    <h4>Lớp học của bạn</h4>
    <div class="list-classroom">
      <ClassroomComponent />
      <ClassroomComponent />
      <ClassroomComponent />
      <ClassroomComponent />
      <ClassroomComponent />
      <ClassroomComponent />
    </div>
  </div>
</template>
<script>
import ClassroomComponent from '../components/ClassroomComponent.vue';
export default {
  name: 'ListClassroom',
  components: {
    ClassroomComponent,
  },
};
</script>
<style src="../css/index.scss" lang="scss" scoped></style>
