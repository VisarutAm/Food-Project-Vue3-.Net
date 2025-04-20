// stores/useMenuStore.js
import { defineStore } from 'pinia'
import axios from 'axios'

export const useMenuStore = defineStore('menu', {
  state: () => ({
    menuList: [],
    loading: false,
    error: null
  }),

  actions: {
    async fetchMenu() {
      this.loading = true
      this.error = null
      try {
        const res = await axios.get('http://localhost:5203/api/food/allmenu')
        this.menuList = res.data
        //console.log(res.data)
      } catch (err) {
        this.error = err.message || 'Failed to fetch menu'
        console.error('Fetch menu error:', err)
      } finally {
        this.loading = false
      }
    }
  }
})
