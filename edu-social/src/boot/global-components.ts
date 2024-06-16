import { boot } from 'quasar/wrappers';

import { createApp } from 'vue';
import App from 'src/App.vue';
import { QuillEditor } from '@vueup/vue-quill';

export default boot(() => {
  const app = createApp(App);
});
