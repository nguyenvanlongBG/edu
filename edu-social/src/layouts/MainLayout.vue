<template>
  <q-layout class="main-layout" view="lHh Lpr lFf">
    <TheMenu />
    <q-page-container class="main-container">
      <the-header />
      <router-view></router-view>
    </q-page-container>
  </q-layout>
</template>

<script setup lang="ts">
import TheMenu from '../modules/core/components/menu/TheMenu.vue';
import TheHeader from '../modules/core/components/header/TheHeader.vue';
</script>
<style>
.main-layout {
  display: flex;
}
.main-container {
  flex: 1;
  background-color: #f0f2f5;
  display: flex;
  flex-direction: column;
  max-height: calc(100vh);
  overflow-y: hidden;
}
</style>
