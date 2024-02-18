import { defineComponent, ref } from 'vue';
import QuestionComponent from '../../core/components/question/QuestionComponent.vue';
import { useExamStore } from '../store';
import { storeToRefs } from 'pinia';
export default defineComponent({
  components: {
    QuestionComponent,
  },
  props: {
    editMode: {
      type: Number,
      required: false,
      default: 1,
    },
  },
  setup() {
    const examStore = useExamStore();
    const { getQuestions } = storeToRefs(examStore);
    const hasRoleEdit = ref(true);
    return {
      examStore,
      getQuestions,
      hasRoleEdit,
    };
  },
});
