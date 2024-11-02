import { defineComponent, ref } from 'vue';
import katex from 'katex';
import 'katex/dist/katex.min.css';
import { Delta, QuillEditor } from '@vueup/vue-quill';
import '@vueup/vue-quill/dist/vue-quill.snow.css';
import { ActionEditor } from '../../enums/Common';
import { useQuasar } from 'quasar';
import PopupMathEditor from './PopupMathEditor.vue';
import commonFunction from '../../commons/CommonFunction';
import { EditorControl } from '../../models/editor/editor-control';
(window as any).katex = katex;
export default defineComponent({
  name: 'EditorComponent',
  components: { QuillEditor, PopupMathEditor },
  props: {
    control: {
      type: EditorControl,
      required: true,
    },
  },
  watch: {
    'control.value': {
      handler(newValue, oldValue) {
        const insertData = [
          ...newValue,
          {
            insert: '\n',
          },
        ];
        this.texto = new Delta(insertData);
      },
      immediate: true, // Gọi handler ngay lập tức với giá trị hiện tại
      deep: true, // Theo dõi sâu cho các thuộc tính lồng nhau
    },
  },
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
      this.editContainerRef.querySelectorAll('.ql-formula')
    ) {
      const formulaElements = this.editContainerRef.querySelectorAll(
        '.ql-formula'
      ) as HTMLElement[];
      if (!formulaElements || !formulaElements.length) return;
      formulaElements.forEach((element) => {
        element.addEventListener('click', () => {
          const dataValue = element.getAttribute('data-value');
          const formula = dataValue ?? '';
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
    const myQuillEditor = ref<InstanceType<typeof QuillEditor> | null>(null);
    const editContainerRef = ref<HTMLElement | null>(null);
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
    const texto = ref(new Delta([]));
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
      quillInstance.focus();
      const cursorPosition = quillInstance.getSelection();
      const position =
        cursorPosition && cursorPosition.index ? cursorPosition.index : 0;
      if (actionEditor.value == ActionEditor.INSERT) {
        quillInstance.insertEmbed(position, 'formula', formula);
      } else {
        const delta = quillInstance.getContents();
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
      const formulaElements =
        editContainerRef.value.querySelectorAll('.ql-formula');
      actionEditor.value = ActionEditor.INSERT;
      isShowMathEditor.value = false;
      formulaElements.forEach((element) => {
        element.addEventListener('click', () => {
          actionEditor.value = ActionEditor.EDIT;
          const dataValue = element.getAttribute('data-value');
          const formula = dataValue ?? '';
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
