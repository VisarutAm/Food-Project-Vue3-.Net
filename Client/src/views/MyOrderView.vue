<template >
  <div class="my-orders">
    <Navbar />
    <div class="mt-32 mx-[100px]">
      <h2 class="font-bold text-3xl mb-5">My Orders</h2>
      <div v-if="orders.length > 0">
        <div
          v-for="(order, index) in orders"
          :key="index"
          class="my-orders-order border-4 border-primary mb-5 grid grid-cols-4  items-center pr-6 pl-20"
        >
        <img src="../assets/parcel_icon.png" class="w-20 h-20 object-contain"/>
          <p class="font-medium">Items: {{ order.items.length }}</p>
          <p class="font-medium">Total: {{ order.amount }}</p>
          <p class="py-4 bg-red-200 text-center font-bold">Status: {{ order.status }}</p>
        </div>
      </div>
      <div v-else>
        <p>No orders found.</p>
      </div>
    </div>
  </div>
</template>
<script setup>
import Navbar from "../components/Navbar.vue";
import { ref, onMounted } from "vue";
import axios from "axios";
import { useAuthStore } from "../stores/useAuthStore";

const authStore = useAuthStore();
const myorders = ref([]);
const email = authStore.response.email;

onMounted(async () => {
  try {
    const response = await axios.get(
      `https://localhost:7089/api/order/email/${email}`
    );
    myorders.value = response.data;
  } catch (error) {
    console.error("Error fetching orders:", error);
  }
});

onMounted(() => {
  console.log("🧑‍💻 Email:");
});
</script>


