<template>

  <button class="offcanvas-button" style="position: absolute; top: 5.5rem; right: 2rem;" type="button" data-bs-toggle="offcanvas" data-bs-target="#offcanvasWithBothOptions" aria-controls="offcanvasWithBothOptions"><span class="w-100 fs-6">Create Recipe</span>
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

<div class="offcanvas offcanvas-lg offcanvas-end" style="width: 1000px; height: 100vh;" data-bs-scroll="true" tabindex="-1" id="offcanvasWithBothOptions" aria-labelledby="offcanvasWithBothOptionsLabel">
  <div class="offcanvas-header">
    <h5 class="offcanvas-title" id="offcanvasWithBothOptionsLabel"></h5>
    <button type="button" class="btn-close" data-bs-dismiss="offcanvas" aria-label="Close"></button>
  </div>
  <div>
    
    <form class="form__contact" @submit.prevent="createRecipe()">
      <fieldset>
        <div class="offcanvas-body row justify-content-center align-items-center pt-5">
          <p class="canvas-header pt-5">Create a New FlavorIt Recipe!</p>
          <div class="col-10">
            <label for="category" class="mt-5">Select a Category</label>
            <select name="category" id="category" class="form-control" required v-model="editable.category">
              <option disabled selected class="text-center fw-bold">** OPTIONS **</option>
              <option value="Specialty Coffee">Specialty Coffee</option>
              <option value="Starters">Starters</option>
              <option value="Mexican">Mexican</option>
              <option value="Italian">Italian</option>
              <option value="Salad">American</option>
              <option value="Bread">Chinese</option>
              <option value="Soup">Soup</option>
              <option value="Cheese">Cheese</option>
              <option value="Dessert">Dessert</option>
              <option value="Other">Other</option>
            </select>
          </div>
          <div class="col-10">
            <label for="title" class="mt-4">FlavorIt Name</label>
            <input type="text" id="title" name="title" class="form-control" minlength="2" maxlength="100" required
              v-model="editable.title">
          </div>
          <div class="col-10">
            <label for="imgUrl" class="mt-4">Photo</label>
            <input type="url" id="imgUrl" name="imgUrl" class="form-control" required minlength="5" maxlength="500"
              v-model="editable.img" @input="showImg()">
            <div class="d-flex justify-content-center">
              <img :src="showImg" v-if="showImg" style="object-fit: cover; object-position: center;" alt="Image unable to render">
            </div>
          </div>
          <div class="col-10">
            <label for="instructions" class="mt-4">Instructions</label>
            <input type="text" id="instructions" name="instructions" class="form-control" required minlength="3"
              maxlength="5000" v-model="editable.instructions">
          </div>
        </div>
        <button type="submit" class="button close-button fs-1" tabindex="4">Create Recipe &#187;</button>
      </fieldset>
    </form>
    //
    <svg version="1.1" id="Layer_1" xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" xml:space="preserve">
      <defs>
        <filter id="blur0">
          <feGaussianBlur in="SourceGraphic" stdDeviation="0 0" />
        </filter>
        <filter id="blur1">
          <feGaussianBlur in="SourceGraphic" stdDeviation="0 5" />
        </filter>
        <filter id="blur2">
          <feGaussianBlur in="SourceGraphic" stdDeviation="0 10" />
        </filter>
        <filter id="blur3">
          <feGaussianBlur in="SourceGraphic" stdDeviation="0 15" />
        </filter>
        <filter id="blur4">
          <feGaussianBlur in="SourceGraphic" stdDeviation="0 20" />
        </filter>
      </defs>
    </svg>

  </div>
</div>













    

</template>

<script>

import { ref, watchEffect } from 'vue'
import { logger } from '../utils/Logger'
import Pop from '../utils/Pop'
import { recipesService } from '../services/RecipesService'
import { AppState } from '../AppState'
import { Offcanvas } from "bootstrap"
import { router } from "../router.js"

export default {
  
  setup() {

    const editable = ref({})
    const showImg = ref(null)

    watchEffect(() => {
      if (AppState.activeRecipe) {
        showImg.value = AppState.activeRecipe.image
      }
    })

    return {

      editable,
      showImg,

      async createRecipe() {
        try {
          logger.log(editable.value)
          const recipeData = editable.value
          await recipesService.createRecipe(recipeData)
          Offcanvas.getOrCreateInstance('#offcanvasWithBothOptions').hide()
          // router.push(
          //   { name:
          //     'RecipeDetails',
          //     params: {
          //       recipeId: newRecipe.id
          //     }
          //   }
          // )
        } catch (error) {
          logger.error(error)
          Pop.toast(error, `Failed to create ${editable.value.title}}`)
        }
      },

      previewImage() {
        showImg.value = editable.value.image
        logger.log(`Selected image for ${AppState.activeRecipe.name}`, showImg.value, editable.value.image)
      }

    }
  }
}

</script>

<style scoped lang="scss">

@import url('https://fonts.googleapis.com/css?family=Roboto+Slab:400');
@import url('https://fonts.googleapis.com/css?family=Shadows+Into+Light');

$line-height: 40px;

::selection { 
  color: #fff;
  background: #9FBE5A;  
}

