<template>
  <div class="container-exam">
    <div class="body">
      <div class="toolbar-exam" v-if="editMode == 2">
        <q-btn color="primary" class="create-question" label="Thêm câu hỏi" />
      </div>
      <QuestionComponent
        v-for="question in getQuestions"
        :key="question.id"
        :question="question"
        :statusEdit="hasRoleEdit && editMode == 2"
      />
    </div>
    <div class="navbar-questions">
      <div class="numeric-question-board">
        <div class="numeric-question">1</div>
        <div class="numeric-question">2</div>
        <div class="numeric-question">3</div>
        <div class="numeric-question">4</div>
        <div class="numeric-question">5</div>
        <div class="numeric-question">6</div>
        <div class="numeric-question">7</div>
        <div class="numeric-question">8</div>
        <div class="numeric-question">9</div>
        <div class="numeric-question">10</div>
      </div>
    </div>
  </div>
</template>
<script src="../ts/exam.ts" lang="ts"></script>
<style src="../css/index.scss" lang="scss"></style>
