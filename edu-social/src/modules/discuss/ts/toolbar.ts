import { defineComponent, ref } from 'vue';
import AvatarComponent from '../../core/components/avatar/AvatarComponent.vue';
import EInput from '../../core/components/input/EInput.vue';
export default defineComponent({
  components: {
    AvatarComponent,
    EInput,
  },
  emits: ['showMessage'],
  setup(props, ctx) {
    const searchMessage = ref('');
    function showMessage() {
      ctx.emit('showMessage');
    }
    return {
      searchMessage,
      showMessage,
    };
  },
});
