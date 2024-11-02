<script src="./editor-component.ts" lang="ts"></script>
<style src="./editor-component.scss" lang="scss" scoped></style>
<template>
  <div class="container-editor" ref="editContainerRef">
    <div :id="idToolbar">
      <button class="ql-bold"></button>
      <button class="ql-italic"></button>
      <button class="ql-underline"></button>
      <select class="ql-header"></select>
      <select class="ql-size">
        <option value="small"></option>
        <option selected></option>
        <option value="large"></option>
        <option value="huge"></option>
      </select>
      <select class="ql-font"></select>
      <select class="ql-color"></select>
      <select class="ql-background"></select>
      <select class="ql-align"></select>
      <button class="ql-link"></button>
      <button class="ql-image"></button>
      <button class="ql-video"></button>
      <button class="ql-formula" @click="showMathEditor"></button>
    </div>
    <QuillEditor contentType="delta" ref="myQuillEditor" v-model:content="texto" :options="editorOptions">
    </QuillEditor>
    <PopupMathEditor v-model:isShow="isShowMathEditor" :formulaValue="formula" @emitFormula="transferMainEditor">
    </PopupMathEditor>
  </div>
</template>
