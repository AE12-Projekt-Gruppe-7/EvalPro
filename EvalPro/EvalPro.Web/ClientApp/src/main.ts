import './main.css'

import { createApp } from 'vue'
import App from './App.vue'
import 'primeicons/primeicons.css'
import PrimeVue from 'primevue/config'
import type { Plugin } from 'vue'
import router from './router/index.js'
import Aura from '@primeuix/themes/aura';

const app = createApp(App)

app.use(router)
app.use(PrimeVue as unknown as Plugin, {
  theme: {
  preset: Aura

  }
})
app.mount('#app')