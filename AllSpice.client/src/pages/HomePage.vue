<template>

<section class="container-fluid">

  <section class="bg-panImg">
    <div class="row">
      <div class="col-12 px-0">
        <!-- <img src="" alt="hero-image"> -->
      </div>
      </div>
  </section>

  <section>
    <div class="row">
      <div class="col-12 d-flex justify-content-center align-items-center my-3 rounded p-3">
        <h1>Select your FlavorIt Category</h1>
      </div>
    </div>
    <div class="row">
      <div class="col-12 d-flex justify-content-center align-items-center my-3 rounded p-3">
        <button @click="filterBy = ''" class="col-1 bg-dark btn btn-outline-light mx-2">All</button>
        <button @click="filterBy = 'Specialty Coffee'" class="col-1 bg-dark btn btn-outline-light mx-2">Specialty Coffee</button>
        <button @click="filterBy = 'Starters'" class="col-1 bg-dark btn btn-outline-light mx-2">Starters</button>
        <button @click="filterBy = 'Mexican'" class="col-1 bg-dark btn btn-outline-light mx-2">Mexican</button>
        <button @click="filterBy = 'Italian'" class="col-1 bg-dark btn btn-outline-light mx-2">Italian</button>
        <button @click="filterBy = 'American'" class="col-1 bg-dark btn btn-outline-light mx-2">American</button>
        <button @click="filterBy = 'Chinese'" class="col-1 bg-dark btn btn-outline-light mx-2">Chinese</button>
        <button @click="filterBy = 'Soup'" class="col-1 bg-dark btn btn-outline-light mx-2">Soups</button>
        <button @click="filterBy = 'Cheese'" class="col-1 bg-dark btn btn-outline-light mx-2">Cheese</button>
        <button @click="filterBy = 'Dessert'" class="col-1 bg-dark btn btn-outline-light mx-2">Desserts</button>
      </div>
    </div>
    <div class="row">
      <button @click="filterBy = 'Other'" class="col-1 justify-content-center align-items-center bg-dark btn btn-outline-light mx-2">Other</button>
    </div>
  </section>

  <div class="row justify-content-center align-items-center">
    <div class="col-12 col-md-4 p-5" v-for="r in recipes" :key="r.id">
      <RecipeCard :recipe="r"/>
    </div>
  </div>
</section>

</template>

<script>
import { logger } from "../utils/Logger.js"
import Pop from "../utils/Pop.js"
import { onMounted, computed, ref } from "vue"
import { recipesService } from "../services/RecipesService.js"
import { ingredientsService } from "../services/IngredientsService.js"
import { AppState } from "../AppState.js"
import RecipeCard from "../components/RecipeCard.vue"

export default {

  name: "HomePage",

  components: {
       RecipeCard
  },

    setup() {

      const filterBy = ref('')

        async function getAllRecipes() {
            try {
                logger.log("[GETTING RECIPES]");
                await recipesService.getAllRecipes();
            }
            catch (error) {
                Pop.error(error.message);
                logger.log(error);
            }
        }

        // async function getRecipeById(recipeId) {
        //     try {
        //         logger.log("[GETTING RECIPE BY ID]");
        //         await ingredientsService.getRecipeById(recipeId);
        //     }
        //     catch (error) {
        //         Pop.error(error.message);
        //         logger.log(error);
        //     }
        // }

        async function getIngredientByRecipeId(recipeId) {
            try {
                logger.log("[GETTING INGREDIENTS BY RECIPE ID]");
                await recipesService.getIngredientByRecipeId(recipeId);
            }
            catch (error) {
                Pop.error(error.message);
                logger.log(error);
            }
        }

        onMounted(() => {
            getAllRecipes();
            // getRecipeById();
        });

        return {

          filterBy,
          account: computed(() => AppState.account),
          recipes: computed(() => {
            if (!filterBy.value) {
              return AppState.recipes
            }
            return AppState.recipes.filter(r => r.category === filterBy.value)
          }),


        };
    },
}
</script>

<style scoped lang="scss">

.container-fluid {
  background-color: #ffcd8de6;
  background-size: cover;
  background-position: center;
  background-repeat: no-repeat;
  background-attachment: fixed;
  height: 100%;
}

.hero-img {
  background-image: url(https://reymannfoundation.org/wp-content/uploads/2020/01/recipe-hero-compressor.jpg);
  height: 100%;
  width: 100%;
  object-fit: cover;
  background-repeat: repeat;
  background-size: cover;
}

.bg-panImg {
  
  animation: panImg 10s ease-in-out infinite;
}

@keyframes panImg {
  0%, 100% {
    background-position: left top;
  }

  25% {
    background-position: right bottom;
  }

  50% {
    background-position: left bottom;
  }

  75% {
    background-position: right top;
  }   
}

</style>
