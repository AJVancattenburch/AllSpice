<template>
  
  <!-- Modal -->
  <div v-if="recipe" :key="recipe?.id" class="modal fade" id="staticBackdrop" data-bs-backdrop="static" data-bs-keyboard="false" tabindex="-1" aria-labelledby="staticBackdropLabel" aria-hidden="true">
    <div class="modal-dialog">
      <div class="modal-content">
        <div class="modal-header">
          <h1 class="modal-title fs-5" id="staticBackdropLabel">Modal title</h1>
          <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
        </div>

        <RecipeDetailsCard />

        <div class="modal-footer">
          <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
          <button type="button" class="btn btn-primary">Understood</button>
        </div>
      </div>
    </div>
  </div>  

</template>

<script>

import { ref } from 'vue'
import { recipesService } from '../services/RecipesService'
import { logger } from '../utils/Logger'
import Pop from '../utils/Pop'
import { Modal } from 'bootstrap'
import { computed } from 'vue'
import { AppState } from '../AppState'
import { Recipe } from '../models/Recipe'
import RecipeDetailsCard from '../components/RecipeDetailsCard.vue'

export default {

  props: {
    recipe: {
      type: Recipe,
      required: true
    }
  },

  components: {
    RecipeDetailsCard
  },

  setup() {

    const modal = ref(null)


    return {

      modal,

      account: computed(() => AppState.account),
      // recipe: computed(() => AppState.activeRecipe),

      async getRecipeById(recipeId) {
        try {
          logger.log("[GETTING RECIPE BY ID]")
          await recipesService.getRecipeById(recipeId)
          Modal.getOrCreateInstance('#staticBackdrop').hide()
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