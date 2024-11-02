import { BaseControl } from '../base/base-control';
import { IEditorControl } from './i-editor-control';

export class EditorControl extends BaseControl {
  constructor(control?: IEditorControl) {
    super();
    if (control) {
      this.value = control.value;
    } else {
      this.value = '';
    }
  }
  value: string;
}
