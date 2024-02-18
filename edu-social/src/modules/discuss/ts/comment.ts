import { defineComponent, ref } from 'vue';
import AvatarComponent from '../../core/components/avatar/AvatarComponent.vue';
import PopupChat from '../../core/components/chat/PopupChat.vue';
import { useQuasar } from 'quasar';

export default defineComponent({
  components: {
    AvatarComponent,
    PopupChat,
  },
  emits: ['seeSolution'],
  setup() {
    const rate = ref(4);
    const isShowAnswer = ref(false);
    const showChatSolution = ref(false);
    const $q = useQuasar();
    function seeSolution() {
      $q.dialog({
        component: PopupChat,
      }).onCancel(() => {
        console.log('Cancel');
      });
    }
    function closeChatSolution() {
      showChatSolution.value = false;
    }
    return {
      rate,
      isShowAnswer,
      seeSolution,
      showChatSolution,
      closeChatSolution,
    };
  },
});
