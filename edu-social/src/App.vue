<template>
  <router-view />
  <!-- <div>
    <h1 class="title">Hello Vue 3!!!!</h1>
    <button @click="connect">Connect</button>
    <button @click="emit">emit</button>
  </div> -->
</template>

<script lang="ts">
import { ref } from 'vue';
import { socket } from '../src/modules/core/services/socket';
export default {
  setup() {
    function connect() {
      socket.connect();
    }
    function emit() {
      socket.emit('name', 'name');
    }
    return {
      connect,
      emit,
    };
  },
};
</script>
