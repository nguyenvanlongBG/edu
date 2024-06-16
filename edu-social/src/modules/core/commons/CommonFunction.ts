import { v4 as uuidv4 } from 'uuid';
class CommonFunction {
  generateID() {
    return uuidv4();
  }
}
const commonFunction = new CommonFunction();
export default commonFunction;
