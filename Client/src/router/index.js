import { createRouter, createWebHistory } from 'vue-router'
import HomeView from '../views/HomeView.vue'
import LogInView from '@/views/LogInView.vue'
import AddFoodsView from '@/views/Admin/AddFoodsView.vue'
import ListMenu from '@/components/Admin/ListMenu.vue'
import CartView  from '../views/CartView.vue'
import SuccessView from '@/views/SuccessView.vue'
import MyOrderView from '@/views/MyOrderView.vue'
import OrdersAdminView from '@/views/Admin/OrdersAdminView.vue'
import { useAuthStore } from '@/stores/useAuthStore'

const requireAuth = (to, from, next) => {
  const authStore = useAuthStore()
  if (!authStore.token) {
    next('/login')
  } else {
    next()
  }
}

const requireAdmin = (to, from, next) => {
  const authStore = useAuthStore()
      if (authStore.token && authStore.response.role === 'Admin') {
    next() 
  } else {
    next('/')
  }
}

const router = createRouter({
  history: createWebHistory(),
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
      path: '/cart',
      name: 'Cart',
      component: CartView,
      beforeEnter: requireAuth,
      
    },
    {
      path: "/success",
      name: "Success",
      component: SuccessView,   
      beforeEnter: requireAuth,  
     
    },
    {
      path: "/myorder",
      name: "Myorder",
      component: MyOrderView,
      beforeEnter: requireAuth,  
    },
    {
      path: '/admin',
      name: 'admin',
      component: AddFoodsView,
      beforeEnter: requireAdmin,
    },
    {
      path: '/list',
      name: 'ListMenu',
      component: ListMenu,
      beforeEnter: requireAdmin,
    },  
    {
      path: "/orders",
      name: "OrdersAdmin",
      component: OrdersAdminView,
      beforeEnter: requireAdmin 
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