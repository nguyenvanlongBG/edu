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
      <button class="ql-formula"></button>
    </div>
    <QuillEditor
      contentType="delta"
      ref="myQuillEditor"
      v-model:content="texto"
      :options="editorOptions"
    >
    </QuillEditor>
    <PopupMathEditor
      v-model:isShow="isShowMathEditor"
      :formulaValue="formula"
      @emitFormula="transferMainEditor"
    ></PopupMathEditor>
  </div>
</template>

<script lang="ts">
import { defineComponent, ref } from 'vue';
import katex from 'katex';
import 'katex/dist/katex.min.css';
import { Delta, QuillEditor } from '@vueup/vue-quill';
import '@vueup/vue-quill/dist/vue-quill.snow.css';
import { ActionEditor } from '../../enums/Common';
import { useQuasar } from 'quasar';
import PopupMathEditor from './PopupMathEditor.vue';
import commonFunction from '../../commons/CommonFunction';

window.katex = katex;
export default defineComponent({
  name: 'EditorComponent',
  components: { QuillEditor, PopupMathEditor },
  created() {
    this.texto = new Delta([
      {
        insert: {
          formula:
            '{\\left(1+x\\right)}^{n}=1+\\frac{nx}{1!}+\\frac{n\\left(n-1\\right){x}^{2}}{2!}',
        },
      },
      {
        insert: '\n',
      },
    ]);
  },
  mounted() {
    if (
      this.editContainerRef &&
      this.editContainerRef &&
      this.editContainerRef.querySelectorAll('.ql-formula')
    ) {
      var formulaElements =
        this.editContainerRef.querySelectorAll('.ql-formula');
      formulaElements.forEach((element) => {
        element.addEventListener('click', () => {
          var dataValue = element.getAttribute('data-value');
          let formula = dataValue ?? '';
          this.isShowMathEditor = true;
          this.actionEditor = ActionEditor.EDIT;
          // this.$q
          //   .dialog({
          //     component: DialogMathEditor,
          //     componentProps: {
          //       formulaProp: formula,
          //     },
          //   })
          //   .onOk((formula: string) => {
          //     this.transferMainEditor(formula);
          //   });
        });
      });
    }
  },
  setup() {
    const myQuillEditor = ref(null);
    const editContainerRef = ref(null);
    const isShowMathEditor = ref(false);
    const actionEditor = ref(ActionEditor.INSERT);
    const formula = ref(
      '{\\left(1+x\\right)}^{n}=1+\\frac{nx}{1!}+\\frac{n\\left(n-1\\right){x}^{2}}{2!}+\\dots'
    );
    const macros = {
      smallfrac: {
        args: 2,
        def: '{}^{#1}\\!\\!/\\!{}_{#2}',
        captureSelection: false,
      },
      genfrac: {
        args: 6,
        def: '\\binom{#4}{#3}',
      },
      dots: {
        args: 0,
        def: '...',
        captureSelection: false,
      },
    };
    const texto = ref(null);
    const $q = useQuasar();
    const idToolbar = 'toolbar-' + commonFunction.generateID();
    function showMathEditor() {
      isShowMathEditor.value = true;
    }
    const editorOptions = ref({
      modules: {
        toolbar: {
          container: '#' + idToolbar,
          handlers: {
            formula: () => {
              isShowMathEditor.value = true;
              // $q.dialog({
              //   component: DialogMathEditor,
              // });
            },
          },
        },
      },
    });
    function transferMainEditor(formula: string) {
      if (!myQuillEditor.value) return;
      const quillInstance = myQuillEditor.value.getQuill();
      quillInstance.focus({ preventScroll: true });
      const cursorPosition = quillInstance.getSelection();
      var position =
        cursorPosition && cursorPosition.index ? cursorPosition.index : 0;
      if (actionEditor.value == ActionEditor.INSERT) {
        quillInstance.insertEmbed(position, 'formula', formula);
      } else {
        var delta = quillInstance.getContents();
        if (position) {
          delta.ops.splice(position - 1, 1);
        } else {
          delta.ops.splice(0, 1);
        }
        quillInstance.setContents(delta);
        if (position) {
          quillInstance.insertEmbed(position - 1, 'formula', formula);
        } else {
          quillInstance.insertEmbed(0, 'formula', formula);
        }
      }
      console.log(quillInstance.getContents());
      if (
        !editContainerRef.value ||
        !editContainerRef.value.querySelectorAll('.ql-formula')
      )
        return;
      var formulaElements =
        editContainerRef.value.querySelectorAll('.ql-formula');
      actionEditor.value = ActionEditor.INSERT;
      isShowMathEditor.value = false;
      const me = this;
      formulaElements.forEach((element) => {
        element.addEventListener('click', () => {
          actionEditor.value = ActionEditor.EDIT;
          var dataValue = element.getAttribute('data-value');
          let formula = dataValue ?? '';
          console.log(cursorPosition);
          isShowMathEditor.value = true;
          // $q.dialog({
          //   component: DialogMathEditor,
          //   componentProps: {
          //     formulaProp: formula,
          //   },
          // }).onOk((formula: string) => {
          //   me.transferMainEditor(formula);
          // });
        });
      });
    }
    function handleInput(e) {
      formula.value = e.target.value;
      console.log(formula);
    }
    function changeStatus() {
      isShowMathEditor.value = !isShowMathEditor.value;
    }
    return {
      myQuillEditor,
      editContainerRef,
      idToolbar,
      isShowMathEditor,
      formula,
      texto,
      editorOptions,
      transferMainEditor,
      handleInput,
      showMathEditor,
      actionEditor,
      macros,
      changeStatus,
    };
  },
});
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style lang="scss" scoped>
.ql-customButton {
  background-color: #ff0000;
  /* Màu nền của nút */
  color: #ffffff;
  /* Màu chữ của nút */
  border: none;
  padding: 5px 10px;
  border-radius: 5px;
  cursor: pointer;
}
</style>
