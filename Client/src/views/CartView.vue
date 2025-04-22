<!-- <template >
  <div>
    <Navbar />
    <div class="orders m-32">
      <div
        v-for="item in orderStore.orderData.items"
        :key="item.id"
        class="grid grid-cols-4 items-center border-4 rounded-xl my-5 bg-white border-primary gap-4 max-sm:flex max-sm:flex-col"
      >
        <iframe
          :src="generateDriveUrl(item.driveUrl)"
          style="
            width: 100px;
            height: 100px;
            border: none;
            overflow: hidden;
            border-radius: 9px;
          "
          allow="autoplay"
          scrolling="no"
        ></iframe>
        <p class="font-bold text-lg ">{{ item.name }}</p>
        <p class="font-bold text-lg ">{{ item.price }} ฿</p>
        <div class="flex flex-row justify-evenly">
          <button
            class="bg-green-500 text-white px-3 py-1 rounded-xl"
            @click="addToOrder(item)"
          >
            ➕
          </button>
          <p class="font-bold text-lg">{{ item.quantity }}</p>
          <button
            class="bg-red-500 text-white px-3 py-1 rounded-xl"
            @click="removeFromOrder(item)"
          >
            ➖
          </button>
        </div>
      </div>
      <div class="catr-total m-10 w-[50%]">
          <h3 class="font-bold text-2xl text-primary">Cart Total</h3>
          <hr class="border-2 my-5 border-gray-600 "/>
          <div class="flex justify-between">
          <span class="text-gray-600 font-medium text-xl">Total </span><span class="text-gray-600 font-medium text-xl"> ฿ {{ orderStore.orderData.amount }}</span>
        </div>
        <button  @click="proceedToPayment" class="bg-orange-500 rounded-2xl p-5 m-5 text-white font-medium">Proceed To Payment</button>
      </div>
    </div>
  </div>
</template>
<script setup>
import Navbar from "../components/Navbar.vue";
import { useOrderStore } from "../stores/useOrderStore";
import { onMounted } from "vue";
import axios from "axios";

const orderStore = useOrderStore();

const generateDriveUrl = (driveUrl) => {
  let match = driveUrl.match(/(?:id=)([a-zA-Z0-9_-]+)/);
  if (match) {
    return `https://drive.google.com/file/d/${match[1]}/preview`;
  } else {
    console.log("ไม่พบ id ใน URL");
    return "";
  }
};

onMounted(() => console.log("order", orderStore.orderData));

const addToOrder = (item) => {
  const index = orderStore.orderData.items.findIndex((i) => i.id === item.id);
  if (index !== -1) {
    orderStore.orderData.items[index].quantity += 1;
  } else {
    const newItem = { ...item, quantity: 1 };
    orderStore.addItem(newItem);
  }
  orderStore.calculateAmount();
};

const removeFromOrder = (item) => {
  const index = orderStore.orderData.items.findIndex(
    (i) => i.id === item.id
  );

  if (index !== -1) {
    const item = orderStore.orderData.items[index];
    if (item.quantity > 1) {
      item.quantity -= 1;
    } else {
      orderStore.removeItem(index);
    }
    orderStore.calculateAmount();
  }
};

