<template>
  <div class="min-h-screen flex justify-center items-center bg-[#F0F1C5]">
   <NavbarAdmin/>
  <div class="add flex justify-center">
    <form class="flex-col mt-20" @submit.prevent="onSubmitHandler">
      <div class="add-img-upload flex-col">
        <p class="text-xl font-bold mb-1">Upload image</p>
        <input
          @change="handleImageChange"
          type="file"
          accept="image/*"
          id="image"
          hidden              
        />
      
        <label for="image" >
          <img :src="image ? imagePreview : uploadArea" alt="upload-image" class="h-[200px] object-cover border border-black rounded-xl cursor-pointer hover:shadow-md"  />
        </label>
      </div>

      <div class="add-product-name flex-col">
        <p class="text-xl font-bold mb-1">Product name</p>
        <input
          name="name"
          v-model="formData.name"
          type="text"
          placeholder=""
          required
          class="w-[250px]  border-2 border-black rounded-lg"
        />
      </div>

      <div class="add-product-description flex-col">
        <p class="text-xl font-bold mb-1">Product description</p>
        <textarea
          name="description"
          v-model="formData.description"
          rows="3"
          placeholder=""
          required
          class="w-[250px]  border-2 border-black rounded-lg"
        ></textarea>
      </div>

      <div class="add-category-price">
        <div class="add-category flex-col">
          <p class="text-xl font-bold mb-1">Product category</p>
          <select name="category" v-model="formData.category"  class="w-[250px]  border-2 border-black rounded-lg">
            <option value="Salad">Salad</option>
            <option value="Rolls">Rolls</option>
            <option value="Deserts">Deserts</option>
            <option value="Sandwich">Sandwich</option>
            <option value="Cake">Cake</option>
            <option value="Pure Veg">Pure Veg</option>
            <option value="Pasta">Pasta</option>
            <option value="Noodles">Noodles</option>
          </select>
        </div>

        <div class="add-price flex-col">
          <p class="text-xl font-bold mb-1">Product Price</p>
          <input
            type="number"
            name="price"
            v-model="formData.price"
            placeholder="25"
            required
            class="w-[250px]  border-2 border-black rounded-lg"
          />
        </div>
      </div>

      <button type="submit" class="mt-3 py-3 px-10 bg-[#FFDF88] text-primary border-4 border-primary rounded-xl font-bold">{{loading ? 'Wait..':'ADD' }}</button>
    </form>
  </div>
</div> 
</template>

<script setup>
import { ref } from 'vue'
import axios from 'axios'
//import { toast } from 'vue3-toastify'
//import 'vue3-toastify/dist/index.css'
import uploadArea from '../../assets/uploadArea.png'
import NavbarAdmin from'../../components/Admin/NavbarAdmin.vue'

const loading = ref(false)
const image = ref(null)
const imagePreview = ref(null)
const formData = ref({
  name: '',
  description: '',
  price: '',
  category: 'Salad',
})

const url = 'http://localhost:5203' // เปลี่ยนตาม API จริง

const handleImageChange = (e) => {
  const file = e.target.files[0]
  if (file) {
    image.value = file
    imagePreview.value = URL.createObjectURL(file)
  }
}

const onSubmitHandler = async () => {
  if (!image.value) {
    alert('Image not selected')
    return
  }

  const dataToSend = new FormData()
  dataToSend.append('name', formData.value.name)
  dataToSend.append('description', formData.value.description)
  dataToSend.append('price', Number(formData.value.price))
  dataToSend.append('category', formData.value.category)
  dataToSend.append('file', image.value)

  loading.value = true // 👉 เริ่มโหลด

  try {
    const res = await axios.post(`${url}/api/food/add`, dataToSend)
    if (res.data.success) {
      alert(res.data.message)
      formData.value = {
        name: '',
        description: '',
        price: '',
        category: formData.value.category,
      }
      image.value = null
      imagePreview.value = null
    } else {
      alert(res.data.message)
    }
  } catch (err) {
    alert(err.response?.data?.message || 'Something went wrong')
  } finally {
    loading.value = false // 👉 จบโหลด
  
  }
}
</script>


