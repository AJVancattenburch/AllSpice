<template>

<section class="container-fluid">

  <section class="bg-panImg">
    <div class="row">
      <div class="col-12 px-0">
        <!-- <img src="" alt="hero-image"> -->
      </div>
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
import { onMounted, computed } from "vue"
import { recipesService } from "../services/RecipesService.js"
import { AppState } from "../AppState.js"
import RecipeCard from "../components/RecipeCard.vue"

export default {

  name: "HomePage",

  components: {
       RecipeCard
  },

    setup() {
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

        async function getRecipeById(recipeId) {
            try {
                logger.log("[GETTING RECIPE BY ID]");
                await recipesService.getRecipeById(recipeId);
            }
            catch (error) {
                Pop.error(error.message);
                logger.log(error);
            }
        }

        

        onMounted(() => {
            getAllRecipes();
            getRecipeById();
        });
        return {
            recipes: computed(() => AppState.recipes),
            account: computed(() => AppState.account),
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
