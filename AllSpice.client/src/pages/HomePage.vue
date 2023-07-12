<template>

  <!-- SECTION - HERO IMAGE ------------------------------------>
<div class="col-12 justify-content-center align-items-center">
  <img src="../assets/img/bg-image.png" class="hero-img" alt="hero-image">
</div>

<!-- SECTION - BACKDROP IMAGE ------------>
<section class="container-fluid bg-img">

  <!-- SECTION = BODY CONTENT -->
  <section class="content">

    <div class="d-flex col-12 justify-content-end align-items-center">
      <CreateRecipeButton class="sticky button justify-content-end align-items-center fs-5" style="position: relative; top: 2rem; z-index: 0; outline: 4px ridge #6a13138d;" />
    </div>

    <section>
      <div class="col-12 d-flex justify-content-center align-items-center" v-if="user.isAuthenticated">
        <router-link :to="{ name: 'Account' }">
          <img src="https://cdn4.iconfinder.com/data/icons/kitchen-129/64/25_book_recipes_recipe_kitchen_cook_cooking_food-512.png" title="My Cookbook" class="img-fluid my-cookbook" role="button" style="" alt="account-link">
        </router-link>
      </div>
      <div v-else>
        <img src="https://cdn4.iconfinder.com/data/icons/kitchen-129/64/25_book_recipes_recipe_kitchen_cook_cooking_food-512.png" class="img-fluid my-cookbook inactive disabled" role="button" style="" alt="account-link">
      </div>
    </section>

    <section style="margin-top: 0rem;">
      <div class="row justify-content-center align-items-center pt-0">
        <div class="col-md-6 col-12 recipe-header text-center mb-3 rounded p-3">
          <h1>Select your FlavorIt Category</h1>
        </div>
      </div>

      <div class="row justify-content-center align-items-center mb-5 pb-5">
        <div class="col-6 box d-flex justify-content-center align-items-center mb-3 rounded p-3">
          <select v-model="filterBy" class="custom-dropdown select-list text-center">
            <option @click="filterBy = ''" :value="''">All</option>
            <option @click="filterBy = 'Starters'">Starters</option>
            <option @click="filterBy = 'Mexican'">Mexican</option>
            <option @click="filterBy = 'Italian'">Italian</option>
            <option @click="filterBy = 'American'">American</option>
            <option @click="filterBy = 'Chinese'">Chinese</option>
            <option @click="filterBy = 'Soup'">Soups</option>
            <option @click="filterBy = 'Cheese'">Cheese</option>
            <option @click="filterBy = 'Salad'">Salads</option>
            <option @click="filterBy = 'Dessert'">Desserts</option>
            <option @click="filterBy = 'Specialty Coffee'">Specialty Coffee</option>
            <option @click="filterBy = 'Other'">Other</option>
          </select>
        </div>
      </div>

    </section>

    <div class="row justify-content-center align-items-center">
      <div v-for="(recipe, each) in recipes" :key="each.id" class="col-12 col-md-4 m-auto my-3" style="">
        <RecipeCard :recipe="recipe" />
      </div>
    </div>

  </section>

</section>


<!-- <div class="modal-header">
  <h1 class="modal-title fs-5" id="staticBackdropLabel">Modal title</h1>
  <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
</div>
<div class="modal-body">
  
</div>
<div class="modal-footer">
  <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
  <button type="button" class="btn btn-primary">Understood</button>
</div> -->

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
import { Recipe } from "../models/Recipe.js"

export default {

  name: "HomePage",

  components: {
    RecipeCard, 
    CreateRecipeButton,
  },

  props: {
    recipe: {
      type: Recipe,
      required: true
    }
  },

  setup() {

    const filterBy = ref('')
    const editable = ref({})

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

      editable,
      filterBy,

      user: computed(() => AppState.user),
      account: computed(() => AppState.account),

      recipes: computed(() => {
        if (!filterBy.value) {
          return AppState.recipes
        }
          return AppState.recipes.filter(r => r.category === filterBy.value)
      })
    }
  }
}

</script>

<style scoped lang="scss">

.container-fluid {
  background-image: url(https://wallpaperaccess.com/full/6361240.jpg);
  background-blend-mode: color;
  background-size: cover;
  background-position: center;
  background-repeat: no-repeat;
  background-attachment: fixed;
  height: 100%;
  width: 100%;
  box-shadow: inset 0px 0px 40px 40px #ff514144;
}

.hero-img {
  height: 75vh;
  width: 100%;
  object-fit: cover;
  background-position: center;
  background-repeat: no-repeat;
  margin-top: 0;
  opacity: 1;
}

.my-cookbook {
  height: 15rem;
  aspect-ratio: 1/1;
  z-index: 0;
  position: absolute;
  top: 15rem;
  left: 43%;
  right: 43%;
  opacity: 0;
  transition: all .3s ease-in-out;
  animation: slideInCookBook 2s ease-in-out .7s forwards;
}

@keyframes slideInCookBook {
  0% { 
      opacity: 0;
  }
  100% {
    transform: 
      translateX(0%) 
      perspective(200px) 
      rotateZ(30deg)  
      scale(1, .7);
    filter: brightness(1.1);
    opacity: .9;
  }
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
  filter: brightness(1.1);
  transition: all .3s ease-in-out;
}

.button {
  position: grid;
  top: -20rem;
}

.button:hover {
  animation: tilt .5s ease-in-out forwards !important;
}

h1,
.recipe-header {
  font-size: 3rem;
  color: #281212;
  background-color: transparent;
  text-shadow: 1.5px 15px 10px #9e2828;
  filter: brightness(1.5) drop-shadow(0px 0px -5px #ff000080);
}


.box select {
  background: linear-gradient(90deg, #FFE27D 0%, #ffb05b 30%, #ff916c 85%);
  background-color: #ffa06c;
  color: #9e2828;
  color: #170F1E;
  box-sizing: border-box;
  box-shadow: -2px -2px 5px #ff000080, 2px 2px 5px #ff955480;
  text-shadow: 1px 1px 2px #fccccc;
  padding: 12px;
  width: 250px;
  font-size: 20px;
  box-shadow: 0 5px 25px #00000033;
  -webkit-appearance: button;
  appearance: button;
  outline: none;
  border: 3px ridge #170f1e72;
  border-radius: .5rem;
  font-weight: 500;
  letter-spacing: 1px;
  transition: all .3s ease-in-out;
}

.box select::before {
  content: "\f13a";
  font-family: FontAwesome;
  width: 20%;
  height: 100%;
  text-align: center;
  font-size: 28px;
  line-height: 45px;
  color: #79535399;
  background-color: #391b1bc5;
  pointer-events: none;
}

.box select:hover::before {
  color: #79535399;
  background-color: #391b1bc5;
}


.box select.custom-dropdown>option {
  padding: 30px;

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
    transform: rotateZ(0deg);
  }

  50% {
    transform: rotateZ(-10deg);
  }

  100% {
    transform: rotateZ(-2deg);
  }
}

</style>
