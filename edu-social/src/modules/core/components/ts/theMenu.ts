import { defineComponent, ref } from 'vue';
import { Menu } from '../../enums/Menu';
import GroupMenu from '../menu/GroupMenu.vue';
import router from 'src/router';
export default defineComponent({
  name: 'TheMenu',
  components: {
    GroupMenu,
  },
  setup() {
    const pageFocus = ref(1);
    const routerNames = [
      'forum',
      'forum',
      'groups',
      'groups',
      'groups',
      'groups',
      'groups',
    ];
    function navigateToPage(indexItemMenu = 0, params = null) {
      pageFocus.value = indexItemMenu;
      console.log(params, routerNames, indexItemMenu);
      router.push({ name: routerNames[indexItemMenu] });
    }
    return { navigateToPage, pageFocus, Menu };
  },
});
