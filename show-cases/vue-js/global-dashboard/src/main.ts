import { createApp } from 'vue'
import App from './App.vue'
import './registerServiceWorker'
import router from './router'
import store from './store'
import defaults from './startup/defaults'
import 'mdb-vue-ui-kit/css/mdb.min.css';

createApp(App)
    .use(store)
    .use(router)
    .use(defaults)
    .mount('#app')
