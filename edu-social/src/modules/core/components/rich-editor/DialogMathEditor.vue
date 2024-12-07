<template>
  <q-dialog v-model="showDialog" ref="dialogRef" @hide="onDialogHide">
    <q-card class="bg-teal" style="width: 300px">
      <q-card-section>
        <div class="text-h6">{{ title }}</div>
      </q-card-section>

      <q-card-section>
        <math-field
          :macros="macros"
          ref="mathLive"
          virtual-keyboard-mode="manual"
          v-model:value="formula"
          @input="handleInput"
        ></math-field>
      </q-card-section>

      <q-card-actions>
        <q-btn flat label="OK" @click="emitMath" />
      </q-card-actions>
    </q-card>
  </q-dialog>
</template>
<script lang="ts">
import { defineComponent, ref } from 'vue';
import { Mathfield } from 'mathlive';
import { useDialogPluginComponent } from 'quasar';

export default defineComponent({
  name: 'DialogMathEditor',
  emits: ['update:formula', ...useDialogPluginComponent.emits],
  components: {
    Mathfield,
  },
  props: {
    formulaProp: {
      type: String,
      required: true,
      default: '',
    },
    title: {
      type: String,
      required: false,
      default: '',
    },
  },
  created() {
    this.formula = this.formulaProp;
  },
  setup(props, ctx) {
    const showDialog = ref(true);
    const macros = {
      smallfrac: {
        args: 2,
        def: '{}^{#1}\\!\\!/\\!{}_{#2}',
        captureSelection: false,
      },
      genfrac: {
        args: 6,
        def: '\\binom{#4}{#3}',
      },
      dots: {
        args: 0,
        def: '...',
        captureSelection: false,
      },
    };
    const formula = ref(
      '{\\left(1+x\\right)}^{n}=1+\\frac{nx}{1!}+\\frac{n\\left(n-1\\right){x}^{2}}{2!}+\\dots'
    );
    const { dialogRef, onDialogHide, onDialogOK, onDialogCancel } =
      useDialogPluginComponent();
    function handleInput(e: any) {
      if (e && e.target) {
        formula.value = e.target.value;
        console.log(formula);
      }
    }
    function emitMath() {
      onDialogOK(formula.value);
    }
    return {
      dialogRef,
      onDialogHide,
      onDialogOK,
      onDialogCancel,
      macros,
      formula,
      handleInput,
      emitMath,
      showDialog,
    };
  },
});
</script>
