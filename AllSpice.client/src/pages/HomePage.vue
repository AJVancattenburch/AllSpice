<template>

<section class="container-fluid">
    <div class="row justify-content-center align-items-center">
        <div class="col-12 col-md-3 p-5" v-for="r in recipes" :key="r.id">
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
  height: 500px;
  width: 100%;
  object-fit: cover;
  object-position: center;
}

</style>
