<template>
  <!-- FIXME THE MODAL BUTTON ISN'T EVEN SHOWING UP. THIS IS BECAUSE THE MODAL IS NOT BEING CALLED CORRECTLY. TO CALL IT CORRECTLY THE CHANGES THAT NEED TO BE MADE ARE THE FOLLOWING: 
    MAKE THE MODAL A COMPONENT, THEN IMPORT IT INTO THE RECIPE CARD COMPONENT, THEN CALL THE MODAL COMPONENT IN THE RECIPE CARD COMPONENT. THOUGHT I WAS DOING THIS, BUT I'M MISSING SOMETHING! -->


  <div class="card-hover rounded-3">
    <div class="card-hover__content">
      <h3 class="card-hover__title">
        Flavor Alert! <span> {{ recipe.title }} </span> is trending!
      </h3>
      <p class="card-hover__text"> {{ recipe.description }} </p>
      <div class="text-center" style="text-shadow: 2px 2px 2px #000000;">
        <a href="#" class="card-hover__link">
          <span @click="getRecipeById(recipe.id)" class="flavor-link">Recipe</span>
          <RecipeDetailsModal />
          <svg fill="none" viewBox="0 0 24 24" stroke-width="3" stroke="#281704">
            <path stroke-linecap="round" stroke-linejoin="round" d="M13.5 4.5L21 12m0 0l-7.5 7.5M21 12H3" />
          </svg>        
        </a>
        
      </div>
    </div>
    <div class="card-hover__extra">
      <h5 class="mt-4"><span>Craving </span> {{ recipe.title }} <span> today?</span> </h5>
    </div>
    <img :src="recipe.image" :alt="recipe.title">
  </div>

  <!-- <div class="card-custom-chef">
    <p>{{ recipe.category }}</p>
  </div>
    <div class="card card-custom border-white border-0 my-5" style="">
      <div class="card-custom-img m-2" :style="`background-image: url(${recipe.image})`">
        <div style="background-color: #00000095; box-shadow: 0 5px 3px 3px #00000095;">
          <h6 class="card-title text-center"> {{ recipe.title }} </h6>
        </div>
      </div>
        // SECTION * * * CARD BODY * * * ---------------
        <div class="card-body" style="overflow-y: auto">
          
          <p class="card-text"> {{ recipe.category }} </p>
          <p  v-if="recipe.instructions.length <= 20" class="card-text"></p>
          <p class="card-text">{{ recipe.instructions }}</p>
        </div>
        // SECTION * * * CARD FOOTER * * * ----------------------------------------------
        <div class="card-footer row" style="background: inherit; border-color: inherit;">
          <div class="col-12">
            <p class="card-text">
              <small class="card-text" style="font-weight: 650;">
                Last Updated on: {{ 
                  new Date( recipe.createdAt )
                  .toLocaleDateString('en-US', {
                    year: 'numeric', 
                    month: 'short', 
                    day: 'numeric'
                  }) }}
                  @ {{ 
                    new Date( recipe.createdAt )
                    .toLocaleTimeString('en-US', {
                      hour: 'numeric', 
                      minute: 'numeric'
                    }) }}
              </small>
            </p>
          </div>
        </div>
      </div>    -->

</template>

<script>

import { logger } from '../utils/Logger';
import Pop from '../utils/Pop';
import { recipesService } from '../services/RecipesService';
import { Recipe } from "../models/Recipe.js";
// import { AppState } from "../AppState.js";
import { onMounted } from "vue";
// import { Modal } from "bootstrap";
import RecipeDetailsModal from "./RecipeDetailsModal.vue";
// import ActiveRecipeCard from "./ActiveRecipeCard.vue";
// import { Account } from "../models/Account.js";

