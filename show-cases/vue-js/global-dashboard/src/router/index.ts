import { createRouter, createWebHashHistory, RouteRecordRaw } from 'vue-router'
import { auth } from './security';
import MainView from '../views/MainView.vue'
import HivesView from '../views/HivesView.vue'
import { security } from "@/services/security";

const routes: Array<RouteRecordRaw> = [
  {
    path: '/',
    name: 'main',
    component: MainView
  },
  {
    path: '/about',
    name: 'about',
    // route level code-splitting
    // this generates a separate chunk (about.[hash].js) for this route
    // which is lazy-loaded when the route is visited.
    component: () => import(/* webpackChunkName: "about" */ '../views/AboutView.vue')
  },
  {
    path: '/hives',
    name: 'hives',
    component: HivesView,
  }
]

const router = createRouter({
  history: createWebHashHistory(),
  routes
})

router.beforeEach(async () => {
  if (!(await auth.confirm())) {
    security.checkAuth();
  }
  else if (window.location.href !== `${window.location.origin}${window.location.pathname}#/`) {
    window.location.href = `${window.location.origin}${window.location.pathname}`
  }
})

export default router
