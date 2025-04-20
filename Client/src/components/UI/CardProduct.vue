<template>
    <div class="w-[205px] h-[350px] border-4 border-primary rounded-xl">
        <iframe
              :src="generateDriveUrl(item.driveUrl)"
              style="
                width: 200px;
                height: 200px;
                border: none;
                overflow: hidden;
                border-radius: 9px;
              "
              allow="autoplay"
              scrolling="no"
            ></iframe>
            <div class="description m-4 ">               
            <p class="font-bold text-xl text-primary">{{ item.name }}</p>
            <p class="font-medium">{{ item.description }}</p>
            <p class="font-medium text-3xl m-2 text-orange-500">฿ {{ item.price }}</p>
            
            <img src="../../assets/rating_starts.png"/>
            <!-- 🔘 ปุ่ม Add / Remove -->
      <div class="flex justify-between items-center mt-4">
        <button class="bg-green-500 text-white px-3 py-1 rounded-xl" @click="addToOrder">➕</button>
        <p class="font-bold text-lg text-primary">
            <!-- {{
    orderStore.orderData.items.find(i => i._id === item._id)?.quantity || 0
  }} -->
  {{ orderStore.orderData.items.find(i => i.id === item.id)?.quantity || 0 }}
        </p>
        <button class="bg-red-500 text-white px-3 py-1 rounded-xl" @click="removeFromOrder">➖</button>
      </div>
        </div>
        
    </div>
</template>
<script setup>
import {useOrderStore} from '../../stores/useOrderStore'
import { computed} from 'vue'

const orderStore = useOrderStore()

const props = defineProps({
  item: {
    type: Object,
    required: true
  }
});

// ➕ เพิ่มเมนูลงออเดอร์
// const addToOrder = () => {
//   const index = orderStore.orderData.items.findIndex(i => i._id === props.item._id)
//   if (index !== -1) {
//     // ใช้ Vue.set-like ด้วย splice เพื่อให้ reactive
//     const updatedItem = { ...orderStore.orderData.items[index] }
//     updatedItem.quantity += 1
//     orderStore.orderData.items.splice(index, 1, updatedItem)
//     orderStore.calculateAmount()
//   } else {
//     orderStore.addItem({ ...props.item, quantity: 1 })
//   }
// }
const addToOrder = () => {
  const index = orderStore.orderData.items.findIndex(i => i.id === props.item.id)

  if (index !== -1) {
    // 👉 ใช้แบบนี้แทน structuredClone
    orderStore.orderData.items[index].quantity += 1
  } else {
    // 👉 สร้างใหม่แบบนี้ก็พอ ไม่ต้อง structuredClone
    const newItem = { ...props.item, quantity: 1 }
    orderStore.addItem(newItem)
  }

  orderStore.calculateAmount()
}




// ➖ ลบเมนูออก หรือ ลด quantity
// const removeFromOrder = () => {
//   const index = orderStore.orderData.items.findIndex(i => i._id === props.item._id)
//   if (index !== -1) {
//     const updatedItem = { ...orderStore.orderData.items[index] }
//     if (updatedItem.quantity > 1) {
//       updatedItem.quantity -= 1
//       orderStore.orderData.items.splice(index, 1, updatedItem)
//       orderStore.calculateAmount()
//     } else {
//       orderStore.removeItem(index)
//     }
//   }
// }
const removeFromOrder = () => {
  const index = orderStore.orderData.items.findIndex(i => i.id === props.item.id)

  if (index !== -1) {
    const item = orderStore.orderData.items[index]
    if (item.quantity > 1) {
      item.quantity -= 1
    } else {
      orderStore.removeItem(index)
    }
    orderStore.calculateAmount()
  }
}



const generateDriveUrl = (driveUrl) => {
  let match = driveUrl.match(/(?:id=)([a-zA-Z0-9_-]+)/);
  if (match) {
    return `https://drive.google.com/file/d/${match[1]}/preview`;
  } else {
    console.log("ไม่พบ id ใน URL");
    return "";
  }
};

const quantity = computed(() => {
  const found = orderStore.orderData.items.find(i => i.id === props.item.id)
  return found?.quantity || 0
})
// const quantity = computed(() => {
//   const match = orderStore.orderData.items.find(i => i._id === props.item._id)
//   return match?.quantity || 0
// })
</script>
<style lang="">
    
</style>