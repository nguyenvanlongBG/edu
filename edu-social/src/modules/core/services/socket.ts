import { reactive } from 'vue';
import { io } from 'socket.io-client';

export const state = reactive({
  connected: false,
  fooEvents: [],
  barEvents: [],
});

// "undefined" means the URL will be computed from the `window.location` object
const URL = 'http://localhost:8000';

export const socket = io(URL, {
  autoConnect: false,
});

socket.on('connect', () => {
  state.connected = true;
});

socket.on('disconnect', () => {
  state.connected = false;
});
socket.on('updatedName', (name: string) => {
  console.log(name);
});
