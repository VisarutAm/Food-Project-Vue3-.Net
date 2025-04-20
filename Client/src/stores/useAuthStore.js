import { defineStore } from 'pinia'

export const useAuthStore = defineStore('auth', {
  state: () => ({
    token: null,
    user: null, // เก็บ user info ที่ backend ส่งกลับมา
    response:null,
  }),
  actions: {
    setToken(token) {
      this.token = token
      
    },
    setUser(user) {
      this.user = user
    },
    setResponse(response) {
      this.response = response
    },
    logout() {
      this.token = null
      this.user = null
      localStorage.removeItem('token')
      
    }
  },
  persist: true, // ทำให้ store อยู่แม้ reload (ต้องใช้ plugin pinia-plugin-persistedstate)
})
