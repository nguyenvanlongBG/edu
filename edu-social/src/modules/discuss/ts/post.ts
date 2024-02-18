import { defineComponent, ref } from 'vue';
import AvatarComponent from '../../core/components/avatar/AvatarComponent.vue';
import EditorComponent from '../../core/components/rich-editor/EditorComponent.vue';
import CommentComponent from '../components/CommentComponent.vue';
import PopupChat from '../../core/components/chat/PopupChat.vue';
import { usePostStore } from '../stores/post';
import { Comment } from '../models/comment';
import PopupCreateComment from '../components/PopupCreateComment.vue';
import { useQuasar } from 'quasar';
export default defineComponent({
  props: ['post'],
  components: {
    AvatarComponent,
    EditorComponent,
    CommentComponent,
    PopupCreateComment,
    PopupChat,
  },
  setup(state) {
    const postsStore = usePostStore();
    const ratingModel = ref(3);
    const text = ref('');
    const $q = useQuasar();
    function showCreateComment() {
      const popupCreateComment = $q.dialog({
        component: PopupCreateComment,
        componentProps: {
          post: state.post,
        },
      });
      popupCreateComment
        .onCancel(() => {
          console.log('Cancel');
        })
        .onOk((comment: Comment) => {
          postsStore.createComment(state.post.id, comment);
        });
    }
    return {
      ratingModel,
      showCreateComment,
      text,
    };
  },
});