const proceedToPayment = async () => {
    try {
      console.log(Callllll)
    const response = await axios.post("http://localhost:5199/api/payment/create-checkout-session", orderStore.orderData);
    const url = response.data.url;
    window.location.href = url; // 👉 redirect ไปยัง Stripe
  } catch (err) {
    console.error("เกิดข้อผิดพลาดในการชำระเงิน:", err);
  }
};
</script> -->
<template>
  <div>
    <Navbar />
    <div class="orders m-32">
      <div
        v-for="item in orderStore.orderData.items"
        :key="item.id"
        class="grid grid-cols-4 items-center border-4 rounded-xl my-5 bg-white border-primary gap-4 max-sm:flex max-sm:flex-col"
      >
        <iframe
          :src="generateDriveUrl(item.driveUrl)"
          style="
            width: 100px;
            height: 100px;
            border: none;
            overflow: hidden;
            border-radius: 9px;
          "
          allow="autoplay"
          scrolling="no"
        ></iframe>
        <p class="font-bold text-lg">{{ item.name }}</p>
        <p class="font-bold text-lg">{{ item.price }} ฿</p>
        <div class="flex flex-row justify-evenly">
          <button
            class="bg-green-500 text-white px-3 py-1 rounded-xl"
            @click="addToOrder(item)"
          >
            ➕
          </button>
          <p class="font-bold text-lg">{{ item.quantity }}</p>
          <button
            class="bg-red-500 text-white px-3 py-1 rounded-xl"
            @click="removeFromOrder(item)"
          >
            ➖
          </button>
        </div>
      </div>
      <div class="cart-total m-10 w-[50%]">
        <h3 class="font-bold text-2xl text-primary">Cart Total</h3>
        <hr class="border-2 my-5 border-gray-600" />
        <div class="flex justify-between">
          <span class="text-gray-600 font-medium text-xl">Total</span>
          <span class="text-gray-600 font-medium text-xl">
            ฿ {{ orderStore.orderData.amount }}</span
          >
        </div>
        <button
          @click="proceedToPayment"
          class="bg-orange-500 rounded-2xl p-5 m-5 text-white font-medium"
        >
          Proceed To Payment
        </button>
      </div>
    </div>
  </div>
</template>
  
  <script setup>
import Navbar from "../components/Navbar.vue";
import { useOrderStore } from "../stores/useOrderStore";
import { onMounted } from "vue";
import axios from "axios";
import { useAuthStore } from "../stores/useAuthStore";

const orderStore = useOrderStore();
const authStore = useAuthStore();

const generateDriveUrl = (driveUrl) => {
  let match = driveUrl.match(/(?:id=)([a-zA-Z0-9_-]+)/);
  if (match) {
    return `https://drive.google.com/file/d/${match[1]}/preview`;
  } else {
    console.log("ไม่พบ id ใน URL");
    return "";
  }
};

onMounted(() => console.log("order", orderStore.orderData));

const addToOrder = (item) => {
  const index = orderStore.orderData.items.findIndex((i) => i.id === item.id);
  if (index !== -1) {
    orderStore.orderData.items[index].quantity += 1;
  } else {
    const newItem = { ...item, quantity: 1 };
    orderStore.addItem(newItem);
  }
  orderStore.calculateAmount();
};

const removeFromOrder = (item) => {
  const index = orderStore.orderData.items.findIndex((i) => i.id === item.id);
  if (index !== -1) {
    const item = orderStore.orderData.items[index];
    if (item.quantity > 1) {
      item.quantity -= 1;
    } else {
      orderStore.removeItem(index);
    }
    orderStore.calculateAmount();
  }
};

// Handle Proceed to Payment
const proceedToPayment = async () => {
  try {
    const { useAuthStore } = await import("@/stores/useAuthStore");
    const authStore = useAuthStore();
    orderStore.orderData.email = authStore.response.email;
    console.log("email:",authStore.response.email);
    const plainOrderData = JSON.parse(JSON.stringify(orderStore.orderData));
    console.log("Calling Stripe API");
    console.log("🧾 orderData ที่ส่ง:", plainOrderData);
    const response = await axios.post(
      "https://localhost:7089/api/order/payment",
      plainOrderData,
      {
        withCredentials: true,
      }
    );
    console.log("response", response);
    const url = response.data.url;
    console.log("response.data", response.data); // ดูว่ามีอะไรกลับมาบ้าง
    console.log("URL returned from API:", url);
    window.location.href = url; // Redirect to Stripe checkout page
  } catch (err) {
    console.error("เกิดข้อผิดพลาดในการชำระเงิน:", err);
  }
};
</script>
  