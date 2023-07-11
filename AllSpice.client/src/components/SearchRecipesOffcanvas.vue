<template>


  <button class="btn btn-primary" style="" type="button" data-bs-toggle="offcanvas" data-bs-target="#myOffcanvas" aria-controls="myOffcanvas">Enable both scrolling & backdrop</button>

  <div class="offcanvas offcanvas-xl offcanvas-end bg-offcanvas px-0" data-bs-scroll="true" tabindex="-1" id="myOffcanvas" aria-labelledby="myOffcanvasLabel">
    <div class="offcanvas-header">
      <h5 class="offcanvas-title" id="offcanvasWithBothOptionsLabel">Backdrop with scrolling</h5>
      <button type="button" class="btn-close" data-bs-dismiss="offcanvas" aria-label="Close"></button>
    </div>
    <div class="offcanvas-body row justify-content-center align-items-center pt-5 scrollable">
      <div class="container-fluid" style="overflow-y: auto; overflow-x: hidden;">
        <div class="col-12" style="width: 1000px;">

          <div class="card bg-card text-light">
            <div class="card-body">
              <div class="row">
                <div class="col-12">
                  <h1 class="text-center">Search Recipes</h1>
                </div>
              </div>

              <div class="row">
                <div class="col-12">
                  <form @submit.prevent="searchRecipes()" class="d-flex" role="search">
                    <input type="text" class="form-control me-2" placeholder="Search Recipes..." aria-label="Search" v-model="editable">
                    <button class="btn btn-outline-light mx-2" style="color: aliceblue; text-shadow: 1px 1px 1px black; box-shadow: 1px 1px 3px black; background-color: #f1ff55b8;" type="submit">Search</button>
                  </form>
                </div>
              </div>

              <div class="row mt-5">
                <div v-for="(recipe, each) in recipes" :key="each.id" class="col-12 d-flex justify-content-center align-items-center col-md-6 p-1 my-3">
                  <RecipeCard :recipe="recipe" />
                </div>
              </div>


            </div>
          </div>
        </div>
      </div>
    </div>
  </div>  


</template>

<script>

import { logger } from '../utils/Logger'
import { ref, computed } from 'vue'
import { AppState } from '../AppState'
import { recipesService } from '../services/RecipesService'
import Pop from "../utils/Pop"
import { Offcanvas } from "bootstrap"
import RecipeCard from "../components/RecipeCard.vue"

export default {

  components: {
    RecipeCard
  },

  setup() {

    const editable = ref({})

    return {

      editable,

      async searchRecipes() {
        try {
          const searchTerm = editable.value
            .split(' ')
            .join('+')
          logger.log("[SEARCH TERM] =>",searchTerm)
          AppState.query = searchTerm
          await recipesService.searchRecipes(searchTerm)
          editable.value = ''
          Offcanvas.getOrCreateInstance('#myOffcanvas').show()
          // router.push(
          //   { name: 'Search' , params: { query: searchString }}
          // )
        } catch (error) {
          logger.error(error)
          Pop.toast(error.message, 'error')
        }
      },

      recipes: computed(() => AppState.recipes)
    }
  }
}

</script>

<style scoped lang="scss">

.bg-card {
  background-color: #ff9e6afe;
  border: 1px solid rgba(255, 255, 255, 0.5);
  box-shadow: 1px 1px 3px black;
}

.bg-offcanvas {
  background-color: #ff7474d5;
  width: auto;
  position: fixed;
  top: 0;
  right: 0;
  overflow-y: auto;
  overflow-x: hidden;
}

.card-hover {
  margin-top: 1rem;
  position: absolute;
}

</style>