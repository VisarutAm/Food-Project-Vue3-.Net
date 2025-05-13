<template>
  <div
    class="flex flex-col items-center justify-center min-h-screen bg-green-50 text-center px-4"
  >
    <h1 class="text-4xl font-bold text-green-600 mb-4">🎉 ชำระเงินสำเร็จ!</h1>
    <p class="text-lg text-gray-700 mb-6">ขอบคุณสำหรับการสั่งซื้อของคุณ</p>

    <router-link
      to="/"
      class="inline-block bg-green-600 hover:bg-green-700 text-white px-6 py-3 rounded-full shadow"
    >
      กลับไปหน้าแรก
    </router-link>
  </div>
</template>
  
  <script setup>
import { onMounted } from "vue";
import axios from "axios";
import { useOrderStore } from "@/stores/useOrderStore";

const orderStore = useOrderStore();

onMounted(async () => {
//   try {
//     const { useAuthStore } = await import("@/stores/useAuthStore");
//     const authStore = useAuthStore();
//     orderStore.orderData.email = authStore.response.email;
//     console.log("email:", authStore.response.email);
//     const plainOrderData = JSON.parse(JSON.stringify(orderStore.orderData));
//     console.log("Calling Stripe API");
//     console.log("🧾 orderData ที่ส่ง:", plainOrderData);
//     const response = await axios.post(
//       "https://localhost:7089/api/order/payment",
//       plainOrderData,
//       {
//         withCredentials: true,
//       }
//     );
//     console.log("response", response);
//     const url = response.data.url;
//     console.log("response.data", response.data); // ดูว่ามีอะไรกลับมาบ้าง
//     console.log("URL returned from API:", url);
//     window.location.href = url; // Redirect to Stripe checkout page
//   } catch (err) {
//     console.error("เกิดข้อผิดพลาดในการชำระเงิน:", err);
//   }
  try {
    const { useAuthStore } = await import("@/stores/useAuthStore");
    const authStore = useAuthStore();
    orderStore.orderData.email = authStore.response.email;
    console.log("email:", authStore.response.email);
    const plainOrderData = JSON.parse(JSON.stringify(orderStore.orderData));
    console.log("Calling Stripe API");
    console.log("🧾 orderData ที่ส่ง:", plainOrderData);
    debugger; // ⏸ หยุดตรงนี้ให้ inspect ได้
    const response = await axios.post(
      `${import.meta.env.VITE_API_URL}/api/order`,
      plainOrderData,
      {
        withCredentials: true,
      }
    );
    console.log("✅ Order saved successfully", response.data);
    console.log("response", response);
    orderStore.$reset();
    localStorage.removeItem("Orders"); // สำรองลบด้วย (ป้องกัน persist bug)
    const url = response.data.url;
    console.log("response.data", response.data); // ดูว่ามีอะไรกลับมาบ้าง
    console.log("URL returned from API:", url);
    orderStore.resetOrder();
    window.location.href = url; // Redirect to Stripe checkout page
  } catch (error) {
    console.error("❌ Failed to save order:", error);
  }
});
</script>
  
  