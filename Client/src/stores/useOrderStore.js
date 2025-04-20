import { defineStore } from "pinia";
import axios from "axios";

export const useOrderStore = defineStore("Orders", {
  state: () => ({
    orderData: {
      items: [],
      amount: 0,
      status: "Food Processing",
      date: "",
      payment: "",
    },
  }),
  actions: {
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
      try {
        const response = await axios.post(
          "http://localhost:5203/api/orders",
          this.orderData
        );
        this.orderData.status = "submitted";
        this.orderData.date = new Date().toISOString();
        //this.orderHistory.push({ ...this.orderData }) // บันทึกลง history
        this.resetOrder();
        return response.data;
      } catch (error) {
        console.error("Error submitting order:", error);
        this.orderData.status = "failed";
      }
    },
  },
});