body {
  width: 100vw;
  height: 100%; 
  padding-bottom: 50px;
  overflow-x: hidden; 
  display: flex;
  align-items: center;
  justify-content: center;
  background: linear-gradient(to bottom, #16a085, #f4d03f) center / cover no-repeat;
}

.canvas-header {
  margin-bottom: 75px;
  font: 400 56px 'Roboto Slab', serif;
  font-size: 40px;
  text-align: center;
  text-shadow: 1px 1px 3px rgba(255, 0, 0, 0.518);
  animation: fadeIn 2s ease-in-out forwards;
}



//NOTE - <form> HOLDS THE 'TORN PAPER' BORDER. CREATE CSS INSTANCE FOR THE FOLLOWING: 
//  form {
//   /* place styling attributes here */
// }

.form__contact {
  max-width: 1000px;
  margin: 0 auto;
  border-left: 30px solid white; 
  border-image: url(data:image/svg+xml;base64,PD94bWwgdmVyc2lvbj0iMS4wIiBlbmNvZGluZz0idXRmLTgiPz48IURPQ1RZUEUgc3ZnIFBVQkxJQyAiLS8vVzNDLy9EVEQgU1ZHIDEuMS8vRU4iICJodHRwOi8vd3d3LnczLm9yZy9HcmFwaGljcy9TVkcvMS4xL0RURC9zdmcxMS5kdGQiPjxzdmcgdmVyc2lvbj0iMS4xIiBpZD0iTGF5ZXJfMSIgeG1sbnM9Imh0dHA6Ly93d3cudzMub3JnLzIwMDAvc3ZnIiB4bWxuczp4bGluaz0iaHR0cDovL3d3dy53My5vcmcvMTk5OS94bGluayIgeD0iMHB4IiB5PSIwcHgiIHdpZHRoPSI0MS44NnB4IiBoZWlnaHQ9IjUyLjMyNnB4IiB2aWV3Qm94PSIwIDAgNDEuODYgNTIuMzI2IiBlbmFibGUtYmFja2dyb3VuZD0ibmV3IDAgMCA0MS44NiA1Mi4zMjYiIHhtbDpzcGFjZT0icHJlc2VydmUiPjxwYXRoIGZpbGw9IiNGRkZGRkYiIGQ9Ik0wLDB2MjUuMTYzaDcuMDk3YzAuNTAxLTQuOTg5LDQuNzEyLTguODg0LDkuODMzLTguODg0YzUuNDU4LDAsOS44ODQsNC40MjUsOS44ODQsOS44ODRzLTQuNDI1LDkuODg0LTkuODg0LDkuODg0Yy01LjEyMSwwLTkuMzMyLTMuODk1LTkuODMzLTguODg0SDB2MjUuMTYzaDQxLjg2VjBIMHoiLz48L3N2Zz4=) 5% 100% repeat;
  border-image-width: 0px 0px 0px 30px;
  transform: translateY(100%);
  animation: init 1s ease-in-out forwards;
  
  fieldset {
    position: relative;
    height: 100vh;
    margin: 0;
    padding: 5px 5px $line-height 80px;
    border: none;
    border-radius: 0 20px 20px 0;
    font: 24px 'Shadows Into Light', cursive;
    background: #fff linear-gradient(rgba(0,0,0,.1) 1px, transparent 0) 0 20px / 100% $line-height;
    
    &:after {
      position: absolute; 
      top: 0;
      left: 50px;
      content: '';
      height: 100%;
      width: 1px;
      border-left: double #E08183;
    }
  } 
  
	p {
    margin: 0 0 $line-height 0;
    line-height: $line-height;
    color: #333;
	}

	span {
		position: relative;
		line-height: $line-height;
	}

	.close-button {
		margin-top: $line-height;
    float: right;
    border: none;
    font-family: 'Shadows Into Light', cursive;
    color: #E08183;
    background: transparent;
    cursor: pointer;
    transition: transform 0.25s ease;
    
    &:hover {
      transform: translateX(10px)
    }
	}
}

.form__field {
  display: inline;
  color: #7DB665;
  outline: none;

  &:empty {
    display: inline-block;
    color: #ff0000;
  }
  
  // Use a data-attr to replicate a placeholder
  &:empty,
  &:empty:focus {
    &:before {
      content: attr(data-placeholder);
    }
  }
}

// Hide blur defs
svg {
  display: none;
}

@keyframes init {
  75% {
    transform: translateY(-20px);
  }
  100% {
    transform: translateY(0);
  }
}

@keyframes do-blur {
  0% {
    filter: url(#blur4);
  }
  
  25% {
    filter: url(#blur3);
  }
  
  50% {
    filter: url(#blur2);
  }
  
  75% {
    filter: url(#blur1);
  }
  
  100% {
    filter: url(#blur0); 
  }
}



@import url('https://fonts.googleapis.com/css2?family=Inter:wght@100..900&display=swap');

body {
	background: #170F1E;
	display: grid;
	height: 100vh;
	place-items: center;
	-webkit-font-smoothing: antialiased;
	width: 100vw;
}

.offcanvas-button {
  animation: 1.5s ease infinite alternate running shimmer;
  background: linear-gradient(90deg, #FFE27D 0%, #ffb05b 30%, #ff916c 85%);
  background-size: 200% 100%;
  border: none;
	border-radius: 6px;
  box-shadow: -2px -2px 10px rgba(255, 227, 126, 0.5), 2px 2px 10px rgba(255, 98, 0, 0.5);
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

@keyframes shimmer {
  to {
    background-size: 100% 100%;
    box-shadow: -2px -2px 6px rgba(255, 227, 126, 0.5), 2px 2px 6px rgba(144, 148, 255, 0.5);
  }
}

</style>