<template>

  <section class="container-fluid">
      <CreateRecipeForm />
  </section>
</template>

<script>
import { computed } from 'vue';
import { AppState } from '../AppState';
import { recipesService } from '../services/RecipesService';
import { logger } from '../utils/Logger';
// import CreateRecipeButton from '../components/CreateRecipeButton.vue';
import CreateRecipeForm from '../components/CreateRecipeForm.vue';

export default {

  components: {
    CreateRecipeForm, 
  },

  setup() {

    async function getAllIngredients() {
      try {
        logger.log('[GETTING INGREDIENTS]')
        await recipesService.getAllIngredients()
      } catch (error) {
        logger.log(error)
      }
    }

    async function getIngredientById(ingredientId) {
      try {
        logger.log('[GETTING INGREDIENT BY ID]')
        await recipesService.getIngredientById(ingredientId)
      } catch (error) {
        logger.log(error)
      }
    }

    async function addIngredientToRecipe(ingredientData) {
      try {
        await recipesService.addIngredientToRecipe(ingredientData)
      } catch (error) {
        logger.log(error)
      }
    }

    return {
      getAllIngredients,
      getIngredientById,
      addIngredientToRecipe,
      account: computed(() => AppState.account),
      ingredients: computed(() => AppState.ingredients)
    }
  }
}

// watchEffect(() => {
//   const ingredient = AppState.ingredients.find(i => i.id === AppState.selectedIngredient)
//   if (ingredient) {
//     AppState.recipe.ingredients.push(ingredient)
//   }
// })

</script>

<style scoped>
img {
  max-width: 100px;
}
</style>
