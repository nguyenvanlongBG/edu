<template>
  <div class="e-combobox" :class="isFocus ? 'focus-e-input' : ''">
    <input class="default-input" :value='getValue' @input="updateValue" :placeholder="placeholder" @blur="blur"
      @focus="focus" />
    <div v-if="isFocus" class="options">
      <div v-for="option in options" :key="option[keyOption]" @click="handleSelect(option)">
        <slot name="option" :option="optionsSelect">
          <div class="option" :class="selected && selected[selected] == option[selected] ? 'selected' : ''">{{
            option[keyValueSelect]
            }}</div>
        </slot>
      </div>
    </div>
  </div>
</template>
<script lang="ts">
import { ref } from 'vue';
import { ResultValidateBase } from '../../models';
export default {
  name: 'ECombobox',
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
      default: (value: string) => { return value }
    },
    options: {
      type: Array,
      required: true
    },
    selected: {
      type: Object,
      required: true
    },
    keyOption: {
      type: String,
      required: true
    },
    keyValueSelect: {
      type: String,
      required: true
    },
    keySearch: {
      type: Array,
      required: false,
      default: () => []
    },
    handleFilter: {
      type: Function,
      required: false,
    }
  },
  computed: {
    getValue() {
      return this.selected && this.selected[this.keyValueSelect] ? this.selected && this.selected[this.keyValueSelect] : ''
    }
  },
  emits: [
    'update:inputValue', 'select'
  ],
  created() {
    this.optionsSelect = this.options
  },
  setup(props, ctx) {
    const value = ref('');
    const isFocus = ref(false)
    const optionsSelect = ref([])
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
    async function updateValue(event) {
      let resultValidate = validateRule(event.target.value)
      if (props.handleFilter) {
        this.optionsSelect = await props.handleFilter(event.target.value)
      } else {
        this.optionsSelect = handleFilterDefault(event.target.value)
      }
      ctx.emit('update:inputValue', event.target.value)
    }
    function focus() {
      isFocus.value = true
    }
    function blur() {
      isFocus.value = false
    }
    function handleSelect(option) {
      ctx.emit('select', option)
    }
    function handleFilterDefault(valueSearch) {
      let optionsSelect = props.options.filter(option => {
        for (let index = 0; index < props.keySearch.length; index++) {
          let keySearch = props.keySearch[index]
          if (option && option[keySearch].includes(valueSearch)) {
            return true
          }
        }
        return false
      })
      return optionsSelect
    }
    return {
      value, validateRule, updateValue, isFocus, blur, focus, handleSelect, optionsSelect, handleFilterDefault
    };
  },
};
</script>
<style src="../../css/e-control.scss" lang="scss"></style>
