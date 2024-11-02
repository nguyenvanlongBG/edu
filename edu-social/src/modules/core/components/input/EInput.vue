<template>
  <div class="e-input" :class="isFocus?'focus-e-input':''">
    <input class="default-input" :value='inputValue' @input="updateValue" :placeholder="placeholder" @blur="blur" @focus="focus"/>
  </div>
</template>
<script lang="ts">
import { ref } from 'vue';
import { ResultValidateBase } from '../../models';
export default {
  name: 'EInput',
  props: {
    inputValue: {
      type: String,
      required: true,
      default: '',
    },
    qClass: {
      type: String,
      required: false,
      default: '',
    },
    placeholder: {
      type: String,
      required: false,
      default: '',
    },
    rules: {
      type: Array,
      required: false,
      default: () => []
    },
    preUpdateValue: {
      type: Function,
      required: false,
      default: (value: string) => {return value}
    }
  },
  emits: [
    'update:inputValue'
  ],
  setup(props, ctx) {
    const value = ref('');
    const isFocus = ref(false)
    function validateRule(value: string) {
      let resultValidate = {
        isValid: true,
        message: ''
      } as ResultValidateBase
      if (props.rules && props.rules.length) {
        if (typeof props.rules[0] === 'function') {
            // Hàm validate custom
          let messagesNotValid = [] as string[]
          props.rules.forEach(rule => {
            if (typeof rule === 'function') {
              let result = rule(value)
              if (result && !result.isValid) {
                resultValidate.isValid = false
                messagesNotValid.push(result.message)
              }
            }
          })
          resultValidate.message = messagesNotValid.join('. ')
        } else if (typeof props.rules[0] === 'string') {
          // Chuỗi Regex
          for (let index = 0; index < props.rules.length; index++) {
            let rule = props.rules[index] as string
            const regex = new RegExp(rule);
            let isValid = regex.test(value);
            if (!isValid) {
              resultValidate.isValid = false
              resultValidate.message = this.$t('common.NotValid')
              return resultValidate
            }
          }
        }
      }
      return resultValidate
    }
    function updateValue(event) {
      let resultValidate = validateRule(event.target.value)
      console.log(resultValidate)
      ctx.emit('update:inputValue', event.target.value)
    }
    function focus() {
      isFocus.value = true
    }
    function blur() {
      isFocus.value = false
    }
    return {
      value, validateRule, updateValue, isFocus, blur, focus
    };
  },
};
</script>
<style src="../../css/e-control.scss" lang="scss"></style>
