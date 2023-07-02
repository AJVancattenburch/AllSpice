<template>
  <!-- FIXME MODAL NEEDS TO BE FIXED SO THE SLOTS VISUALLY DISPLAY CORRECTLY -->
  <div class="modal-body row" v-if="recipe" :key="recipe?.id">
    <button type="button" class="btn-close" data-bs-dismiss="modal" data-bs-target="#activeRecipeModal"
      aria-label="Close"></button>
    <p>blah blah</p>


  </div>

</template>

<script>

import { AppState } from "../AppState.js";
import { Recipe } from '../models/Recipe.js'
import { computed } from 'vue'

export default {
  
  setup() {
    
    return {

      recipe: computed(() => AppState.activeRecipe)
    }
  }

}

</script>

<style scoped lang="scss">



</style>