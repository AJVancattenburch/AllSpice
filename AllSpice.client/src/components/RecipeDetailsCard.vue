<template>
 
  <!-- <div class="modal-body row">
    <button type="button" class="btn-close" data-bs-dismiss="modal" data-bs-target="#recipeDetailsModal" aria-label="Close">
    </button> -->
    <div class="card-custom-chef">
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
            <!-- <p  v-if="recipe.instructions.length <= 20" class="card-text"></p>
            <div v-if="recipe?.id == account.id">

            </div> -->
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
        </div>
    
  <!-- </div> -->
  

</template>

<script>

import { computed } from 'vue'
import { AppState } from '../AppState'
// import Pop from "../utils/Pop"
// import { logger } from '../utils/Logger'
// import { recipesService } from '../services/RecipesService.js'
// import { useRoute } from "vue-router"

export default {
  
  setup() {

    
    return {

      account: computed(() => AppState.account),
      recipe: computed(() => AppState.recipes.find(r => r.id === AppState.activeRecipe.id)),

    }
  }
}

</script>

<style scoped lang="scss">



</style>