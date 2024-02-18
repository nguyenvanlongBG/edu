<template>
  <div class="container-classroom" @click="goToClassroom(1)">
    <div class="classroom">
      <div class="box-action-classroom">
        <i
          class="fa-solid fa-ellipsis icon-tool-classroom"
          v-on:click.stop
          @click="displayActionClassroom"
        ></i>
        <ul v-if="displayNavClassroom" class="menu-action-classroom">
          <li>
            <div class="action-classroom">Chia sẻ</div>
          </li>
          <li>
            <div class="action-classroom">Rời lớp</div>
          </li>
          <li>
            <div class="action-classroom">Xóa</div>
          </li>
        </ul>
      </div>
      <div class="box-image-classroom">
        <img class="image-classroom" src="../assets/picture/classroom.png" />
      </div>
      <div class="name-classroom">Lớp cô Hoa</div>
    </div>
  </div>
</template>
<script>
import { ref } from 'vue';

export default {
  name: 'ClassroomComponent',
  setup() {
    const displayNavClassroom = ref(false);
    return {
      displayNavClassroom,
    };
  },
  methods: {
    displayActionClassroom() {
      this.displayNavClassroom = !this.displayNavClassroom;
    },
  },
};
</script>
<style scoped>
.container-classroom {
  padding: 8px;
  width: 100%;
  box-sizing: border-box;
}

.classroom {
  background-color: rgba(191, 198, 204, 0.507);
  height: 250px;
  border-radius: 8px;
  cursor: pointer;
  border: 2px solid rgb(199, 212, 223);
  box-sizing: border-box;
}

.box-action-classroom {
  display: flex;
  flex-direction: column;
  position: relative;
  align-items: flex-end;
}

.box-action-classroom ul {
  display: block;
  padding-left: 0px;
  min-width: 100px;
}

.menu-action-classroom {
  position: absolute;
  top: 5px;
  right: 5px;
  border: 2px solid #033e63;
  background-color: #e6eff5;
  border-radius: 5px;
}

/* .menu-action-classroom::after {
    content: '';
    position: absolute;
    top: 0;
    left: 90%;
    margin-left: -15px;
    margin-top: -15px;
    width: 0;
    z-index: 1;
    height: 0;
    border-bottom: solid 10px #222020;
    border-left: solid 10px transparent;
    border-right: solid 10px transparent;
} */

.menu-action-classroom li {
  display: block;
  list-style: none;
  background-color: #025c94;
  text-align: center;
}

.action-classroom {
}

.action-classroom:hover {
  color: white;
}

.icon-tool-classroom {
  font-size: 25px;
  margin-right: 5px;
}

.classroom:hover {
  border: 2px solid #025c94 !important;
}

.box-image-classroom {
  display: flex;
  justify-content: center;
}

.image-classroom {
  width: 80px;
  height: auto;
}

.name-classroom {
  margin-top: 5px;
  text-align: center;
  font-size: 25px;
  font-weight: bold;
}
</style>
