<template>
  <div>OK</div>
  <!-- <EInput v-model:inputValue="inputValue" :rules="[validateInput]"></EInput> -->
  <div class="test-view">
    <EditorComponent :control="editor" />
    <form @submit.prevent="testUploadFile">
      <div>
        <label for="fileInput">Chọn tệp:</label>
        <input type="file" @change="handleFileChange" />
      </div>
      <div>
        <label for="testId">Test ID:</label>
        <input type="number" id="testId" v-model="testId" />
      </div>
      <button type="submit">Gửi</button>
    </form>
  </div>

</template>
<script>
import { ref } from 'vue';
import originRequest from '@core/utils/origin-request';
import EditorComponent from '@core/components/rich-editor/EditorComponent.vue';
import { EditorControl } from '../../core/models/editor/editor-control';

export default {
  name: 'TestView',
  components: {
    EditorComponent
  },

  setup() {
    const inputValue = ref('')
    const options = ref([{ id: 1, name: 'A', age: 10 }])
    const selected = ref({ id: 1, name: 'A', age: 10 })
    const fileInput = ref(null)
    const editor = ref(new EditorControl())
    function validateInput() {
      return {
        isValid: false,
        message: 'Không hợp lệ'
      }
    }
    const handleFileChange = (event) => {
      fileInput.value = event.target.files[0]; // Lưu tệp vào biến `fileInput`
    };
    function handleSelect(value) {
      this.selected = value
    }
    async function testUploadFile() {

      if (fileInput.value === 0) {
        this.message = 'Vui lòng chọn một tệp!';
        return;
      }
      const formData = new FormData();
      formData.append('file', fileInput.value); // Thêm tệp vào FormData
      formData.append('testId', 1); // Thêm các trường khác nếu cần
      let result = await originRequest({
        url: 'Login/uploadFile',
        method: 'POST',
        data: formData,
      })
      editor.value.value = result
    }
    return {
      inputValue, validateInput, options, selected, testUploadFile, fileInput, handleFileChange, editor, handleSelect
    }
  }
};
</script>
<style src="../css/testview.scss" lang="scss"></style>
