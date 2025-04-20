<template >
    <div>
        <h1>Explore our menu</h1>
        <p>Choose from a diverse menu featuring a delectable array of dishes. Our mission is to satisfy your cravings and elevate your dining experience, one delicious meal at a time.</p>
        <hr class="border-4 border-primary m-5 rounded-full"/>
        <div class="food-display flex flex-wrap justify-center gap-10 my-10">
            <CardProduct v-for="item in menuList" :key="item._id" :item="{ ...item }"/>
        </div>
    </div>
</template>
<script setup>
import CardProduct from '../components/UI/CardProduct.vue'
import axios from "axios";
import { storeToRefs } from "pinia";
import { onMounted,watch } from "vue";
import { useLink } from 'vue-router';
import { useMenuStore } from '../stores/useMenuStore';
import { useOrderStore } from '../stores/useOrderStore'

const orderStore = useOrderStore()
const menuStore = useMenuStore();
const { fetchMenu } = menuStore;
const {menuList} = storeToRefs(menuStore);

onMounted(async () => {
    console.log("menuList:", menuList.value)

    try {
        await fetchMenu();
        console.log("✅ เมนูที่โหลดมา:", menuList.value);
    } catch (error) {
        console.error("เกิดข้อผิดพลาดในการโหลดเมนู:", error)
    }
})

watch(
  () => orderStore.orderData,
  (newVal) => {
    console.log("orderData changed", newVal)
    orderStore.calculateAmount()
  },
  { deep: true })
</script>
