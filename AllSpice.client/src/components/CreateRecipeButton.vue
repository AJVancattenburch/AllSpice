<template>
  
  <button class="offcanvas-button" style="position: absolute; top: 5.5rem; right: 2rem;" type="button" data-bs-toggle="offcanvas" data-bs-target="#offcanvasWithBothOptions" aria-controls="offcanvasWithBothOptions"><span class="w-100">Create Recipe</span>
    <svg width="79" height="46" viewBox="0 0 79 46" fill="none" xmlns="http://www.w3.org/2000/svg">
    <g filter="url(#filter0_f_618_1123)">
      <path d="M42.9 2H76.5L34.5 44H2L42.9 2Z" fill="url(#paint0_linear_618_1123)"/>
    </g>
    <defs>
      <filter id="filter0_f_618_1123" x="0" y="0" width="78.5" height="46" filterUnits="userSpaceOnUse" color-interpolation-filters="sRGB">
        <feFlood flood-opacity="0" result="BackgroundImageFix"/>
        <feBlend mode="normal" in="SourceGraphic" in2="BackgroundImageFix" result="shape"/>
        <feGaussianBlur stdDeviation="1" result="effect1_foregroundBlur_618_1123"/>
      </filter>
      <linearGradient id="paint0_linear_618_1123" x1="76.5" y1="2.00002" x2="34.5" y2="44" gradientUnits="userSpaceOnUse">
        <stop stop-color="white" stop-opacity="0.6"/>
        <stop offset="1" stop-color="white" stop-opacity="0.05"/>
      </linearGradient>
    </defs>
  </svg>
</button>

</template>

<script>

import { computed } from 'vue'
import { AppState } from '../AppState'
import { logger } from '../utils/Logger'
import { recipesService } from '../services/RecipesService'
import Pop from '../utils/Pop'

export default {
  
  setup() {

    return {

      account: computed(() => AppState.account),
      
      // recipe: computed(() => AppState.activeRecipe),

      // getRecipeById(recipeId) {
      //   try {
      //     logger.log('[GETTING RECIPE BY ID]')
      //     recipesService.getRecipeById(recipeId)
      //   } catch (error) {
      //     Pop.error(error.message)
      //     logger.log(error)
      //   }
      // }

    }
  }
}

</script>

<style scoped lang="scss">

@import url('https://fonts.googleapis.com/css2?family=Inter:wght@100..900&display=swap');

body {
	background: #170F1E;
	display: grid;
	height: 100vh;
	place-items: center;
	-webkit-font-smoothing: antialiased;
	width: 100vw;
}

.button {
  animation: 1.5s ease infinite alternate running shimmer;
  background: linear-gradient(90deg, #FFE27D 0%, #ffb05b 30%, #ff916c 85%);
  background-size: 200% 100%;
  border: none;
	border-radius: 6px;
  box-shadow: -2px -2px 10px #ffe37e80, 
              2px 2px 10px #ff620080;
  color: #170F1E;
  cursor: pointer;
	font-family: 'Inter', sans-serif;
  font-size: 16px;
	font-weight: 670;
  line-height: 24px;
  overflow: hidden;
  padding: 12px 24px;
  position: absolute;
  text-decoration: none;
  transition: 0.2s;
  
  svg {
    left: -20px;
    opacity: 0.5;
    position: absolute;
    top: -2px;
    transition: 0.5s cubic-bezier(.5,-0.5,.5,1.5);
  }
  
  &:hover svg {
    opacity: 0.8;
    transform: translateX(50px) scale(1.5);
  }
  
  &:hover {
    transform: rotate(-3deg);
  }
  
  &:active {
    transform: scale(0.95) rotate(-3deg);
  }
}

.button:hover {
  animation: 1.5s ease infinite alternate running shimmerAndTilt;
}

@keyframes shimmerAndTilt {
  0% {
    transform: rotate(0deg) translateX(100%);
    background-size: 100% 100%;
    box-shadow: -2px -2px 6px #ffe37e80, 
                2px 2px 6px #9094ff80;
  }
  50% {
    transform: rotate(-10deg) translateX(50%);
    background-size: 200% 100%;
    box-shadow: -2px -2px 10px #ffe37e80, 
                2px 2px 10px #ff620080;
  }
  100% {
    transform: rotate(0deg) translateX(0%);
    background-size: 100% 100%;
    box-shadow: -2px -2px 6px #ffe37e80, 
                2px 2px 6px #9094ff80;
  }
}

</style>