export default {

  props: {
    recipe: {
      type: Recipe,
      required: true
    }
  },

  components: {
    RecipeDetailsModal
  },

	setup() {

    // const modalElement = ref(null)
    // const modal = ref(null)

    onMounted(() => {
      // modal.value = new Modal(modalElement.value)
    })
    
		return {
      // modalElement,
      // modal,

      getRecipeById(recipeId) {
        try {
          logger.log('[GETTING RECIPE BY ID]')
          recipesService.getRecipeById(recipeId)
        } catch (error) {
          Pop.error(error.message)
          logger.log(error)
        }
      }

      
			// async deleteRecipe() {
			// 	try {
			// 		await recipesService.deleteRecipe(props.recipes.id)
			// 		Pop.toast(`You have deleted ${props.recipes.title}`, 'success')
			// 	} catch (error) {
			// 		loggerecipe.error(error)
			// 		Pop.toast(errorecipe.message, 'error')
			// 	}
			// },
		}
	}
}

</script>

<style scoped lang="scss">

@import url('https://fonts.googleapis.com/css2?family=Fira+Sans:wght@300;400;600&display=swap');

$firaSans: 'Fira Sans', sans-serif;



*, *:before, *:after {
  box-sizing: inherit;
}

img {
  max-width: 100%;
  height: auto;
}

h1,
h2,
h3,
h4,
h5 {
  font-weight: 800;
  margin-top: 0;
  margin-bottom: 0;
}


body {
  display: flex;
  flex-direction: column;
  justify-content: center;
  align-items: center;
  margin: 0;
  padding: 0;
  height: 100%;
  color: #1F1D42;
  background-color: #F0F8E1;
}

.card-hover__content {
  padding-top: 10px !important;
}

