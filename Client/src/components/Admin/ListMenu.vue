<script setup>
import NavbarAdmin from "./NavbarAdmin.vue";
import { onMounted, watch, ref, computed } from "vue";
import { useMenuStore } from "../../stores/useMenuStore";
import { storeToRefs } from "pinia";
import axios from "axios";
import { toRaw } from 'vue';
import { useToast } from 'vue-toastification';

const menuStore = useMenuStore();
const { fetchMenu } = menuStore;
const { menuList, loading, error } = storeToRefs(menuStore);
const toast = useToast(); 

onMounted(async () => {
  await fetchMenu();
 // console.log("✅ เมนูที่โหลดมา:", menuList.value);
});


// watch(menuList, (val) => {
//   console.log("📦 อัปเดตเมนู:", val);
// });

// ฟังก์ชั่นสำหรับสร้าง URL ใหม่จาก driveUrl
const generateDriveUrl = (driveUrl) => {
  let match = driveUrl.match(/(?:id=)([a-zA-Z0-9_-]+)/);
  if (match) {
    return `https://drive.google.com/file/d/${match[1]}/preview`;
  } else {
    console.log("ไม่พบ id ใน URL");
    return "";
  }
};

const deleteItem = async (id) => {
  let clonedMenuList = structuredClone(toRaw(menuList.value));
  // console.log("🧪 ลองกดลบ id:", id);
  // console.log("🧪 menuList:", clonedMenuList);
  // console.log(typeof id);
  try {
    await axios.delete(`${import.meta.env.VITE_API_URL}/api/food/delete/${id}`);
    toast.success("Menu deleted successfully. ✅");
    clonedMenuList = clonedMenuList.filter((item) => item.id !== id);
    menuList.value = clonedMenuList; 
  } catch (error) {
    console.error("Delete error:", error);
    toast.error(error.response?.data?.message || "Failed to delete menu. ❌");
  }
};
</script>

<template>
  <div class="bg-[#F0F1C5]">
    <NavbarAdmin />
    <div class="flex flex-col justify-center items-center">
      <p v-if="loading">Loading...</p>
      <p v-else-if="error">❌ {{ error }}</p>
      <ul v-if="menuList.length" class="m-32">
        <li
          v-for="item in menuList"
          :key="item.id"
          class="grid grid-cols-4 items-center border-4 rounded-xl my-5 bg-white border-primary p-4 gap-4"
        >
          <!-- รูปภาพ -->
          <div>
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
          </div>

          <!-- ข้อมูล -->

          <h3 class="font-bold text-lg ml-20">{{ item.name }}</h3>
          <p class="font-bold text-lg ml-20">{{ item.price }} ฿</p>

          <!-- ปุ่มลบ -->
          <div class="flex justify-center gap-5">
            <button @click="deleteItem(item.id)">
              <img src="../../assets/delete.png" class="w-7" />
            </button>

            <!-- <button>
      <img src="../../assets/edit.png" class="w-7" />
    </button> -->
          </div>
        </li>
      </ul>
    </div>
  </div>
</template>
