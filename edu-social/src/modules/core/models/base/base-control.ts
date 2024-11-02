import commonFunction from '../../commons/CommonFunction';
import { IBaseControl } from './i-base-control';

export class BaseControl implements IBaseControl {
  constructor(control?: IBaseControl) {
    if (control != null && control != undefined) {
      this.id = control.id;
      commonFunction.assignProperties(this, control);
    } else {
      this.id = commonFunction.generateID();
    }
  }
  id: string;
  readonly = true;
}
