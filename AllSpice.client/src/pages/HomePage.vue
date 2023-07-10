<template>

<section class="container-fluid bg-img">
  
  <section class="content">

    <section class="bg-panImg">
      <div class="row">
        <div class="col-12 justify-content-center align-items-center px-0">
          <img src="https://www.haushaltstipps.net/wp-content/uploads/gewuerzmischung-selber-machen.jpeg" class="hero-img" alt="hero-image">
        </div>
      </div>
    </section>
  
    <section>
      <div class="col-12 d-flex justify-content-center align-items-center" v-if="user.isAuthenticated">
        <router-link :to="{ name: 'Account' }">
          <img src="https://cdn4.iconfinder.com/data/icons/kitchen-129/64/25_book_recipes_recipe_kitchen_cook_cooking_food-512.png" title="My Cookbook" class="img-fluid my-cookbook" role="button" style="" alt="account-link">
        </router-link>
      </div>
  
      <div v-else>
        <img src="https://cdn4.iconfinder.com/data/icons/kitchen-129/64/25_book_recipes_recipe_kitchen_cook_cooking_food-512.png" class="img-fluid my-cookbook inactive disabled" role="button" style="" alt="account-link">
      </div>
  
      <div class="col-12 d-flex justify-content-center align-items-center pt-4">
        <CreateRecipeButton class="sticky button justify-content-center align-items-center fs-5" style="position: inherit; margin-top: -23rem; height: 60px; width: 200px; z-index: 1;" />
      </div>
  
      <!-- <Offcanvas id="offcanvasWithBothOptions">
        <CreateRecipeForm />
      </Offcanvas> -->
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
      <div class="col-12 col-md-4 p-5" v-for="(recipe, each) in recipes" :key="each.id">
        <RecipeCard :recipe="recipe" />
      </div>
    </div>

  </section>

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
import CreateRecipeButton from "../components/CreateRecipeButton.vue"
// import CreateRecipeForm from "../components/CreateRecipeForm.vue"
// import Offcanvas from "../components/Offcanvas.vue"

export default {

  name: "HomePage",

  components: {
       RecipeCard, 
       CreateRecipeButton, 
      //  CreateRecipeForm, 
      //  Offcanvas
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
        });

        return {

          filterBy,
          user: computed(() => AppState.user),
          account: computed(() => AppState.account),
          recipes: computed(() => {
            if (!filterBy.value) {
              return AppState.recipes
            }
            return AppState.recipes.filter(r => r.category === filterBy.value)
          }),
          allRecipes: computed(() => AppState.recipes),


        };
    },
}
</script>

<style scoped lang="scss">

.container-fluid {
  background-image: url(https://cmkt-image-prd.freetls.fastly.net/0.1.0/ps/4236966/1820/1213/m1/fpnw/wm1/sogsvrlqh72xgiipezdauuzhs2sr0bybcel6505dy3y2aagtywzijgbxnisfyu46-.jpg?1522829692&s=01b060822bfbcbbb0623af512a70f03d);
  background-blend-mode: color;
  background-size: cover;
  background-position: center;
  background-repeat: no-repeat;
  background-attachment: fixed;
  height: 100%;
  box-shadow: inset 0px 0px 40px 40px #ff514144;
}

.hero-img {
  height: 100%;
  width: 100%;
  object-fit: cover;
  background-repeat: repeat;
  margin-bottom: -10rem;
  margin-top: 0;
  opacity: 1;
  filter: drop-shadow(0px 0px 10px #3e0c0c);
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

.my-cookbook {
  height: 10rem;
  aspect-ratio: 1/1;
  z-index: 1;
  position: relative;
  top: -15em;
  opacity: .9;
  transition: all .3s ease-in-out;
}

@keyframes bounceInCookbook {
  0% {
    transform: 
      translateX(250%) 
      rotate(0deg) 
      translateY(-100%);
  }
  50% {
    transform: 
      translateX(50%) 
      rotate(-360deg) 
      translateY(45%) 
      scale(1, .5);
  }
  100% {
    transform: 
      translateX(0%) 
      rotate(360deg) 
      perspective(200px) 
      rotateZ(30deg) 
      translateY(0%) 
      scale(1, .7);
    filter: brightness(1.1);
  }
}

.my-cookbook:hover {
  opacity: 1;
  transition: all .3s ease-in-out;
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
