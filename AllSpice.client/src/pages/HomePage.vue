<template>

<section class="container-fluid">

  <section class="bg-panImg">
    <div class="row">
      <div class="col-12 px-0">
        <img src="https://www.haushaltstipps.net/wp-content/uploads/gewuerzmischung-selber-machen.jpeg" class="hero-img" alt="hero-image">
        <!-- <button class="btn btn-primary">sFDASD</button> -->
      </div>
      </div>
    </section>

  <section>
    <div class="row pt-0">
      <div class="col-12 d-flex justify-content-center align-items-center mb-3 rounded px-3 pb-3">
        <h1>Select your FlavorIt Category</h1>
      </div>
    </div>
    <div class="row">
      <div class="col-12 d-flex justify-content-center align-items-center mb-3 rounded p-3">
        <button @click="filterBy = ''" class="col-1 btn btn-category mx-2">All</button>
        <button @click="filterBy = 'Starters'" class="col-1 btn btn-category mx-2">Starters</button>
        <button @click="filterBy = 'Mexican'" class="col-1 btn btn-category mx-2">Mexican</button>
        <button @click="filterBy = 'Italian'" class="col-1 btn btn-category mx-2">Italian</button>
        <button @click="filterBy = 'American'" class="col-1 btn btn-category mx-2">American</button>
        <button @click="filterBy = 'Chinese'" class="col-1 btn btn-category mx-2">Chinese</button>
        <button @click="filterBy = 'Soup'" class="col-1 btn btn-category mx-2">Soups</button>
        <button @click="filterBy = 'Cheese'" class="col-1 btn btn-category mx-2">Cheese</button>
      </div>
    </div>
    <div class="row justify-content-center align-items-center">
      <button @click="filterBy = 'Dessert'" class="col-1 btn btn-category mx-2">Desserts</button>
      <button @click="filterBy = 'Specialty Coffee'" class="col-2 btn btn-category mx-2">Specialty Coffee</button>
      <button @click="filterBy = 'Other'" class="col-1 btn btn-category mx-2">Other</button>
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
import { onMounted, computed, ref, watchEffect } from "vue"
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

        async function getIngredientsByRecipeId(recipeId) {
          try {
            await ingredientsService.getIngredientsByRecipeId(recipeId)
          } catch (error) {
            logger.log(error)
            Pop.error(error.message)
          }
        }

        watchEffect(() => {
          if (AppState.activeRecipe) {
            AppState.ingredients = []
            getIngredientsByRecipeId(AppState.activeRecipe.id)
          }
        })

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
  height: 75%;
  width: 100%;
  object-fit: cover;
  background-repeat: repeat;
  background-size: cover;
  margin-bottom: -15rem;
  margin-top: 0;
  opacity: .9;
}

.hero-img:hover {
  filter: blur(1px) brightness(1.1);
}

.bg-panImg {
  animation: panImg 30s ease-out infinite;
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

.btn-category {
  background-color: #ffb048d3;
  text-shadow: 1px 1px 2px #ffffffe3;
  color: #170F1E;
  box-shadow: -2px -2px 5px #ff000080, 2px 2px 5px #ff955480;
  box-sizing: content-box;
  text-align: center;
  border: 3px ridge #170f1e72;
  border-radius: .5rem;
  font-weight: 700;
  letter-spacing: 1px;
  transition: all .3s ease-in-out;
}

.btn-category:hover {
  background-color: #ff955480;
  box-shadow: -2px -2px 5px #ff000080, 2px 2px 5px #ff955480;
  color: #170F1E;
  animation: tilt .5s ease-in-out forwards;
}

@keyframes tilt {
  0% {
    transform: rotate(0deg);
  }

  50% {
    transform: rotate(-10deg);
  }

  100% {
    transform: rotate(0deg);
  }
}

</style>
