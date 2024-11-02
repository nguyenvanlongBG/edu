import { v4 as uuidv4 } from 'uuid';
class CommonFunction {
  generateID() {
    return uuidv4();
  }
  convertInterfaceToInstance(data: Record<string, any>, root: any) {
    if (root) {
    }
  }
  assignProperties<T>(
    target: Record<string, any>,
    source: Record<string, any>
  ): void {
    Object.keys(source).forEach((key) => {
      if (key in target) {
        (target as any)[key] = (source as any)[key];
      }
    });
  }
}
const commonFunction = new CommonFunction();
export default commonFunction;
