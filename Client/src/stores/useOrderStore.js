import { defineStore } from "pinia";
import axios from "axios";
// import { useAuthStore } from "./useAuthStore";

// const authStore = useAuthStore();

export const useOrderStore = defineStore("Orders", {
  state: () => ({
    orderData: {
      items: [],
      amount: 0,
      status: "Food Processing",
      date: new Date().toISOString(),
      payment: "Pending",
      email:"",
    },
  }),
  persist: {
    enabled: true,
    strategies: [
      {
        key: "Orders", // ชื่อที่ใช้เก็บใน localStorage
      },
    ],
  },
  actions: {
    setEmail(email) {
      this.orderData.email = email;
    },
    addItem(item) {
      const newItem = { ...item, quantity: item.quantity ?? 1 };
      this.orderData.items.push(newItem);
      this.calculateAmount();
    },

    removeItem(index) {
      this.orderData.items.splice(index, 1);
      this.calculateAmount();
    },

    calculateAmount() {
      this.orderData.amount = this.orderData.items.reduce((sum, item) => {
        return sum + item.price * item.quantity;
      }, 0);
    },

    async submitOrder() {
      console.log(">> submitOrder CALLED <<");
      try {
        const { useAuthStore } = await import("./useAuthStore");
        const authStore = useAuthStore(); // ✅ เรียกตรงนี้
        debugger; // ⏸ หยุดตรงนี้ให้ inspect ได้
        console.log("Auth Store:", authStore);
        console.log("Email from Auth Store:", authStore.email);
        alert("Email: " + authStore.email);

        this.orderData.email = authStore.email;

        const response = await axios.post(
          "https://localhost:70899/กกกapi/orders",
          this.orderData
        );
        this.orderData.status = "submitted";
        this.orderData.date = new Date().toISOString();
        //this.orderHistory.push({ ...this.orderData }) // บันทึกลง history
        //  this.orderData.email = authStore.email;
        this.resetOrder();
        return response.data;
      } catch (error) {
        console.error("Error submitting order:", error);
        this.orderData.status = "failed";
      }
    },
  },
});
