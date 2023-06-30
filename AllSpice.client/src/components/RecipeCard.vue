<template>

<section class="container-fluid">
  
  
    <div class="card card-custom border-white border-0" style="">
      <div class="card-custom-img" :style="`background-image: url(${recipe.image})`"></div>
        <div v-if="recipe.creatorId == account.id">
          <h5 class="host-badge">CHEF</h5>
        </div>
        <div class="card-custom-avatar">
          <img class="img-fluid object-fit-cover" :src="recipe.creator.picture" :alt="recipe.creator.name" />
        </div>
        <!--SECTION * * * CARD BODY * * * ------------->
        <div class="card-body" style="overflow-y: auto">
          <h4 class="card-title"> {{ recipe.title }} </h4>
          <p class="card-text"> {{ recipe.category }} </p>
          <p  v-if="recipe.instructions.length <= 20" class="card-text"></p>
          <p class="card-text">{{ recipe.instructions }}</p>
        </div>
        <!--SECTION * * * CARD FOOTER * * * -------------------------------------------->
        <div class="card-footer row" style="background: inherit; border-color: inherit;">
          <div class="col-12">
            <p class="card-text">
              <small class="card-text" style="font-weight: 650;">
                Last Updated on: {{ 
                  new Date( recipe.updatedAt )
                  .toLocaleDateString('en-US', {
                    year: 'numeric', 
                    month: 'short', 
                    day: 'numeric'
                  }) }}
                  @ {{ 
                    new Date( recipe.updatedAt )
                    .toLocaleTimeString('en-US', {
                      hour: 'numeric', 
                      minute: 'numeric'
                    }) }}
              </small>
            </p>
          </div>
          <div class="col-6 ps-4 py-1">
            <a href="#" class="btn btn-dark" style="color: dodgerblue;">To Top</a>
          </div>
          <div class="col-6 ps-5 py-1">
            <!-- <router-link :to="{ name: 'rDetails', params: { rId: recipe.id } }">
              <a href="#" aria-label="Go to r Page" class="btn btn-outline-primary">Battle</a>
            </router-link> -->
          </div>
        </div>
      </div>   

  



</section>

</template>

<script>

import { logger } from '../utils/Logger';
import Pop from '../utils/Pop';
import { recipesService } from '../services/RecipesService';
import { Recipe } from "../models/Recipe.js";
import { AppState } from "../AppState.js";
import { computed } from "vue";

export default {

  props: {
    recipe: {
      type: Recipe,
      required: true
    }
  },

	setup() {
    
		return {

      account: computed(() => AppState.account),

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

.recipe-img {
  aspect-ratio: 1/.5;
  box-shadow: 0 0 15px 15px #000000;
  filter: drop-shadow(0 0 15px 15px #000000);
}

.card-custom {
  color: #efefef;
  text-shadow: 0 1px 5px #998ce2;
  overflow: hidden;
  height: 700px;
  background-color: #0a0b14f3;
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

/* First border-left-width setting is a fallback */
.card-custom-img::after {
  position: absolute;
  content: '';
  top: 161px;
  left: 0;
  width: 0;
  height: 0;
  border-style: solid;
  border-top-width: 40px;
  border-right-width: 0;
  border-bottom-width: 0;
  border-left-width: 545px;
  border-left-width: calc(575px - 5vw);
  border-top-color: transparent;
  border-right-color: transparent;
  border-bottom-color: transparent;
  border-left-color: #0b0d12ef;
}

.card-custom-chef img {
  border-radius: 50%;
  box-shadow: 0 0 15px 3px #0a0a0a9a;
  position: absolute;
  border: 2.5px solid #000000;
  top: 100px;
  left: 1.25rem;
  width: 100px;
  height: 100px;
}


</style>