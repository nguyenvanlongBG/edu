import Cookies from 'universal-cookie';
const accessToken = 'accessTokenEduNetwork';
export function getAccessToken() {
  return new Cookies().get(accessToken);
}
export function setAccessToken(token = '', options = {}) {
  return new Cookies().set(accessToken, token, options);
}
export function removeAccessToken() {
  if (getAccessToken()) {
    return new Cookies().remove(accessToken);
  }
}
