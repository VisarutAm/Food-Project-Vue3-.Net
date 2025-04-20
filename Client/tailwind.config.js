/** @type {import('tailwindcss').Config} */
export default {
  content: ['./index.html', './src/**/*.{vue,js,ts,jsx,tsx}'],
  theme: {
    extend: {
      colors:{
        primary:'#74512D',
        secondary:'#F8F4E1'
      }
    },
  },
  plugins: [],
}

