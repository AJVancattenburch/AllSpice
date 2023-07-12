<template>



  <div class="offcanvas offcanvas-fullscreen offcanvas-end bg-offcanvas px-0" data-bs-scroll="true" tabindex="-1" id="myOffcanvas" aria-labelledby="myOffcanvasLabel">
    <div class="col-1 justify-content-end align-items-center">
      <button type="button" class="btn-close" data-bs-dismiss="offcanvas" aria-label="Close"></button>
    </div>
    <div class="offcanvas-header ps-5">
      <div class="row container ps-5">
        <div class="col-12 d-flex justify-content-center align-items-center">
          <h1 class="search-title text-center">What Are You <span class="inner-search-title">Craving</span> today?</h1>
        </div>
      </div>
    </div>
    <div class="offcanvas-body row justify-content-center align-items-center scrollable">
      <div class="container-fluid" style="overflow-y: auto; width: 100vw;">
        <div class="col-12" style="">
          
          <div class="card bg-card text-light">
            <div class="card-body">
              

              <div class="row mt-5 py-1">
                <div class="col-12 d-flex justify-content-center align-items-center m-auto" style="width: auto;">
                  <form @submit.prevent="searchRecipes()" class="d-flex " role="search">
                    <input type="search" class="form-control" style="width: 75vw;" placeholder="Search Recipes..." aria-label="Search" v-model="editable">
                    <button class="btn btn-outline-light ms-2" style="width: auto;" type="submit">Search</button>
                  </form>
                </div>
              </div>

              <div class="row justify-content-center align-items-center mt-5">
                <div v-for="(recipe, each) in recipes" :key="each.id" class="col-12 col-md-6 p-1 my-3">
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

    const editable = ref('')

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

@import url('https://fonts.googleapis.com/css2?family=Fira+Sans:wght@300;400;600&display=swap');

$firaSans: 'Fira Sans', sans-serif;

.bg-card {
  background-color: #fff9d2c1;
  background-image: url(https://img.rawpixel.com/s3fs-private/rawpixel_images/website_content/v728-mynt-22_1.jpg?auto=format&bg=F4F4F3&con=3&cs=srgb&dpr=1&fm=jpg&ixlib=php-3.1.0&mark=rawpixel-watermark.png&markalpha=90&markpad=13&markscale=10&markx=25&q=75&usm=15&vib=3&w=1200&s=9ebacda16a793f653d639a3c6f036b1d);
  background-size: contain;
  object-fit: contain;
  background-repeat: repeat;
  background-position: sticky;
  background-blend-mode: multiply;
  border: 1px solid rgba(255, 255, 255, 0.5);
  box-shadow: 0 0 40px inset #fd0101d5;
  border: none;
  width: 100%;
  min-height: 100%;
  padding-top: 10rem;
  padding-bottom: 10rem;
}

.btn-close {
  color: #fff;
  text-shadow: 1px 1px 1px black;
  box-shadow: 1px 1px 3px black;
  background-color: #fd0101d5;
  height: 2rem;
  width: 2rem;
  margin: 1rem 1rem;
}

.search-title {
  text-shadow: 1px 1px 1px black;
  font-family: $firaSans;
  font-size: 3rem;
  font-weight: 600;
  color: #462a2ae4;
  text-shadow: 2px 2px 4px #e1d584;
  filter: brightness(.9);

  .inner-search-title {
    color: rgb(255, 25, 0);
    text-shadow: 3px 3px 2px #eddc7c;
    filter: brightness(1);
  }
}

.bg-offcanvas {
  background-color: #f88c8ceb;
  background-blend-mode: color-burn;
  width: auto;
  position: fixed;
  top: 0;
  right: 0;
  overflow-y: auto;
  overflow-x: hidden;
}

input[type="search"] {
  background-color: #ffffff;
  box-shadow: 0 0 10px inset #ffdeded5;
  border: 3px groove #fd0101ab;
  color: #251616f6;
  font-family: $firaSans;
  font-weight: 500;
  text-shadow: 2px 2px 4px #e1d584;
  filter: brightness(.9);
}

button[type="submit"] {
  background-color: #fd0101d5;
  box-shadow: 0 0 10px inset #ffdeded5;
  border: 3px groove #fd0101d5;
  color: #fff;
  text-shadow: .5px .5px 2px black;
  font-family: $firaSans;
  font-weight: 500;
  filter: brightness(.9);
}

.card-hover {
  margin-top: 1rem;
}

</style>