/** @type {import('tailwindcss').Config} */
export default {
  corePlugins: {
    preflight: false,
  },
  // prefix: 'tw-',
  content: ["./src/**/*.{html,js,tsx}"],
  theme: {
    fontFamily: {
      custom1: ["Custom1", "sans-serif"],
      sans: ['Montserrat', 'sans-serif'],
    },
    extend: {},
  },
  plugins: [require('daisyui'),],
  daisyui: {
    themes: ["light", "cupcake"],
  }
}

