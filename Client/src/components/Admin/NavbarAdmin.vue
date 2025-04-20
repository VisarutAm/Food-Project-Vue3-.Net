<template >
  <header class="fixed top-0 left-0 right-0 z-50">
    <div
      class="bg-secondary flex justify-between items-center py-[10px] pl-[20px] pr-[10px] lg:py-[10px] lg:pr-[10px] lg:pl-[20px] md:py-[20px] md:pr-[20px] md:pl-[24px] rounded-2xl m-4 sm:m-6 lg:mx-20 lg:my-5 flex-row gap-4 sm:gap-0 border border-[#FFFF] from-gray-50/80"
      style="
        box-shadow: rgba(33, 2, 73, 0.06) 0px 6px 14px 0px;
        backdrop-filter: blur(15px);
      "
    >
      <h1 class="text-primary text-5xl font-bold">Thai Food.</h1>

      <!-- Mobile Toggle Button -->
      <div class="md:hidden z-30">
        <button
          class="block focus:outline-none"
          @click="isMenuOpen = !isMenuOpen"
        >
          <span v-if="isMenuOpen" class="text-5xl md:text-primary text-white">
            <img src="../../assets/close.png" alt="close" class="w-5 mr-4" />
          </span>
          <span v-else class="text-5xl md:text-primary text-white">
            <img
              src="../../assets/burger-bar.png"
              alt="menu"
              class="lg:w-6 md:h-6 h-6 w-6 mr-4"
            />
          </span>
        </button>
      </div>

      <!-- Navbar -->
      <nav
        :class="[
          `fixed inset-0 z-20 flex flex-col items-center justify-center  md:relative md:bg-transparent
         md:flex md:justify-between md:flex-row ${
           isMenuOpen ? 'block' : 'hidden'
         }`,
        ]"
      >
        <ul
          class="flex flex-col items-center space-y-5 md:flex-row md:space-x-5 md:space-y-0 font-extrabold max-md:mt-[500px] bg-secondary rounded-2xl"
        >
          <li v-for="item in Menu" :key="item.name">
            <router-link
              :to="item.href"
              class="block transition ease-linear md:text-lg lg:text-xl font-bold text-primary md:text-primary hover:font-extrabold text-2xl"
            >
              {{ item.name }}
            </router-link>
          </li>
          <hr class="w-full border-2 border-gray-500 my-4 md:hidden" />
          <div
            v-if="authStore.token && authStore.response"
            class="navbar-login flex flex-col gap-5 mr-20 items-center md:hidden"
          >
            <p class="text-primary font-bold text-xl ml-20">
              Hello,
              {{
                authStore.response.userName + "  " + authStore.response.lastname
              }}
            </p>
            <div class="flex flex-row text-center ml-10">
              <img
                src="../../assets/shopping-basket.png"
                alt="order"
                class="w-10 mr-3"
              />
              <img
                src="../../assets/customer.png"
                alt="user-image"
                class="rounded cursor-pointer w-10 border-[3px] border-primary"
              />
            </div>
            <div class="navbar-profile group mr-15 border-amber-500 ml-10">
              <ul class="navbar-profile">
                <li class="text-center font-medium">
                  <p>Orders</p>
                </li>
                <li class="text-center font-medium">
                  <p
                    v-if="authStore.token && authStore.response.role == 'Admin'"
                  >
                    Admindashboard
                  </p>
                </li>
                <li
                  @click="handleLogout"
                  class="text-center font-bold block text-red-500 cursor-pointer"
                >
                  <p>Logout</p>
                </li>
              </ul>
            </div>
          </div>

          <!-- 🔓 ถ้ายังไม่ login -->
          <router-link
            v-if="!authStore.token"
            to="/login"
            class="md:hidden font-medium"
          >
            <Button label="Log In" />
          </router-link>
        </ul>
      </nav>
      <!-- 🔐 ถ้า login แล้ว -->

      <div
        v-if="authStore.token && authStore.response"
        class="navbar-login flex flex-row gap-5 mr-20 max-md:hidden"
      >
        <p class="text-primary font-bold text-xl mr-5">
          Hello,
          {{ authStore.response.userName + "  " + authStore.response.lastname }}
        </p>
        <img
          src="../../assets/shopping-basket.png"
          alt="order"
          class="w-10 mr-3"
        />
        <div
          class="navbar-profile relative group mr-15 border-amber-500 border-3 rounded-full"
        >
          <img
            src="../../assets/customer.png"
            alt="user-image"
            class="rounded-xl cursor-pointer w-10 border-[3px] border-primary"
          />
          <ul
            class="navbar-profile-dropdown absolute right-0 mt-2 w-40 bg-white shadow-lg rounded-lg overflow-hidden opacity-0 group-hover:opacity-100 transition-opacity duration-300 z-100 border-green-800 border-2"
          >
            <li class="text-center font-medium">
              <p>Orders</p>
            </li>
            <li class="text-center font-medium">
              <p v-if="authStore.token && authStore.response.role == 'Admin'">
                Admindashboard
              </p>
            </li>
            <hr />
            <li
              @click="handleLogout"
              class="text-center font-medium cursor-pointer"
            >
              <p>Logout</p>
            </li>
          </ul>
        </div>
      </div>
      <!-- 🔓 ถ้ายังไม่ login -->
      <router-link
        v-if="!authStore.token"
        to="/login"
        class="mr-20 max-md:hidden"
      >
        <Button label="Log In" />
      </router-link>
    </div>
  </header>
</template>
  <script setup>
import Button from "../UI/Button.vue";
import { ref, onMounted } from "vue";
import { useAuthStore } from "../../stores/useAuthStore";
const authStore = useAuthStore();
const isMenuOpen = ref(false);
const Menu = ref([
  { name: "AddItem", href: "/admin" },
  { name: "ListMenu", href: "/list" },
  { name: "Order " },
]);
const scrollToSection = (href) => {
  isMenuOpen.value = false;
  const section = document.querySelector(href);
  if (section) {
    section.scrollIntoView({ behavior: "smooth" });
  }
};
const handleLogout = () => {
  authStore.logout(); // ล้าง token + user จาก store
};
onMounted(() => {
  console.log("🧑‍💻 token:", authStore.token);
  //   console.log('🧑‍💻 user:', authStore.user)
  console.log("🧑‍💻 Response:", authStore.response);
});
// watch(() => authStore.token, (newUser) => {
//   console.log('🧑‍💻 user changed:', newUser)
// })
</script>
 