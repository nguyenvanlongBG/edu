<template>
  <div class="container-question">
    <div class="toolbar-question">
      <q-btn
        rounded
        label="Sửa"
        size="16px"
        color="teal"
        @click="changeEditStatus(true)"
        v-if="!isEdit && statusEdit"
      />
      <q-btn
        rounded
        label="Đóng"
        size="16px"
        color="teal"
        @click="changeEditStatus(false)"
        v-if="isEdit && statusEdit"
      />
    </div>
    <div class="question-content">
      <EditorComponent :content="question.content" :isReadonly="!isEdit" />
    </div>
    <q-list class="answers" v-if="question.type == 2">
      <q-item
        :active="results.includes(answer.id) ? true : false"
        active-class="result"
        class="answer"
        color="teal"
        tag="label"
        v-for="answer in question.answers"
        :key="answer.id"
        v-ripple
      >
        <q-item-section class="hidden" avatar top>
          <q-radio
            :disable="isEdit"
            v-model="results[0]"
            :val="answer.id"
          ></q-radio>
        </q-item-section>
        <q-item-section>
          <div class="content-answer">
            <EditorComponent :content="answer.content" :isReadonly="!isEdit" />
          </div>
        </q-item-section>
      </q-item>
      <div class="add-answer" @click="createAnswer" v-if="isEdit">
        Thêm câu trả lời
      </div>
    </q-list>

    <q-list class="answers" v-if="question.type == 3">
      <q-item
        :active="results.includes(answer.id) ? true : false"
        active-class="result"
        class="answer"
        color="teal"
        tag="label"
        v-for="answer in question.answers"
        :key="answer.id"
        v-ripple
      >
        <q-item-section avatar top>
          <q-checkbox
            :disable="isEdit"
            v-model="results"
            :val="answer.id"
          ></q-checkbox>
        </q-item-section>
        <q-item-section>
          <div class="content-answer">
            <EditorComponent :isReadonly="!isEdit" />
          </div>
        </q-item-section>
      </q-item>
      <div class="add-answer" @click="createAnswer" v-if="isEdit">
        Thêm câu trả lời
      </div>
    </q-list>
  </div>
</template>
<script src="../ts/question.ts" lang="ts"></script>
<style src="../../css/question.scss" lang="scss"></style>
