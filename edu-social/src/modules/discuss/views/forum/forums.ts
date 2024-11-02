import { defineComponent, ref } from 'vue';
import ToolBar from '@discuss/components/ToolBar.vue';
import PostComponent from '@discuss/components/PostComponent.vue';
import PopupCreatePost from '@discuss/components/PopupCreatePost.vue';
import AvatarComponent from '@core/components/avatar/AvatarComponent.vue';
import ChatComponent from '@core/components/chat/ChatComponent.vue';
import EditorComponent from '@core/components/rich-editor/EditorComponent.vue';
import { useQuasar } from 'quasar';
import { Post } from '@discuss/models/post';
import { usePostStore } from '@discuss/stores/post';
import { mapActions, storeToRefs } from 'pinia';
export default defineComponent({
  name: 'ForumView',
  components: {
    ToolBar,
    PostComponent,
    AvatarComponent,
    ChatComponent,
    EditorComponent,
  },

  setup() {
    const postsStore = usePostStore();
    const text = ref('');
    const showPostsView = ref(true);
    const $q = useQuasar();
    const { getPosts } = storeToRefs(postsStore);
    function openPopupCreatePost() {
      const popupCreatePost = $q.dialog({
        component: PopupCreatePost,
      });
      popupCreatePost
        .onCancel(() => {
          console.log('Cancel');
        })
        .onOk((post: Post) => {
          post.id = '' + (getPosts.value.length + 1);
          postsStore.savePost(post);
        });
    }
    function changeMode() {
      showPostsView.value = !showPostsView.value;
    }

    return {
      ...mapActions(usePostStore, ['savePost']),
      postsStore,
      text,
      getPosts,
      openPopupCreatePost,
      showPostsView,
      changeMode,
    };
  },
});
