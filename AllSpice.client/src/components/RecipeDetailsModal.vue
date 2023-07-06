<template>

  <button type="button" class="btn btn-primary" data-bs-toggle="modal" data-bs-target="#recipeDetailsModal">
    Launch demo modal
  </button>
  
  <!-- Modal -->
  <div v-if="recipe"
   class="modal fade" id="recipeDetailsModal" tabindex="-1" aria-labelledby="recipeDetailsModalLabel" aria-hidden="true">
    <div class="modal-dialog">
      <div class="modal-content">
        <div class="modal-header">
          <h1 class="modal-title fs-5" id="exampleModalLabel">Modal title</h1>
          <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
        </div>
        <div class="modal-body">
          <div v-if="recipe">
            <button type="button" class="btn-close" data-bs-dismiss="modal" data-bs-target="#recipeDetailsModal" aria-label="Close">
            </button>
            <div class="card-custom-chef">
              <p>{{ recipe.category }}</p>
            </div>
              <div class="card card-custom border-white border-0 my-5" style="">
 
        
                </div>
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
        </div>
        <div class="modal-footer">
          <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
          <button type="button" class="btn btn-primary">Save changes</button>
        </div>
      </div>
    </div>
  </div>














  <!-- <div class="modal fade" id="recipeDetailsModal" aria-labelledby="recipeDetailsModalLabel" aria-hidden="true">
    <div class="modal-dialog modal-xl">
      <div class="modal-content">
        <slot></slot>
      </div>
    </div>
  </div> -->
</template>

<script>

import { ref } from 'vue'
import { recipesService } from '../services/RecipesService'
import { logger } from '../utils/Logger'
import Pop from '../utils/Pop'
import { Modal } from 'bootstrap'
import { computed } from 'vue'
import { AppState } from '../AppState'

export default {
  setup() {

    const modal = ref(null)


    return {

      modal,

      account: computed(() => AppState.account),
      recipe: computed(() => AppState.activeRecipe),

      async getRecipeById(recipeId) {
        try {
          logger.log("[GETTING RECIPE BY ID]")
          await recipesService.getRecipeById(recipeId)
          modal.value = new Modal(document.getElementById('recipeDetailsModal'))
          Modal.getOrCreateInstance('#recipeDetailsModal').show()
        }
        catch (error) {
          Pop.error(error.message);
          logger.log(error);
        }
      },
      

      
    }
  }
}
</script>


<style lang="scss" scoped></style>