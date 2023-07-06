<template>
  <!-- FIXME THE MODAL BUTTON ISN'T EVEN SHOWING UP. THIS IS BECAUSE THE MODAL IS NOT BEING CALLED CORRECTLY. TO CALL IT CORRECTLY THE CHANGES THAT NEED TO BE MADE ARE THE FOLLOWING: 
    MAKE THE MODAL A COMPONENT, THEN IMPORT IT INTO THE RECIPE CARD COMPONENT, THEN CALL THE MODAL COMPONENT IN THE RECIPE CARD COMPONENT. THOUGHT I WAS DOING THIS, BUT I'M MISSING SOMETHING! -->

    <div v-if="recipe" class="card-hover rounded-3">
      <div class="card-hover__content">
        <h3 class="card-hover__title">
          Flavor Alert! <span> {{ recipe.title }} </span> is trending!
        </h3>
        <p class="card-hover__text"> {{ recipe.description }} </p>
        <div class="text-center" style="text-shadow: 2px 2px 2px #000000;">
          <div class="card-hover__link">
            <!-- STUB OFFCANVAS BUTTON FOR RECIPE DETAILS -->
            <span @click="getRecipeById(recipe.id)" class="flavor-link offcanvas-button" type="button" data-bs-toggle="offcanvas" data-bs-target="#offcanvasWithBothOptions" aria-controls="offcanvasWithBothOptions">Recipe Details</span>
            <svg fill="none" viewBox="0 0 24 24" stroke-width="3" stroke="#281704">
              <path stroke-linecap="round" stroke-linejoin="round" d="M13.5 4.5L21 12m0 0l-7.5 7.5M21 12H3" />
            </svg>        
          </div>
      </div>
    </div>
    <div class="card-hover__extra">
      <h5 class="mt-4"><span>Craving </span> {{ recipe.title }} <span> today?</span> </h5>
    </div>
    <img :src="recipe.image" :alt="recipe.title">
    <div v-if="recipe.creatorId == account.id" style="position: absolute; bottom: 0; right: 0;">
      <i @click="deleteRecipe()" class="mdi mdi-trash-can delete-recipe fs-1" style="color: white;"></i>
    </div>
  </div>
  <OffCanvas id="offcanvasWithBothOptions" class="offcanvas offcanvas-xxl offcanvas-top">
    <RecipeDetailsCard />
  </OffCanvas>

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
import { AppState } from "../AppState.js";
import { onMounted, computed } from "vue";
// import { Modal } from "bootstrap";
// import { Account } from "../models/Account.js";
import RecipeDetailsCard from "../components/RecipeDetailsCard.vue";
import OffCanvas from '../components/Offcanvas.vue';
// import { Offcanvas } from "bootstrap";

export default {

  props: {
    recipe: {
      type: Recipe,
      required: true
    }
  },

  components: {
    RecipeDetailsCard, 
    OffCanvas
  },

	setup(props) {

    // const modalElement = ref(null)
    // const modal = ref(null)

    onMounted(() => {
      // modal.value = new Modal(modalElement.value)
    })
    
		return {
      // modalElement,
      // modal,
      account: computed(() => AppState.account),

      getRecipeById(recipeId) {
        try {
          logger.log('[GETTING RECIPE BY ID]')
          recipesService.getRecipeById(recipeId)
          // Offcanvas.getOrCreateInstance('#offcanvasWithBothOptions').hide()
        } catch (error) {
          Pop.error(error.message)
          logger.log(error)
        }
      },

      
			async deleteRecipe() {
				try {
					if (await Pop.confirm(`Are you sure you want to delete ${props.recipe.title}?`)) {
             recipesService.deleteRecipe(props.recipe.id)
          }
					Pop.toast(`Did not delete ${props.recipe.title}`, 'success')
				} catch (error) {
					logger.error(error)
					Pop.toast(error.message, 'error')
				}
			},
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

.offcanvas-xxl {
  display: flex;
  flex-direction: column;
  justify-content: center;
  align-items: center;
  min-width: 75% !important;
  right: 0 !important;
  left: 0 !important;
  margin: auto !important;
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
    opacity: 1;
  }
}

i.delete-recipe {
  transform: translateX(30px) !important;
}

</style>