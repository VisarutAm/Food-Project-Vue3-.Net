<template >
  <div class="my-orders">
    <NavbarAdmin />
    <div class="mt-32 mx-[100px]">
      <h2 class="font-bold text-3xl mb-5">My Orders</h2>
      <div v-if="orders.length > 0">
        <div
          v-for="(order, index) in orders"
          :key="index"
          class="my-orders-order border-4 border-primary mb-5 grid grid-cols-4 items-center pr-6 pl-20 max-md:flex max-md:flex-col max-md:pl-6"
        >
          <img
            src="../../assets/parcel_icon.png"
            class="w-20 h-20 object-contain"
          />
          <p class="font-medium">Items: {{ order.items.length }}</p>
          <p class="font-medium">Total: {{ order.amount }}</p>
          <!-- <p class="py-4 bg-red-200 text-center font-bold">Status: {{ order.status }}</p> -->
          <!-- 🔁 Select สำหรับเปลี่ยน Status -->
          <select
            class="p-2 border rounded bg-white"
            :value="order.status"
            @change="handleStatusChange($event, order.id)"
            :style="{
              backgroundColor: getStatusColor(order.status),
            }"
          >
            <option value="Food Processing" style="background-color: #fef3c7">
              Food Processing
            </option>
            <option value="Out for delivery" style="background-color: #bfdbfe">
              Out for delivery
            </option>
            <option value="Delivered" style="background-color: #bbf7d0">
              Delivered
            </option>
          </select>
        </div>
      </div>
      <div v-else>
        <p>No orders found.</p>
      </div>
    </div>
  </div>
</template>
<script setup>
import NavbarAdmin from "../../components/Admin/NavbarAdmin.vue";
import { onMounted, ref } from "vue";
import axios from "axios";

const orders = ref([]);

onMounted(async () => {
  await fetchOrders();
});

const fetchOrders = async () => {
  try {
    const response = await axios.get(`${import.meta.env.VITE_API_URL}/api/order/all`);
    orders.value = response.data;
    //console.log("response.data", response.data[0].id);
  } catch (error) {
    console.error("Error fetching orders:", error);
  }
};
const handleStatusChange = (event, orderId) => {
  statusHandler(event, orderId);
  //console.log(" orderId:", orderId);
};

const statusHandler = async (event, orderId) => {
  //console.log("✅ statusHandler called"); // 🔍 debug
  const newStatus = event.target.value;
  try {
    const response = await axios.put(
      `${import.meta.env.VITE_API_URL}/api/order/status/${orderId}`,
    //   `"${newStatus}"`, // 👈 อย่าลืมส่งแบบ object ที่ API รองรับ
    { newStatus }, // 👈 อย่าลืมส่งแบบ object ที่ API รองรับ
      {
        headers: {
          "Content-Type": "application/json",
        },
        withCredentials: true,
      }
    );
    console.log("✅ Status updated:", response.data);

    // อัปเดต status ใน local state เพื่อให้ UI เปลี่ยนทันที
    const targetOrder = orders.value.find((order) => order.id === orderId);
    if (targetOrder) targetOrder.status = newStatus;
  } catch (error) {
    console.error("❌ Failed to update status:", error);
  }
};

const getStatusColor = (status) => {
  switch (status) {
    case "Food Processing":
      return "#fef3c7"; // สีเหลืองอ่อน
    case "Out for delivery":
      return "#bfdbfe"; // สีฟ้าอ่อน
    case "Delivered":
      return "#bbf7d0"; // สีเขียวอ่อน
    default:
      return "#ffffff"; // ขาว
  }
}
</script>
