export function currentUrlWithPortNumber(port: number) {
  const protocol = window.location.protocol;
  const hostname = window.location.hostname;
  const url = `${protocol}//${hostname}:${port}`;
  return url;
}