.card-hover {
  $root: &;
  width: 350px;
  height: 400px;
  position: relative;
  overflow: hidden;
  box-shadow: 0 0 32px -10px rgba(0,0,0,0.08);

  &:has(#{$root}__link:hover) {

    #{$root}__extra {
      transform: translateY(0);
      transition: transform 0.35s;
    }
  }

  &:hover {
   
    #{$root} {

      &__content {
        background-color: #f9c592;
        filter: drop-shadow(15px 15px 5px #f9c692);
        bottom: 100%;
        transform: translateY(100%);
        padding: 50px 60px;
        transition: all 0.35s cubic-bezier(.1,.72,.4,.97);
        z-index: 2;
        opacity: .9;
      }

      &__link {
        bottom: -15rem;
        opacity: 1;
        transform: translate(-50%, 0);
        transition: all 0.3s 0.35s cubic-bezier(.1,.72,.4,.97);
      }
    }

    img {
      transform: scale(1);
      transition: 0.35s 0.1s transform cubic-bezier(.1,.72,.4,.97);
    }
  }

  &__content {
    width: 100%;
    height: 40%;
    text-align: center;
    background-color: #dc6563;
    text-shadow: 1px 1px 2px #f0f0f0;
    padding: 0 60px 50px;
    position: absolute;
    bottom: 0;
    left: 0;
    transform: translateY(0);
    transition: all 0.35s 0.35s cubic-bezier(.1,.72,.4,.97);
    will-change: bottom, background-color, transform, padding;
    z-index: 1;

    &::before,
    &::after {
      content: '';
      width: 100%;
      height: 80px;
      background-color: inherit;
      position: absolute;
      left: 0;
      z-index: -1;
    }

    &::before {
      top: -35px;
      clip-path: ellipse(60% 80px at bottom center);
    }

    &::after {
      bottom: -80px;
      clip-path: ellipse(60% 80px at top center);
    }
  }

  &__title {
    font-size: 1.5rem;
    margin-top: -1em;

    span {
      color: #e3e613;
      text-shadow: 3px 3px 5px #000000;
    }
  }

  &__text {
    font-size: 0.75rem;
  }

  &__link {
    font-size: 1.25rem;
    font-weight: 700;
    position: absolute;
    bottom: -14.5rem;
    left: 50%;
    transform: translate(-50%, 10%);
    display: flex;
    flex-wrap: wrap;
    align-items: center;
    text-decoration: none;
    color: #ffe241;
    text-shadow: 2px 2px 2px #000000;
    background-color: 0 0 0 2px #000000;
    opacity: 0;
    padding: 10px;
    transition: all 0.35s;   

    &:hover {
      
      svg {
        transform: translateX(10px);
      }
    }

    svg {
      width: 18px;
      margin-left: 4px;
      transition: transform 0.3s;
    }
  }

  &__extra {
    height: 50%;
    position: absolute;
    display: flex;
    align-items: center;
    justify-content: center;
    flex-direction: column;
    width: 100%;
    font-size: 1.4rem;
    text-align: center;
    background-color: #fffdcb9b;
    padding: 80px;
    bottom: 0;
    color: #462a2a;
    text-shadow: 2px 2px 3px #ffffff;
    transform: translateY(100%);
    will-change: transform;
    transition: transform 0.35s;
    z-index: 1;

    span {
      color: #482a06;
      text-shadow: 2px 2px 2px #b39f2e;
    }
  }

  .flavor-link:hover {
    color: #281704 !important;
    text-shadow: 2px 2px 3px #b39f2e !important;
    transition: all 0.35s;
  }

  img {
    position: absolute;
    top: 0;
    left: 0;
    width: 100%;
    height: 100%;
    object-fit: cover;
    object-position: center;
    transform: scale(1.2);
    transition: 0.35s 0.35s transform cubic-bezier(.1,.72,.4,.97);
    opacity: .9;
  }
}
/**.recipe-img {
  aspect-ratio: 1/.5;
  box-shadow: 0 0 15px 15px #000000;
  filter: drop-shadow(0 0 15px 15px #000000);
}

.card-custom {
  color: #efefef;
  text-shadow: 0 1px 5px #998ce2;
  overflow: hidden;
  height: 400px;
  background-color: #ff000035;
  box-shadow: 0 0 15px 2px #0a0a0a4d;
  transition: .5 ease-in-out;

}

.card-custom-img {
  height: 200px;
  min-height: 200px;
  background-color: #00000090;
  background-repeat: no-repeat;
  background-size: cover;
  background-position: center;
  opacity: .8;
  filter: brightness(1.3);
  box-shadow: 0 10px 0 10px #0a0a0a4d;
  border-color: inherit;
}

<!-- First border-left-width setting is a fallback -->
.card-custom-img::after {
  position: absolute;
  content: '';
  top: 160px;
  left: -10px;
  transform: rotate-X(180deg);
  width: 0;
  height: 0;
  border-style: solid;
  border-top-width: 40px;
  border-right-width: 0;
  border-bottom-width: 0;
  border-left-width: 545px;
  border-right-width: 545px;
  border-top-color: transparent;
  border-right-color: transparent;
  border-bottom-color: transparent;
  border-left-color: #0b0d12ef;
}

.card-custom-chef p {
  background-color: #ffaa64d4;
  border-radius: 5%;
  box-shadow: 0 0 15px 3px #0a0a0a9a;
  position: relative;
  top: 55px;
  left: 5.25rem;
  text-align: center;
  width: 110px;
  height: 33px;
}

.card-custom:hover {
  transform: scale(1.01);
  filter: brightness(1.1);
  transition: .5s ease-in-out;
}

::-webkit-scrollbar {
  background-color: #2d288cc6;
  width: .85rem;
  border-radius: 0.25rem;
}

::-webkit-scrollbar-thumb {
  background-color: #8b8b8b5f;
  border-radius: 0.25rem;
}

::-webkit-scrollbar-track {
  background-color: rgba(32, 20, 12, 0.512);
  border-radius: 0.25rem;
  border-style: ridge;
}

::-webkit-scrollbar-corner {
  background-color: #2d288c;
  border-radius: 0.5rem;
  border-style: groove;
}
 */
</style>