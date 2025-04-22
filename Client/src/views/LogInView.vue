<template>
  <form
    class="Login-warp flex items-center justify-center min-h-screen bg-[#D4EBF8]"
    @submit.prevent="handleSubmit"
  >
    <!-- Sign in -->
    <div
      v-if="isSignInStatus === 'SignIn'"
      class="border-4 text-center w-full max-w-md p-6 flex-col gap-4 rounded-xl border-primary bg-white shadow-lg"
    >
      <h1 class="font-bold text-primary text-4xl">Log In</h1>
      <div class="text-left w-full">
        <label class="font-bold text-primary text-xl block mb-1">Email:</label>
        <input
          v-model="email"
          type="email"
          class="w-full p-2 border-2 border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-primary"
          required
        />
      </div>
      <div class="text-left w-full">
        <label class="font-bold text-primary text-xl block">Password:</label>
        <input
          v-model="password"
          type="password"
          class="w-full p-2 border-2 border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-primary"
          required
        />
      </div>
      <button
       type="submit"
        class="w-full border-4 border-primary p-3 my-5 text-xl font-bold bg-[#FFF085] text-primary rounded-full"
      >
        Log in
      </button>
      <div class="mt-4 flex flex-row">
        <input type="checkbox" required class="mx-3 mt-0 w-7" />
        <div class="text-xl text-gray-600 text-left">
          By continuing, I agree to the terms of use & privacy policy.
        </div>
      </div>
      <button
        class="text-xl mt-2 font-medium"
        @click="isSignInStatus = 'SignUp'"
      >
        Create a new account ? <span class="text-red-600">Click here</span>
      </button>
    </div>
    <!-- Sign Up -->
    <div
      v-else-if="isSignInStatus === 'SignUp'"
      class="border-4 text-center w-full max-w-md p-6 flex-col gap-4 rounded-xl border-primary bg-white shadow-lg"
    >
      <h1 class="font-bold text-primary text-4xl">Sign Up</h1>
      <div class="text-left w-full">
        <label class="font-bold text-primary text-xl block mb-1">Name:</label>
        <input
          v-model="name"
          type="text"
          class="w-full p-2 border-2 border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-primary"
          required
        />
      </div>
      <div class="text-left w-full">
        <label class="font-bold text-primary text-xl block mb-1"
          >Lastname:</label
        >
        <input
          v-model="lastname"
          type="text"
          class="w-full p-2 border-2 border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-primary"
          required
        />
      </div>
      <div class="text-left w-full">
        <label class="font-bold text-primary text-xl block mb-1">Email:</label>
        <input
          v-model="email"
          type="email"
          class="w-full p-2 border-2 border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-primary"
          required
        />
      </div>
      <div class="text-left w-full">
        <label class="font-bold text-primary text-xl block">Password:</label>
        <input
          v-model="password"
          type="password"
          class="w-full p-2 border-2 border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-primary"
          required
        />
      </div>
      <button
        type="submit"
        class="w-full border-4 border-primary p-3 my-5 text-xl font-bold bg-[#FFF085] text-primary rounded-full"
      >
        Sign Up
      </button>
      <div class="mt-4 flex flex-row">
        <input type="checkbox" required class="mx-3 mt-0 w-7" />
        <div class="text-xl text-gray-600 text-left">
          By continuing, I agree to the terms of use & privacy policy.
        </div>
      </div>
      <button
        class="text-xl mt-2 font-medium"
        @click="isSignInStatus = 'SignIn'"
      >
        Already have an account ? <span class="text-red-600">Login here</span>
      </button>
    </div>
  </form>
</template>
<script setup>
import { ref } from "vue";
import axios from "axios";
import { useRouter } from "vue-router";
import { useAuthStore } from "../stores/useAuthStore";
const authStore = useAuthStore();

const isSignInStatus = ref("SignUp");
const email = ref("");
const password = ref("");
const name = ref("");
const lastname = ref("");
const router = useRouter();

const baseUrl = "https://localhost:7089/api/auth";

const handleSubmit = async () => {
  try {
    let res;
    if (isSignInStatus.value === "SignUp") {
      res = await axios.post(`${baseUrl}/register`, {
        email: email.value,
        password: password.value,
        username: name.value,
        lastname: lastname.value,
      });
    } else {
      res = await axios.post(`${baseUrl}/login`, {
        email: email.value,
        password: password.value,
      });
    }

    const token = res.data.token;
    const user = res.data.User;
    const respone =res.data
    localStorage.setItem("token", token);

    // console.log("🧪 token:", token);
    // console.log("🧪 User:", user); // 👈 สำคัญ
    // console.log("🧪 Respone:", respone);

    authStore.setToken(token);
    authStore.setUser(user);
    authStore.setResponse(respone);

    router.push("/");
  } catch (err) {
    alert(
      "Authentication failed: " +
        (err.response?.data ?? err.message ?? "Unknown error")
    );
  }
};
</script>
