import './style.css'

import { createApp } from 'vue'
import { createPinia } from 'pinia'
import piniaPluginPersistedstate from 'pinia-plugin-persistedstate'
import App from './App.vue'
import router from './router'
import Toast from 'vue-toastification';
import 'vue-toastification/dist/index.css';

const app = createApp(App)
const pinia = createPinia() // ✅ สร้าง instance แล้วเก็บไว้ในตัวแปร
pinia.use(piniaPluginPersistedstate) // ✅ ให้ pinia ใช้ persistedstate
app.use(pinia)

app.use(router)
app.use(Toast, {
    // กำหนดค่าการใช้งานเช่น Timeout, Position, etc.
    position: "top-center",
    timeout: 3000,
  });

app.mount('#app')

