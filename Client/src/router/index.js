import { createRouter, createWebHistory } from 'vue-router'
import HomeView from '../views/HomeView.vue'
import LogInView from '@/views/LogInView.vue'
import AddFoodsView from '@/views/Admin/AddFoodsView.vue'
import ListMenu from '@/components/Admin/ListMenu.vue'
import CartView  from '../views/CartView.vue'

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/',
      name: 'home',
      component: HomeView,
    },
    {
      path: '/login',
      name: 'login',
      component: LogInView,
    },
    {
      path: '/admin',
      name: 'admin',
      component: AddFoodsView,
    },
    {
      path: '/list',
      name: 'ListMenu',
      component: ListMenu,
    },
    {
      path: '/cart',
      name: 'Cart',
      component: CartView,
    },
  ],
})

export default router
 // {
    //   path: '/about',
    //   name: 'about',
    //   // route level code-splitting
    //   // this generates a separate chunk (About.[hash].js) for this route
    //   // which is lazy-loaded when the route is visited.
    //   component: () => import('../views/AboutView.vue'),
    // },
 

//     import { useAuthStore } from '../stores/useAuthStore'

// const requireAuth = (to, from, next) => {
//   const authStore = useAuthStore()
//   if (!authStore.token) {
//     next('/login')
//   } else {
//     next()
//   }
// }

// const requireAdmin = (to, from, next) => {
//   const authStore = useAuthStore()
//   if (authStore.role !== 'Admin') {
//     next('/') // หรือ redirect หน้า 403
//   } else {
//     next()
//   }
// }

// const routes = [
//   {
//     path: '/dashboard',
//     component: () => import('../views/Dashboard.vue'),
//     beforeEnter: requireAuth,
//   },
//   {
//     path: '/admin',
//     component: () => import('../views/Admin.vue'),
//     beforeEnter: requireAdmin,
//   },
// ]