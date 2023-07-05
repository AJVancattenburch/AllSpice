<template>

  <div v-if="modal">
    <button @click="openModal()" type="button" class="btn btn-primary" data-bs-toggle="modal" data-bs-target="#recipeDetailsModal">
      Launch demo modal
    </button>
    
    <!-- Modal -->
    <div class="modal fade" id="recipeDetailsModal" tabindex="-1" aria-labelledby="recipeDetailsModalLabel" aria-hidden="true">
      <div class="modal-dialog">
        <div class="modal-content">
          <div class="modal-header">
            <h1 class="modal-title fs-5" id="exampleModalLabel">Modal title</h1>
            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
          </div>
          <div v-if="recipe" class="modal-body">
            <p> {{ recipe.title }} </p>
          </div>
          <div class="modal-footer">
            <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
            <button type="button" class="btn btn-primary">Save changes</button>
          </div>
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

import { ref, watchEffect, computed } from 'vue'
import { recipesService } from '../services/RecipesService'
import { logger } from '../utils/Logger'
import Pop from '../utils/Pop'
import { Modal } from 'bootstrap'
import { AppState } from "../AppState.js"

export default {
  setup() {

    const modal = ref({})

    watchEffect(() => {
      // modal.value = Modal.getOrCreateInstance('#recipeDetailsModal')
    })

    return {

      modal,
      recipe: computed(() => AppState.activeRecipe),

      async openModal() {
        try {
          logger.log(modal.value)
          const recipeData = modal.value
          await recipesService.getRecipeById(recipeData.id)
          Modal.getOrCreateInstance('#recipeDetailsModal').hide()
        } catch (error) {
          logger.error(error)
          Pop.toast(error, 'Failed to open modal')
        }
      }

      // async openModal() {
      //   try {
      //     logger.log(modal.value)
      //     const recipeData = modal.value
      //     Modal.getOrCreateInstance('#recipeDetailsModal').hide()
      //   } catch (error) {
      //     logger.error(error)
      //     Pop.toast(error, `Failed to open ${modal.value.title}}`)
      //   }
      // }
    }
  }
}
</script>


<style lang="scss" scoped></style>