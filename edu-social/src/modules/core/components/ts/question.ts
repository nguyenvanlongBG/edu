import { defineComponent, ref } from 'vue';
import EditorComponent from '../../components/rich-editor/EditorComponent.vue';
import { Answer, Question } from '../../models';
import { EditorControl } from '../../models/editor/editor-control';
export default defineComponent({
  name: 'QuestionComponent',
  components: {
    EditorComponent,
  },
  props: {
    question: {
      type: Object as () => Question,
      required: true,
    },
    statusEdit: {
      type: Boolean,
      required: false,
      default: false,
    },
  },
  emits: ['update:question'],
  created() {
    this.questionLocal = this.question;
    if (this.question?.result) {
      this.results = this.question?.result.split(',');
    } else {
      this.results = [];
    }
  },
  setup(props, ctx) {
    const editMode = ref(0);
    const isEdit = ref(false);
    const results = ref([] as string[]);
    const questionLocal = ref({} as Question);
    const edittorControl = ref(new EditorControl());
    function createAnswer() {
      const answer = {
        id: '',
        content: '',
      } as Answer;
      questionLocal.value.answers.push(answer);
      ctx.emit('update:question');
    }
    function changeEditStatus(val: boolean) {
      isEdit.value = val;
    }
    return {
      questionLocal,
      edittorControl,
      createAnswer,
      editMode,
      isEdit,
      changeEditStatus,
      results,
    };
  },
});
